using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopHungVuong.Web.Models
{
    public class AccountModelView
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Avatar { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
    }
}