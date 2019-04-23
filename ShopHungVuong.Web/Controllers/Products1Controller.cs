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
using ShopHungVuong.Web.Models;

namespace ShopHungVuong.Web.Controllers
{
    public class Products1Controller : Controller
    {
        private DataContext db = new DataContext();

        // GET: Products1
        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var products = db.Products.Include(p => p.GuaranteeProduct).Include(p => p.Manufacturer).Include(p => p.ProductGroup).Include(p => p.Promotion);
            return View(products.ToList());
        }

        // GET: Products1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products1/Create
        public ActionResult Create()
        {
            ViewBag.GuaranteeProductId = new SelectList(db.GuaranteeProducts, "GuaranteeProductId", "Description");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "ManufacturerId", "Name");
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "ProductGroupId", "Name");
            ViewBag.PromotionId = new SelectList(db.Promotions, "PromotionId", "Name");
            return View();
        }

        // POST: Products1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,Description,ImportPrice,Price,Status,MainPhoto1,MainPhoto2,MainPhoto3,SecondaryPhoto1,SecondaryPhoto2,SecondaryPhoto3,CreatedDate,Color,Amount,Point,ProductGroupId,GuaranteeProductId,PromotionId,ManufacturerId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GuaranteeProductId = new SelectList(db.GuaranteeProducts, "GuaranteeProductId", "Description", product.GuaranteeProductId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "ManufacturerId", "Name", product.ManufacturerId);
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "ProductGroupId", "Name", product.ProductGroupId);
            ViewBag.PromotionId = new SelectList(db.Promotions, "PromotionId", "Name", product.PromotionId);
            return View(product);
        }

        // GET: Products1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.GuaranteeProductId = new SelectList(db.GuaranteeProducts, "GuaranteeProductId", "Description", product.GuaranteeProductId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "ManufacturerId", "Name", product.ManufacturerId);
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "ProductGroupId", "Name", product.ProductGroupId);
            ViewBag.PromotionId = new SelectList(db.Promotions, "PromotionId", "Name", product.PromotionId);
            return View(product);
        }

        //// POST: Products1/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ProductId,ProductGroupId,ManufacturerId,Name,Description,ImportPrice,Price,Status,PromotionId,Amount,Point,GuaranteeProductId,MainPhoto1,MainPhoto2,MainPhoto3,SecondaryPhoto1,SecondaryPhoto2,SecondaryPhoto3,Color,CreatedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GuaranteeProductId = new SelectList(db.GuaranteeProducts, "GuaranteeProductId", "Description", product.GuaranteeProductId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "ManufacturerId", "Name", product.ManufacturerId);
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "ProductGroupId", "Name", product.ProductGroupId);
            ViewBag.PromotionId = new SelectList(db.Promotions, "PromotionId", "Name", product.PromotionId);
            return View(product);
        }

        // GET: Products1/Delete/5
        [HttpPost]
        public JsonResult DeleteProduct(int Id)
        {
            bool result = false;
            Product product = db.Products.Find(Id);
            db.Products.Remove(product);
            db.SaveChanges();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: Products1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
