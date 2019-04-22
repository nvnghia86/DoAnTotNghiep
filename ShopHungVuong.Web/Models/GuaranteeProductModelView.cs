using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopHungVuong.Web.Models
{
    public class GuaranteeProductModelView
    {
        public int Id { get; set; }
        public int Time { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}