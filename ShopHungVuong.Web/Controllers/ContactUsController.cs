using DataAccess;
using DataAccess.Models;
using ShopHungVuong.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopHungVuong.Web.Controllers
{
    public class ContactUsController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FeedbackModelView model)
        {
            try
            {
                //Insert
                Feedback item = new Feedback
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Content = model.Content,
                    CreateDate = DateTime.UtcNow.ToString("dd-MMMM-yyyy")
                };
                db.Feedbacks.Add(item);
                db.SaveChanges();
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}