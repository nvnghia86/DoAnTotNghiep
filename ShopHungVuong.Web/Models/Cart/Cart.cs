using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using DataAccess.Models;
using ShopHungVuong.Web.Models;

namespace ShopHungVuong.Web.Models.Cart
{
    
    public class Cart
    {
        DataContext db = new DataContext();
        public int iProductId { get; set; }
        public string Name { get; set; }
        public string MainImage { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public double TotalMoney
        {
            get { return Amount * Price; }
        }
        //Hàm tạo cho giỏ hàng
        public Cart(int ProductId)
        {
            iProductId = ProductId;
            Product product = db.Products.Single(n => n.ProductId == iProductId);
            Name = product.Name;
            MainImage = product.MainPhoto1;
            Price = double.Parse(product.Price.ToString());
            Amount = 1;
        }
    }
}