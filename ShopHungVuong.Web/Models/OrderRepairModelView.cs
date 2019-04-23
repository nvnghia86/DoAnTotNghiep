using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopHungVuong.Web.Models
{
    public class OrderRepairModelView
    {
        [Required]
        public int OrderRepairId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string Content { get; set; }
    }
}