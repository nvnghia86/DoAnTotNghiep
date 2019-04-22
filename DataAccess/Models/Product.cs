using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
        public int ProductId { get; set; }
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
        public DateTime CreatedDate { get; set; }
        public string Color { get; set; }
        public int Amount { get; set; }
        public int Point { get; set; }

        public int ProductGroupId { get; set; }
        [ForeignKey("ProductGroupId")]
        public virtual ProductGroup ProductGroup { get; set; }

        public int GuaranteeProductId { get; set; }
        [ForeignKey("GuaranteeProductId")]
        public virtual GuaranteeProduct GuaranteeProduct { get; set; }

        public int PromotionId { get; set; }
        [ForeignKey("PromotionId")]
        public virtual Promotion Promotion { get; set; }

        public int ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer Manufacturer { get; set; }
        

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProductWarrantyCard> ProductWarrantyCards { get; set; }
    }
}
