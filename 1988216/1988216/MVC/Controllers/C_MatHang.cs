using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _1988216.MVC.Models;
using _1988216.MVC.Core;

namespace _1988216.MVC.Controllers
{
    public class C_MatHang
    {
        private Lib lib;
        private M_MatHang m_matHang;

        public C_MatHang()
        {
           m_matHang = new M_MatHang();
           lib = new Lib();
        }
        public List<MatHang> getAllProduct()
        {
            return m_matHang.getMatHang();
        }

        public MatHang getProductById(string productId)
        {
            try
            {
                List<MatHang> list = m_matHang.getMatHang();
                MatHang res = new MatHang();
                foreach (MatHang mh in list)
                {
                    if (mh.Id == int.Parse(productId))
                    {
                        res = mh;
                        break;
                    }
                }

                // Modify format Date Time to display to input element in HTML View
                res.HanSD = DateTime.Parse(res.HanSD).ToString("yyyy-MM-dd");
                res.NamSX = DateTime.Parse(res.NamSX).ToString("yyyy-MM-dd");

                return res;
            }
            catch
            {
                return new MatHang();
            }
            
        }

        public bool addNewProduct(string tenMatHang, string hanSD, string congTySX, string namSX, int gia, int category)
        {
            return m_matHang.addNewProduct(tenMatHang, hanSD, congTySX, namSX, gia, category);
        }

        public bool deleteProduct(int id)
        {
            return m_matHang.deleteProduct(id);
        }

        public bool updateInfoProduct(string productId, string tenMatHang, string hanSD, string congTySX, string namSX, int gia, int category)
        {
            if(productId != "")
            {
                return m_matHang.updateInfoProduct(int.Parse(productId), tenMatHang, hanSD, congTySX, namSX, gia, category);
            }
            else
            {
                return false;
            }
            
        }

        public List<MatHang> searchProduct(string searchType, string keyword)
        {
            try
            {
                List<MatHang> resultList = new List<MatHang>();
                List<MatHang> listMatHangfromDB = m_matHang.getMatHang();
                switch (searchType)
                {
                    case "stype_maHang":
                        foreach (MatHang mh in listMatHangfromDB)
                        {
                            if (mh.Id == int.Parse(keyword))
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
                        if (keyword.Trim().Contains(">="))
                        {
                            DateTime ts = lib.convertStringToDateTimeWithOperator(">=", keyword);

                            foreach (MatHang mh in listMatHangfromDB)
                            {
                                if (DateTime.Parse(mh.HanSD) >= ts)
                                {
                                    resultList.Add(mh);
                                }
                            }
                        }

                        if (keyword.Trim().Contains("<="))
                        {
                            DateTime ts = lib.convertStringToDateTimeWithOperator("<=", keyword);
                            foreach (MatHang mh in listMatHangfromDB)
                            {
                                if (DateTime.Parse(mh.HanSD) <= ts)
                                {
                                    resultList.Add(mh);
                                }
                            }
                        }

                        if (keyword.Trim().Contains("=="))
                        {
                            DateTime ts = lib.convertStringToDateTimeWithOperator("==", keyword);
                            foreach (MatHang mh in listMatHangfromDB)
                            {
                                if (DateTime.Parse(mh.HanSD) == ts)
                                {
                                    resultList.Add(mh);
                                }
                            }
                        }

                        break;
                }
                return resultList;
            }
            catch
            {
                return new List<MatHang>();
            }
        }

    }
}