using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Article
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
        public int ArticleId { get; set; }
        public int ArticleGroupId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Sumary { get; set; }
        public string Photo { get; set; }
        public string Detail { get; set; }
        public DateTime PostDate { get; set; }
        public int? ViewCount { get; set; }

        public string Tags { get; set; }
        [ForeignKey("ArticleGroupId")]
        public virtual ArticleGroup ArticleGroup { get; set; }
        
    }
}
