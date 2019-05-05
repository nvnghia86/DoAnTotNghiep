using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Models;
namespace ShopHungVuong.Web.Controllers
{
    public class AdminController : Controller
    {
        DataContext db = new DataContext();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult Account()
        {
            User user = db.Users.Find(Session["userID"]);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
    }
}