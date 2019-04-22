using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccess.Models
{
    public class Ward
    {
        [Key]
        public int WardId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        public string WardName { get; set; }
        public string WardType { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }

    }
}
