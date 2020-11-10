using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _1988216.MVC.Models;

namespace _1988216.MVC.Controllers
{
    public class C_MatHang
    {
        private M_MatHang m_matHang;

        public C_MatHang()
        {
            this.m_matHang = new M_MatHang();
        }
        public List<MatHang> getAllProduct()
        {
            return m_matHang.getMatHang();
        }

        public bool addNewProduct(string tenMatHang, string hanSD, string congTySX, string namSX, int gia, int category)
        {
            return m_matHang.addNewProduct(tenMatHang, hanSD, congTySX, namSX, gia, category);
        }

        public bool deleteProduct(int id)
        {
            return m_matHang.deleteProduct(id);
        }

        public List<MatHang> searchProduct(string searchType, string keyword)
        {
            List<MatHang> resultList = new List<MatHang>();
            List<MatHang> listMatHangfromDB = m_matHang.getMatHang();

            switch (searchType)
            {
                case "stype_maHang":
                    foreach(MatHang mh in listMatHangfromDB)
                    {
                        if(mh.Id == int.Parse(keyword))
                        {
                            resultList.Add(mh);
                            break;
                        }
                    }
                    break;

                case "stype_tenMatHang":
                    foreach (MatHang mh in listMatHangfromDB)
                    {
                        if (mh.TenMatHang.ToLower().Contains(keyword.ToLower()))
                        {
                            resultList.Add(mh);
                        }
                    }
                    break;

                case "stype_hanSD":
                    if(keyword.Trim().Contains(">="))
                    {
                        string timestamp = keyword.Replace(">=", "");
                        DateTime ts = DateTime.Parse(timestamp);
                        foreach (MatHang mh in listMatHangfromDB)
                        {
                            if(DateTime.Parse(mh.HanSD) >= ts)
                            {
                                resultList.Add(mh);
                            }
                        }
                    }

                    if (keyword.Trim().Contains("<="))
                    {
                        string timestamp = keyword.Replace("<=", "");
                        DateTime ts = DateTime.Parse(timestamp);
                        foreach (MatHang mh in listMatHangfromDB)
                        {
                            if (DateTime.Parse(mh.HanSD) <= ts)
                            {
                                resultList.Add(mh);
                            }
                        }
                    }

                    break;
            }

            return resultList;
        }
         
    }         
}