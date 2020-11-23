using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _1988216.MVC.Models;
using _1988216.MVC.Core;

namespace _1988216.MVC.Controllers
{
    public class C_HoaDonNhapHang
    {
        private Lib lib;
        private M_HoaDonNhapHang m_HoaDonNhapHang;
        private M_MatHang m_MatHang;

        public C_HoaDonNhapHang()
        {
            m_HoaDonNhapHang = new M_HoaDonNhapHang();
            m_MatHang = new M_MatHang();
            lib = new Lib();
        }

        public List<HoaDonNhapHang> getGoodsReceivedNote()
        {
            List<HoaDonNhapHang> list = m_HoaDonNhapHang.getGoodsReceivedNote();
            List<MatHang> listMh = m_MatHang.getMatHang();
            List<HoaDonNhapHang> res = new List<HoaDonNhapHang>();

            foreach(HoaDonNhapHang hd in list)
            {
                int sumOfValue = 0;
                int sumOfQuantity = 0;
                foreach (ProductWithQuantityAndUnitCost item in hd.ProductList)
                {
                    sumOfValue += item.UnitCost*item.Quantity;

                    sumOfQuantity += item.Quantity;
                }

                hd.TotalValueOfGoodsReceivedNote = sumOfValue + hd.Shipfee;

                hd.TotalQuantityOfProduct = sumOfQuantity;



                // Display Shorter Address of Billing by ... if length of string > 50 character

                hd.ShipperAddress = lib.simplifyText(hd.ShipperAddress, 50);

                res.Add(hd);
            }


            return res;
        }



        /*public List<HoaDonBanHang> searchBillOfSale(string searchType, string keyword)
        {
            try
            {
                List<HoaDonBanHang> resultList = new List<HoaDonBanHang>();
                List<HoaDonBanHang> listBillOfSaleFromDB = m_HoaDonBanHang.getHoaDonBanHang();
                switch (searchType)
                {
                    case "stype_maHoaDonBanHang":
                        foreach (HoaDonBanHang hd in listBillOfSaleFromDB)
                        {
                            if (hd.Id == int.Parse(keyword))
                            {
                                resultList.Add(hd);
                                break;
                            }
                        }
                        break;

                    case "stype_customerName":
                        foreach (HoaDonBanHang hd in listBillOfSaleFromDB)
                        {
                            if (hd.CustomerName.ToLower().Contains(keyword.ToLower()))
                            {
                                resultList.Add(hd);
                            }
                        }
                        break;                    
                    
                    case "stype_paymentMethod":
                        foreach (HoaDonBanHang hd in listBillOfSaleFromDB)
                        {
                            if (hd.PaymentMethod.ToLower().Contains(keyword.ToLower()))
                            {
                                resultList.Add(hd);
                            }
                        }
                        break;
                }

                // Simply the Address Text

                foreach(HoaDonBanHang hd in resultList)
                {
                    hd.BillingAddress = lib.simplifyText(hd.BillingAddress, 50);
                }


                return resultList;
            }
            catch
            {
                return new List<HoaDonBanHang>();
            }
        }*/
    }
}