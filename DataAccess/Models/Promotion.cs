﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Promotion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
        public int PromotionId { get; set; }
        public string Name { get; set; }
        public int SaleOff { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
