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

        private void calculateTotalBillAndQuantity(ref List<HoaDonBanHang> list)
        {
            List<MatHang> listMh = m_MatHang.getMatHang();
            foreach (HoaDonBanHang hd in list)
            {
                float sumOfValue = 0;
                int sumOfQuantity = 0;
                foreach (ProductWithQuantity item in hd.ProductSold)
                {
                    int prodPrice = listMh.Find(mh => mh.Id == item.Id).Gia;
                    sumOfValue += prodPrice * item.Quantity;

                    sumOfQuantity += item.Quantity;
                }

                sumOfValue += sumOfValue * hd.VatTax;
                sumOfValue += hd.Shipfee;

                list.Find(x => x == hd).TotalValueOfBill = sumOfValue;
                list.Find(x => x == hd).TotalOfQuantity = sumOfQuantity;
            }
        }

        private void simplyAddress(ref List<HoaDonBanHang> list)
        {
            foreach (HoaDonBanHang hd in list)
            {
                // Display Shorter Address ... if length of string > 50 character
                list.Find(x => x == hd).BillingAddress = lib.simplifyText(hd.BillingAddress, 50);
            }
        }

        public List<HoaDonBanHang> getHoaDonBanHang()
        {
            // Calculate Total Value Of Bill
            List<HoaDonBanHang> list = m_HoaDonBanHang.getHoaDonBanHang();

            calculateTotalBillAndQuantity(ref list);
            simplyAddress(ref list);

            return list;
        }


        public HoaDonBanHang getBillOfSaleById(string billId)
        {
            try
            {
                List<HoaDonBanHang> list = m_HoaDonBanHang.getHoaDonBanHang();
                HoaDonBanHang res = new HoaDonBanHang();
                foreach (HoaDonBanHang hd in list)
                {
                    if (hd.Id == int.Parse(billId))
                    {
                        res = hd;
                        break;
                    }
                }

                res.Dob = DateTime.Parse(res.Dob).ToString("yyyy-MM-dd");

                return res;
            }
            catch
            {
                return new HoaDonBanHang();
            }

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

                // Calculate Total Value of Bill
                calculateTotalBillAndQuantity(ref resultList);
                simplyAddress(ref resultList);

                return resultList;
            }
            catch
            {
                return new List<HoaDonBanHang>();
            }
        }


        public bool addNewBillOfSale(string customerName, string dob, string address, string billingAdress, string phone, string paymentMethod, int shipfee, List<ProductWithQuantity> listProductSold)
        {
            return m_HoaDonBanHang.addNewBillOfSale(customerName, dob, address, billingAdress, phone, paymentMethod, shipfee, listProductSold);
        }

        public bool modifyBillOfSale(int BillId, string customerName, string dob, string address, string billingAdress, string phone, string paymentMethod, int shipfee, List<ProductWithQuantity> listProductSold)
        {
            // Modify ListProductSold before call modifyBillOfSale()
            // If Product in list have same Id, we recalculate total of Quantity;

            List<int> listProductId = new List<int>();

            foreach(ProductWithQuantity p in listProductSold)
            {
                listProductId.Add(p.Id);
            }

            // Find duplicate
            var query = listProductId.GroupBy(x => x)
                          .Where(g => g.Count() > 1)
                          .Select(y => new { ProductId = y.Key, Counter = y.Count() })
                          .ToList();

            foreach(var prodDup in query)
            {
                // Calculate Quantity
                int sumOfQuantity = 0;
                foreach(ProductWithQuantity p in listProductSold)
                {
                    if (p.Id == prodDup.ProductId) sumOfQuantity += p.Quantity;
                }

                // Remove duplicate product from the list
                listProductSold.RemoveAll(p => p.Id == prodDup.ProductId);

                // Re-add
                ProductWithQuantity newProd = new ProductWithQuantity();
                newProd.Id = prodDup.ProductId;
                newProd.Quantity = sumOfQuantity;

                listProductSold.Add(newProd);
            }


            return m_HoaDonBanHang.modifyBillOfSale(BillId, customerName, dob, address, billingAdress, phone, paymentMethod, shipfee, listProductSold);
        }



        public bool deleteBillOfSale(int BillId)
        {
            return m_HoaDonBanHang.deleteBillOfSale(BillId);
        }
    }
}