﻿using System;
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



                // Display Shorter Address of Billing by ... if length of string > 50 character

                hd.BillingAddress = lib.simplifyText(hd.BillingAddress, 50);

                res.Add(hd);
            }


            return res;
        }



        public List<HoaDonBanHang> searchBillOfSale(string searchType, string keyword)
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
        }
    }
}