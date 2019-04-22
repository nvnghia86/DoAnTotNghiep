using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ReceivedDate { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        [ForeignKey("CustomerId")]
        public virtual User User { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
