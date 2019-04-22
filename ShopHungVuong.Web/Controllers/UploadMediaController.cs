using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace ShopHungVuong.Web.Controllers
{
    public class UploadMediaController : Controller
    {
        [HttpPost]
        public string UploadFile(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("~/assets/images/manufacturer" + file.FileName));
            return "" + file.FileName;
        }
        public string UploadProduct(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("~/assets/images/product" + file.FileName));
            return "" + file.FileName;
        }
    }
}