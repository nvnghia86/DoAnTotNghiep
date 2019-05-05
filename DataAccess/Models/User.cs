using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class User
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
        public string Birthday { get; set; }
        public string CreateDate { get; set; }
        public string ModifiedDate { get; set; }
        public string Avatar { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        //public Guid ActivationCode { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        //public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<ProductWarrantyCard> ProductWarrantyCards { get; set; }


    }
}
