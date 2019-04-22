using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ServiceWarrantyCard
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
        public int ServiceWarrantyCardId { get; set; }
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        public int Price { get; set; }
        public string Employee { get; set; }
        public DateTime RepairDate { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("ServiceId")]
        public virtual ICollection<Service> Services { get; set; }
    }
}
