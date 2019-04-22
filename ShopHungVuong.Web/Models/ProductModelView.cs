using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopHungVuong.Web.Models
{
    public class ProductModelView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ImportPrice { get; set; }
        public int Price { get; set; }
        public bool Status { get; set; }
        public string MainPhoto1 { get; set; }
        public string MainPhoto2 { get; set; }
        public string MainPhoto3 { get; set; }
        public string SecondaryPhoto1 { get; set; }
        public string SecondaryPhoto2 { get; set; }
        public string SecondaryPhoto3 { get; set; }
        public string Color { get; set; }
        public int Amount { get; set; }
        public int Point { get; set; }

        public int ProductGroupId { get; set; }
        public string ProductGroupName { get; set; }

        public int GuaranteeProductId { get; set; }
        public int GuaranteeProductTime { get; set; }

        public int PromotionId { get; set; }
        public string PromotionName { get; set; }
        public int PromotionSaleOff { get; set; }

        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
    }
}