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

        public bool addNewCategory(string tenLoaiHang)
        {
            return m_loaiHang.addNewCategory(tenLoaiHang);
            
        }
    }
}