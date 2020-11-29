using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace _1988216.MVC.Models
{
    public class M_HoaDonBanHang
    {
        public List<HoaDonBanHang> getHoaDonBanHang()
        {
            List<HoaDonBanHang> listHoaDonBanHang = new List<HoaDonBanHang>();
            string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/HoaDonBanHangData.json");
            using (StreamReader r = new StreamReader(filePath))
            {
                var json = r.ReadToEnd();
                r.Close();
                dynamic data = JsonConvert.DeserializeObject(json);
                foreach (var jitem in data)
                {
                    HoaDonBanHang hdbh = new HoaDonBanHang();

                    // Convert từ Json sang Object MatHang
                    hdbh.Id = jitem["Id"];
                    hdbh.CustomerName = jitem["CustomerName"];
                    hdbh.Dob = jitem["Dob"];
                    hdbh.Address = jitem["Address"];
                    hdbh.Phone = jitem["Phone"];
                    hdbh.BillingAddress = jitem["BillingAddress"];
                    hdbh.PaymentMethod = jitem["PaymentMethod"];
                    hdbh.Shipfee = jitem["Shipfee"];
                    hdbh.Currency = jitem["Currency"];
                    hdbh.ExchangeRate = jitem["ExchangeRate"];
                    hdbh.VatTax = jitem["VatTax"];

                    // Count Product
                    int counter = 0;
                    foreach (var item in jitem["ProductSold"])
                    {
                        counter++;
                    }

                    ProductWithQuantity[] productSoldList = new ProductWithQuantity[counter];

                    int i = 0;
                    foreach (var item in jitem["ProductSold"])
                    {
                        ProductWithQuantity pwt = new ProductWithQuantity();
                        pwt.Id = item["Id"];
                        pwt.Quantity = item["Quantity"];

                        productSoldList[i] = pwt;
                        i++;
                    }

                    hdbh.ProductSold = productSoldList;

                    // Thêm mặt hàng mới vào List
                    listHoaDonBanHang.Add(hdbh);
                }
                return listHoaDonBanHang;
            }
        }

        public bool addNewBillOfSale(string customerName, string dob, string address, string billingAdress, string phone, string paymentMethod, int shipfee, List<ProductWithQuantity> listProductSold)
        {
            List<HoaDonBanHang> listBill = this.getHoaDonBanHang();
            int maxId = listBill.ElementAt(0).Id;

            // Find MAX VALUE OF ID
            foreach (HoaDonBanHang hd in listBill)
            {
                if (hd.Id > maxId)
                {
                    maxId = hd.Id;
                }
            }

            // Id of new bill
            int newId = maxId + 1;


            // Create new Bill
            HoaDonBanHang newHd = new HoaDonBanHang();
            newHd.Id = newId;
            newHd.CustomerName = customerName;
            newHd.Dob = dob;
            newHd.Address = address;
            newHd.Phone = phone;
            newHd.BillingAddress = billingAdress;
            newHd.PaymentMethod = paymentMethod;
            newHd.Shipfee = shipfee;
            newHd.Currency = "VNĐ";
            newHd.ExchangeRate = 1;
            newHd.VatTax = 0.1f;
            newHd.ProductSold = listProductSold.ToArray();

            // Add new item to listBill
            listBill.Add(newHd);


            // Convert list to Array and Convert to JSON
            string json = JsonConvert.SerializeObject(listBill.ToArray());

            // Write to file
            try
            {
                string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/HoaDonBanHangData.json");

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


        public bool modifyBillOfSale(int BillId, string customerName, string dob, string address, string billingAdress, string phone, string paymentMethod, int shipfee, List<ProductWithQuantity> listProductSold)
        {
            List<HoaDonBanHang> listBill = getHoaDonBanHang();
            // Delete the item with BillId
            listBill.Remove(listBill.Find(b => b.Id == BillId));
      
            // Create new Bill
            HoaDonBanHang newHd = new HoaDonBanHang();
            newHd.Id = BillId;
            newHd.CustomerName = customerName;
            newHd.Dob = dob;
            newHd.Address = address;
            newHd.Phone = phone;
            newHd.BillingAddress = billingAdress;
            newHd.PaymentMethod = paymentMethod;
            newHd.Shipfee = shipfee;
            newHd.Currency = "VNĐ";
            newHd.ExchangeRate = 1;
            newHd.VatTax = 0.1f;
            newHd.ProductSold = listProductSold.ToArray();

            // Add new item to listBill
            listBill.Add(newHd);


            // Convert list to Array and Convert to JSON
            string json = JsonConvert.SerializeObject(listBill.ToArray());

            // Write to file
            try
            {
                string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/HoaDonBanHangData.json");

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