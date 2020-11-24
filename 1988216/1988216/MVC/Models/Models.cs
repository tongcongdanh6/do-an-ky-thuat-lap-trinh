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
        public int Quantity { get; set; }
        public int LoaiHang { get; set; }

    }

    public class ProductWithQuantity
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
    }

    public class ProductWithQuantityAndUnitCost : ProductWithQuantity
    {
        public int UnitCost { get; set; }
    }
   

    public class HoaDonBanHang
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Dob { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string BillingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public int Shipfee { get; set; }
        public string Currency  { get; set; }
        public int ExchangeRate  { get; set; }
        public float VatTax  { get; set; }
        public ProductWithQuantity[] ProductSold { get; set; }

        public float TotalValueOfBill { get; set; }
        public float TotalOfQuantity { get; set; }
    }

    public class HoaDonNhapHang
    {
        public int Id { get; set; }
        public string ShipperName { get; set; }
        public string PersonalID { get; set; }
        public string ShipperAddress { get; set; }
        public string ShipperPhone { get; set; }
        public string Currency { get; set; }
        public int Shipfee { get; set; }
        public List<ProductWithQuantityAndUnitCost> ProductList { get; set; }

        public int TotalValueOfGoodsReceivedNote { get; set; }
        public int TotalQuantityOfProduct { get; set; }

    }

    public class Models
    {

    }
}