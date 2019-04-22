using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Manufacturer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
