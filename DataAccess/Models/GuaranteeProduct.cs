using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Models
{
    public class GuaranteeProduct
    {
        public int GuaranteeProductId { get; set; }
        public int Time { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
