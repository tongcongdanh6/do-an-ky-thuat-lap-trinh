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

    public class M_LoaiHang
    {
        public List<LoaiHang> getLoaiHang()
        {
            List<LoaiHang> listLoaiHang = new List<LoaiHang>();
            string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/LoaiHangData.json");
            using (StreamReader r = new StreamReader(filePath))
            {
                var json = r.ReadToEnd();
                r.Close();
                dynamic data = JsonConvert.DeserializeObject(json);
                foreach (var jitem in data)
                {
                    LoaiHang lh = new LoaiHang();

                    // Convert từ Json sang Object LoaiHang
                    lh.Id = jitem["Id"];
                    lh.TenLoaiHang = jitem["TenLoaiHang"];

                    // Thêm mặt hàng mới vào List
                    listLoaiHang.Add(lh);
                }

                return listLoaiHang;
            }

        }

        public bool addNewCategory(string tenLoaiHang)
        {
            List<LoaiHang> listLoaiHang = this.getLoaiHang();
            int maxId = listLoaiHang.ElementAt(0).Id;

            // Find MAX VALUE OF ID
            foreach (LoaiHang lh in listLoaiHang)
            {
                if (lh.Id > maxId)
                {
                    maxId = lh.Id;
                }
            }

            // Id of new category
            int newId = maxId + 1;

            // Add new Category to List
            LoaiHang newlh = new LoaiHang(newId, tenLoaiHang);
            listLoaiHang.Add(newlh);


            // Convert list to Array and Convert to JSON
            string json = JsonConvert.SerializeObject(listLoaiHang.ToArray());

            // Write to file
            try
            {
                string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/LoaiHangData.json");

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

        public bool deleteCategory(int categoryId)
        {
            List<LoaiHang> listLoaiHang = this.getLoaiHang();

            foreach (LoaiHang lh in listLoaiHang)
            {
                if (lh.Id == categoryId)
                {
                    listLoaiHang.Remove(lh);
                    break;
                }
            }

            // Convert list to Array and Convert to JSON
            string json = JsonConvert.SerializeObject(listLoaiHang.ToArray());

            // Write to file
            try
            {
                string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/LoaiHangData.json");

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


        public bool updateInfoCategory(int categoryId, string categoryName)
        {
            List<LoaiHang> listLoaiHang = this.getLoaiHang();

            LoaiHang curLh = listLoaiHang.Find(lh => lh.Id == categoryId);
            int idx = listLoaiHang.FindIndex(lh => lh.Id == categoryId);

            curLh.Id = categoryId;
            curLh.TenLoaiHang = categoryName;

            // Delete Old Element with Id = categoryId
            listLoaiHang.RemoveAt(idx);

            // Add curLh with Id = categoryId to the list
            listLoaiHang.Add(curLh);


            // Convert list to Array and Convert to JSON
            string json = JsonConvert.SerializeObject(listLoaiHang.ToArray());

            // Write to file
            try
            {
                string filePath = HttpContext.Current.Server.MapPath("~/MVC/Models/LoaiHangData.json");

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