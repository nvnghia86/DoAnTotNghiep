using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopHungVuong.Web.Models
{
    public class FeedbackModelView
    {
        public int FeedbackId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public string CreateDate { get; set; }
    }
}