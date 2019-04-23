using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        // GET: OrderRepairs/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: OrderRepairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderRepair orderRepair = db.OrderRepairs.Find(id);
            db.OrderRepairs.Remove(orderRepair);
            db.SaveChanges();
            return RedirectToAction("Index");
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
