using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _1988216.MVC.Models;

namespace _1988216.MVC.Controllers
{
    public class C_LoaiHang
    {
        private M_LoaiHang m_loaiHang;

        public C_LoaiHang()
        {
            this.m_loaiHang = new M_LoaiHang();
        }

        public List<LoaiHang> getAllCategory()
        {

            return m_loaiHang.getLoaiHang();
        }

        public bool addNewCategory(string categoryName)
        {
            return m_loaiHang.addNewCategory(categoryName);
        }

        public bool deleteCategory(int categoryId)
        {
            return m_loaiHang.deleteCategory(categoryId);
        }

        public LoaiHang getCategoryById(string categoryId)
        {
            try
            {
                List<LoaiHang> list = m_loaiHang.getLoaiHang();
                LoaiHang res = new LoaiHang();
                foreach (LoaiHang lh in list)
                {
                    if (lh.Id == int.Parse(categoryId))
                    {
                        res = lh;
                        break;
                    }
                }
                return res;
            }
            catch
            {
                return new LoaiHang();
            }
        }

        public bool updateInfoCategory(string categoryId, string categoryName)
        {
            if (categoryId != "")
            {
                return m_loaiHang.updateInfoCategory(int.Parse(categoryId), categoryName);
            }
            else
            {
                return false;
            }
        }


        public List<LoaiHang> searchCategory(string searchType, string keyword)
        {
            try
            {
                List<LoaiHang> resultList = new List<LoaiHang>();
                List<LoaiHang> listCategoryFromDB = m_loaiHang.getLoaiHang();
                switch (searchType)
                {
                    case "stype_maLoaiHang":
                        foreach (LoaiHang lh in listCategoryFromDB)
                        {
                            if (lh.Id == int.Parse(keyword))
                            {
                                resultList.Add(lh);
                                break;
                            }
                        }
                        break;

                    case "stype_tenLoaiHang":
                        foreach (LoaiHang lh in listCategoryFromDB)
                        {
                            if (lh.TenLoaiHang.ToLower().Contains(keyword.ToLower()))
                            {
                                resultList.Add(lh);
                            }
                        }
                        break;
                }
                return resultList;
            }
            catch
            {
                return new List<LoaiHang>();
            }
        }
    }
}