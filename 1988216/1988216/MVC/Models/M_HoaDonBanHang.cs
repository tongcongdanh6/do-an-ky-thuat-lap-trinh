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
                    hdbh.Id = jitem["id"];
                    hdbh.CustomerName = jitem["customerName"];
                    hdbh.Dob = jitem["dob"];
                    hdbh.Address = jitem["address"];
                    hdbh.Phone = jitem["phone"];
                    hdbh.BillingAddress = jitem["billing_address"];
                    hdbh.PaymentMethod = jitem["payment_method"];
                    hdbh.Shipfee = jitem["shipfee"];
                    hdbh.Currency = jitem["currency"];
                    hdbh.ExchangeRate = jitem["exchange_rate"];
                    hdbh.VatTax = jitem["vat_tax"];

                    List<ProductWithQuantity> productSold = new List<ProductWithQuantity>();

                    foreach(var item in jitem["products_sold"])
                    {
                        ProductWithQuantity pwt = new ProductWithQuantity();
                        pwt.Id = item["id"];
                        pwt.Quantity = item["quantity"];

                        productSold.Add(pwt);
                    }

                    hdbh.ProductSold = productSold;

                    // Thêm mặt hàng mới vào List
                    listHoaDonBanHang.Add(hdbh);
                }
                return listHoaDonBanHang;
            }
        }
    }
}