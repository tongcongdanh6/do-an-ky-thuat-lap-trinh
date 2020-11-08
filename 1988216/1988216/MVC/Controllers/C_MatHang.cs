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
         
    }         
}