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
                foreach (ProductWithQuantityAndUnitCost item in hd.ProductImported)
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

        public HoaDonNhapHang getGoodsReceivedNoteById(string billId)
        {
            try
            {
                List<HoaDonNhapHang> list = m_HoaDonNhapHang.getGoodsReceivedNote();
                HoaDonNhapHang res = new HoaDonNhapHang();
                foreach (HoaDonNhapHang hd in list)
                {
                    if (hd.Id == int.Parse(billId))
                    {
                        res = hd;
                        break;
                    }
                }

                return res;
            }
            catch
            {
                return new HoaDonNhapHang();
            }

        }


        public bool addNewGoodsReceivedNote(string shipperName, string personalId, string shipperAddress, string shipperPhone, int shipfee, List<ProductWithQuantityAndUnitCost> listProductImported)
        {
            return m_HoaDonNhapHang.addNewGoodsReceivedNote(shipperName, personalId, shipperAddress, shipperPhone, shipfee, listProductImported);
        }

        public bool deleteGoodsReceivedNote(int BillId) {
            return m_HoaDonNhapHang.deleteGoodsReceivedNote(BillId);
        }


        private void calculateTotalBillAndQuantity(ref List<HoaDonNhapHang> list)
        {
            List<MatHang> listMh = m_MatHang.getMatHang();
            foreach (HoaDonNhapHang hd in list)
            {
                int sumOfValue = 0;
                int sumOfQuantity = 0;
                foreach (ProductWithQuantityAndUnitCost item in hd.ProductImported)
                {
                    sumOfValue += item.UnitCost * item.Quantity;
                    sumOfQuantity += item.Quantity;
                }

                sumOfValue += hd.Shipfee;

                list.Find(x => x == hd).TotalValueOfGoodsReceivedNote = sumOfValue;
                list.Find(x => x == hd).TotalQuantityOfProduct = sumOfQuantity;
            }
        }

        private void simplyAddress(ref List<HoaDonNhapHang> list)
        {
            foreach (HoaDonNhapHang hd in list)
            {
                // Display Shorter Address ... if length of string > 50 character
                list.Find(x => x == hd).ShipperAddress = lib.simplifyText(hd.ShipperAddress, 50);
            }
        }

        public List<HoaDonNhapHang> searchGoodsReceivedNote(string searchType, string keyword)
        {
            try
            {
                List<HoaDonNhapHang> resultList = new List<HoaDonNhapHang>();
                List<HoaDonNhapHang> listGoodsReceivedNote = m_HoaDonNhapHang.getGoodsReceivedNote();
                switch (searchType)
                {
                    case "stype_maHoaDonNhapHang":
                        foreach (HoaDonNhapHang hd in listGoodsReceivedNote)
                        {
                            if (hd.Id == int.Parse(keyword))
                            {
                                resultList.Add(hd);
                                break;
                            }
                        }
                        break;

                    case "stype_shipperName":
                        foreach (HoaDonNhapHang hd in listGoodsReceivedNote)
                        {
                            if (hd.ShipperName.ToLower().Contains(keyword.ToLower()))
                            {
                                resultList.Add(hd);
                            }
                        }
                        break;

                    case "stype_shipperAddress":
                        foreach (HoaDonNhapHang hd in listGoodsReceivedNote)
                        {
                            if (hd.ShipperAddress.ToLower().Contains(keyword.ToLower()))
                            {
                                resultList.Add(hd);
                            }
                        }
                        break;

                    case "stype_personalId":
                        foreach (HoaDonNhapHang hd in listGoodsReceivedNote)
                        {
                            if (hd.PersonalID.ToLower().Contains(keyword.ToLower()))
                            {
                                resultList.Add(hd);
                            }
                        }
                        break;

                    case "stype_shipperPhone":
                        foreach (HoaDonNhapHang hd in listGoodsReceivedNote)
                        {
                            if (hd.ShipperPhone.ToLower().Contains(keyword.ToLower()))
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
                return new List<HoaDonNhapHang>();
            }
        }

        public bool modifyGoodsReceivedNote(int BillId, string shipperName, string personalId, string shipperAddress, string shipperPhone, int shipfee, List<ProductWithQuantityAndUnitCost> listProductImported)
        {
            // Modify ListProductSold before call modifyBillOfSale()
            // If Product in list have same Id, we recalculate total of Quantity;

            List<int> listProductId = new List<int>();

            foreach (ProductWithQuantity p in listProductImported)
            {
                listProductId.Add(p.Id);
            }

            // Find duplicate
            var query = listProductId.GroupBy(x => x)
                          .Where(g => g.Count() > 1)
                          .Select(y => new { ProductId = y.Key, Counter = y.Count() })
                          .ToList();

            foreach (var prodDup in query)
            {
                // Calculate Quantity
                int sumOfQuantity = 0;
                // Store Unit Cost Before Removing
                int tmp_unitcost = listProductImported.Find(t => t.Id == prodDup.ProductId).UnitCost;

                foreach (ProductWithQuantityAndUnitCost p in listProductImported)
                {
                    if (p.Id == prodDup.ProductId) sumOfQuantity += p.Quantity;
                }

                // Remove duplicate product from the list
                listProductImported.RemoveAll(p => p.Id == prodDup.ProductId);

                // Re-add
                ProductWithQuantityAndUnitCost newProd = new ProductWithQuantityAndUnitCost();
                newProd.Id = prodDup.ProductId;
                newProd.Quantity = sumOfQuantity;
                newProd.UnitCost = tmp_unitcost;

                listProductImported.Add(newProd);
            }
            return m_HoaDonNhapHang.modifyGoodsReceivedNote(BillId, shipperName, personalId, shipperAddress, shipperPhone, shipfee, listProductImported);
        }
    }
}