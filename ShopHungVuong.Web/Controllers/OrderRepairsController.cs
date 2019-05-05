using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Models;

namespace ShopHungVuong.Web.Controllers
{
    public class OrderRepairsController : Controller
    {
        private DataContext db = new DataContext();
        
        // GET: OrderRepairs
        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(db.OrderRepairs.ToList());
        }

        // GET: OrderRepairs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderRepair orderRepair = db.OrderRepairs.Find(id);
            if (orderRepair == null)
            {
                return HttpNotFound();
            }
            return View(orderRepair);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderRepair orderRepair = db.OrderRepairs.Find(id);
            if (orderRepair == null)
            {
                return HttpNotFound();
            }
            return View(orderRepair);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderRepairId,CustomerName,PhoneNumber,CustomerAddress,CustomerEmail,OrderDate,Content")] OrderRepair orderRepair)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderRepair).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderRepair);
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            bool result = false;
            OrderRepair item = db.OrderRepairs.Find(Id);
            db.OrderRepairs.Remove(item);
            db.SaveChanges();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
