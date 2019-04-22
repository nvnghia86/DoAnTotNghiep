using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopHungVuong.Web.Models
{
    public class ArticleModelView
    {
        public int ArticleId { get; set; }
        public int ArticleGroupId { get; set; }
        public string ArticleGroupName { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Sumary { get; set; }
        public string Photo { get; set; }
        public string Detail { get; set; }
        public DateTime PostDate { get; set; }
    }
}