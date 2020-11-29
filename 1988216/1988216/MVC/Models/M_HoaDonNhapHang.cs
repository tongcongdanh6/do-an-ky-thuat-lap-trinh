using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace _1988216.MVC.Models
{
    public class M_HoaDonNhapHang
    {
        public List<HoaDonNhapHang> getGoodsReceivedNote()
        {
            List<HoaDonNhapHang> listOfGoodsReceivedNote = new List<HoaDonNhapHang>();
            string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/HoaDonNhapHangData.json");
            using (StreamReader r = new StreamReader(filePath))
            {
                var json = r.ReadToEnd();
                r.Close();
                dynamic data = JsonConvert.DeserializeObject(json);
                foreach (var jitem in data)
                {
                    HoaDonNhapHang hd = new HoaDonNhapHang();

                    // Convert từ Json sang Object MatHang
                    hd.Id = jitem["Id"];
                    hd.ShipperName = jitem["ShipperName"];
                    hd.PersonalID = jitem["PersonalID"];
                    hd.ShipperAddress = jitem["ShipperAddress"];
                    hd.ShipperPhone = jitem["ShipperPhone"];
                    hd.Currency = jitem["Currency"];
                    hd.Shipfee = jitem["Shipfee"];

                    List<ProductWithQuantityAndUnitCost> productList = new List<ProductWithQuantityAndUnitCost>();

                    foreach (var item in jitem["ProductImported"])
                    {
                        ProductWithQuantityAndUnitCost p = new ProductWithQuantityAndUnitCost();
                        p.Id = item["Id"];
                        p.Quantity = item["Quantity"];
                        p.UnitCost = item["UnitCost"];

                        productList.Add(p);
                    }



                    hd.ProductImported = productList.ToArray();

                    // Thêm mặt hàng mới vào List
                    listOfGoodsReceivedNote.Add(hd);
                }
                return listOfGoodsReceivedNote;
            }
        }

        public bool addNewGoodsReceivedNote(string shipperName, string personalId, string shipperAddress, string shipperPhone, int shipfee, List<ProductWithQuantityAndUnitCost> listProductImported)
        {
            List<HoaDonNhapHang> listBill = getGoodsReceivedNote();
            int maxId = listBill.ElementAt(0).Id;

            // Find MAX VALUE OF ID
            foreach (HoaDonNhapHang hd in listBill)
            {
                if (hd.Id > maxId)
                {
                    maxId = hd.Id;
                }
            }

            // Id of new bill
            int newId = maxId + 1;


            // Create new Bill
            HoaDonNhapHang newHd = new HoaDonNhapHang();
            newHd.Id = newId;
            newHd.ShipperName = shipperName;
            newHd.PersonalID = personalId;
            newHd.ShipperAddress = shipperAddress;
            newHd.ShipperPhone = shipperPhone;
            newHd.Currency = "VNĐ";
            newHd.Shipfee = shipfee;
            newHd.ProductImported = listProductImported.ToArray();

            // Add new item to listBill
            listBill.Add(newHd);


            // Convert list to Array and Convert to JSON
            string json = JsonConvert.SerializeObject(listBill.ToArray());

            // Write to file
            try
            {
                string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/HoaDonNhapHangData.json");

                StreamWriter file = new StreamWriter(filePath);
                file.WriteLine(json);
                file.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool deleteGoodsReceivedNote(int BillId)
        {
            List<HoaDonNhapHang> listBill = getGoodsReceivedNote();
            listBill.Remove(listBill.Find(b => b.Id == BillId));

            // Convert list to Array and Convert to JSON
            string json = JsonConvert.SerializeObject(listBill.ToArray());

            // Write to file
            try
            {
                string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/HoaDonNhapHangData.json");

                StreamWriter file = new StreamWriter(filePath);
                file.WriteLine(json);
                file.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool modifyGoodsReceivedNote(int BillId, string shipperName, string personalId, string shipperAddress, string shipperPhone, int shipfee, List<ProductWithQuantityAndUnitCost> listProductImported)
        {
            List<HoaDonNhapHang> listBill = getGoodsReceivedNote();
            // Delete the item with BillId
            listBill.Remove(listBill.Find(b => b.Id == BillId));

            // Create new Bill
            HoaDonNhapHang newHd = new HoaDonNhapHang();
            newHd.Id = BillId;
            newHd.ShipperName = shipperName;
            newHd.PersonalID = personalId;
            newHd.ShipperAddress = shipperAddress;
            newHd.ShipperPhone = shipperPhone;
            newHd.Shipfee = shipfee;
            newHd.Shipfee = shipfee;
            newHd.Currency = "VNĐ";
            newHd.ProductImported = listProductImported.ToArray();

            // Add new item to listBill
            listBill.Add(newHd);


            // Convert list to Array and Convert to JSON
            string json = JsonConvert.SerializeObject(listBill.ToArray());

            // Write to file
            try
            {
                string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/HoaDonNhapHangData.json");

                StreamWriter file = new StreamWriter(filePath);
                file.WriteLine(json);
                file.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}