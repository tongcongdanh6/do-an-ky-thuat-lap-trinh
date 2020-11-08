using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1988216.MVC.Models
{

    public class LoaiHang
    {
        public LoaiHang()
        {
            this.Id = 0;
            this.TenLoaiHang = String.Empty;
        }
        public LoaiHang(int id, string tenlh)
        {
            this.Id = id;
            this.TenLoaiHang = tenlh;
        }
        public int Id { get; set; }
        public string TenLoaiHang { get; set; }

    }
    public class MatHang
    {
        public int Id { get; set; }
        public string TenMatHang { get; set; }
        public string HanSD { get; set; }
        public string CongTySX { get; set; }
        public string NamSX { get; set; }
        public int Gia { get; set; }
        public int LoaiHang { get; set; }

    }
    public class Models
    {

    }
}