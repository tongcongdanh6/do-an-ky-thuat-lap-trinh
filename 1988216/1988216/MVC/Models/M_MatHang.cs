using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using System.Linq;
using System.Web;


namespace _1988216.MVC.Models
{

    public class M_MatHang
    {
        public List<MatHang> getMatHang()
        {
            List<MatHang> listMatHang = new List<MatHang>();
            string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/MatHangData.json");
            using (StreamReader r = new StreamReader(filePath))
            {
                var json = r.ReadToEnd();
                r.Close();
                dynamic data = JsonConvert.DeserializeObject(json);
                foreach (var jitem in data)
                {
                    MatHang mh = new MatHang();

                    // Convert từ Json sang Object MatHang
                    mh.Id = jitem["Id"];
                    mh.TenMatHang = jitem["TenMatHang"];
                    mh.HanSD = jitem["HanSD"];
                    mh.CongTySX = jitem["CongTySX"];
                    mh.NamSX = jitem["NamSX"];
                    mh.Gia = jitem["Gia"];

                    // Quantity Available Of Product will get from HoaDonNhapHangData - HoaDonBanHang
                    M_HoaDonNhapHang m_HoaDonNhapHang = new M_HoaDonNhapHang();
                    M_HoaDonBanHang m_HoaDonBanHang = new M_HoaDonBanHang();
                    List<HoaDonNhapHang> listNhapHang = m_HoaDonNhapHang.getGoodsReceivedNote();
                    List<HoaDonBanHang> listBanHang = m_HoaDonBanHang.getHoaDonBanHang();

                    int totalQuantityOfAProduct = 0;
                    // SO LUONG NHAP HANG (+)
                    foreach (HoaDonNhapHang hd in listNhapHang)
                    {
                        if (hd.ProductList.Find(hdInList => hdInList.Id == mh.Id) != null)
                        {
                            totalQuantityOfAProduct += hd.ProductList.Find(hdInList => hdInList.Id == mh.Id).Quantity;
                        }
                    }

                    // SO LUONG DA BAN (-)

                    foreach (HoaDonBanHang hd in listBanHang)
                    {
                        if (hd.ProductSold.Find(hdInList => hdInList.Id == mh.Id) != null)
                        {
                            totalQuantityOfAProduct -= hd.ProductSold.Find(hdInList => hdInList.Id == mh.Id).Quantity;
                        }
                    }


                    mh.Quantity = totalQuantityOfAProduct;



                    mh.LoaiHang = jitem["LoaiHang"];

                    // Thêm mặt hàng mới vào List
                    listMatHang.Add(mh);
                }
                return listMatHang;
            }
        }

        public bool addNewProduct(string tenMatHang, string hanSD, string congTySX, string namSX, int gia, int category)
        {
            List<MatHang> listMatHang = this.getMatHang();
            int maxId = listMatHang.ElementAt(0).Id;

            // Find MAX VALUE OF ID
            foreach (MatHang mh in listMatHang)
            {
                if (mh.Id > maxId)
                {
                    maxId = mh.Id;
                }
            }

            // Id of new product
            int newId = maxId + 1;

            // Add new Product to List
            MatHang newMh = new MatHang();
            newMh.Id = newId;
            newMh.TenMatHang = tenMatHang;
            newMh.HanSD = hanSD;
            newMh.CongTySX = congTySX;
            newMh.NamSX = namSX;
            newMh.Gia = gia;
            newMh.LoaiHang = category;

            listMatHang.Add(newMh);


            // Convert list to Array and Convert to JSON
            string json = JsonConvert.SerializeObject(listMatHang.ToArray());

            // Write to file
            try
            {
                string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/MatHangData.json");

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

        public bool updateInfoProduct(int productId, string tenMatHang, string hanSD, string congTySX, string namSX, int gia, int category)
        {
            List<MatHang> listMatHang = this.getMatHang();


            MatHang curMh = listMatHang.Find(mh => mh.Id == productId);
            int idx = listMatHang.FindIndex(mh => mh.Id == productId);


            curMh.TenMatHang = tenMatHang;
            curMh.HanSD = hanSD;
            curMh.CongTySX = congTySX;
            curMh.NamSX = namSX;
            curMh.Gia = gia;
            curMh.LoaiHang = category;

            // Delete Old Element with Id = productId
            listMatHang.RemoveAt(idx);

            // Add curMh with Id = productId to the list
            listMatHang.Add(curMh);


            // Convert list to Array and Convert to JSON
            string json = JsonConvert.SerializeObject(listMatHang.ToArray());

            // Write to file
            try
            {
                string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/MatHangData.json");

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

        public bool deleteProduct(int id)
        {
            List<MatHang> listMatHang = this.getMatHang();

            foreach (MatHang mh in listMatHang)
            {
                if (mh.Id == id)
                {
                    listMatHang.Remove(mh);
                    break;
                }
            }

            // Convert list to Array and Convert to JSON
            string json = JsonConvert.SerializeObject(listMatHang.ToArray());

            // Write to file
            try
            {
                string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/MatHangData.json");

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