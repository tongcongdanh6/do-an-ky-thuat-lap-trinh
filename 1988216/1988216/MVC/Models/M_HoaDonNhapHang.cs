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
                    hd.Id = jitem["id"];
                    hd.ShipperName = jitem["shipperName"];
                    hd.PersonalID = jitem["personalID"];
                    hd.ShipperAddress = jitem["shipperAddress"];
                    hd.ShipperPhone = jitem["shipperPhone"];
                    hd.Currency = jitem["currency"];
                    hd.Shipfee = jitem["shipfee"];

                    List<ProductWithQuantityAndUnitCost> productList = new List<ProductWithQuantityAndUnitCost>();

                    foreach (var item in jitem["products_list"])
                    {
                        ProductWithQuantityAndUnitCost p = new ProductWithQuantityAndUnitCost();
                        p.Id = item["id"];
                        p.Quantity = item["quantity"];
                        p.UnitCost = item["unitCost"];

                        productList.Add(p);
                    }

                    hd.ProductList = productList;

                    // Thêm mặt hàng mới vào List
                    listOfGoodsReceivedNote.Add(hd);
                }
                return listOfGoodsReceivedNote;
            }
        }
    }
}