using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _1988216.MVC.Models;
using _1988216.MVC.Core;

namespace _1988216.MVC.Controllers
{
    public class C_HoaDonBanHang
    {
        private Lib lib;
        private M_HoaDonBanHang m_HoaDonBanHang;
        private M_MatHang m_MatHang;

        public C_HoaDonBanHang()
        {
            m_HoaDonBanHang = new M_HoaDonBanHang();
            m_MatHang = new M_MatHang();
            lib = new Lib();
        }

        public List<HoaDonBanHang> getHoaDonBanHang()
        {
            // Calculate Total Value Of Bill
            List<HoaDonBanHang> list = m_HoaDonBanHang.getHoaDonBanHang();
            List<MatHang> listMh = m_MatHang.getMatHang();
            List<HoaDonBanHang> res = new List<HoaDonBanHang>();

            foreach(HoaDonBanHang hd in list)
            {
                float sumOfValue = 0;
                int sumOfQuantity = 0;
                foreach (ProductWithQuantity item in hd.ProductSold)
                {
                    int prodPrice = listMh.Find(mh => mh.Id == item.Id).Gia;
                    sumOfValue += prodPrice*item.Quantity;

                    sumOfQuantity += item.Quantity;
                }

                sumOfValue += sumOfValue * hd.VatTax;
                sumOfValue += hd.Shipfee;
                hd.TotalValueOfBill = sumOfValue;

                hd.TotalOfQuantity = sumOfQuantity;



                // Display Shorter Address of Billing by ... if length of string > 30 character

                if(hd.BillingAddress.Length > 50)
                {
                    string text1 = hd.BillingAddress.Substring(0, 50) + " ...";
                    hd.BillingAddress = text1;
                }

                res.Add(hd);
            }


            return res;
        }
    }
}