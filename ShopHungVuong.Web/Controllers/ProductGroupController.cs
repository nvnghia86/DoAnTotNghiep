using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Models;
using ShopHungVuong.Web.Models;

namespace ShopHungVuong.Web.Controllers
{
    public class ProductGroupController : Controller
    {
        public DataContext db = new DataContext();
        // GET: ProductGroup
        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<ProductGroupModelView> listManu = db.ProductGroups.Select(x => new ProductGroupModelView { Id = x.ProductGroupId, Name = x.Name }).ToList();
            ViewBag.ProductGroupList = listManu;
            return View();
        }

        [HttpPost]
        public ActionResult Index(ProductGroupModelView model)
        {
            try
            {
                List<ProductGroup> list = db.ProductGroups.ToList();
                if (model.Id > 0)
                {
                    //update
                    ProductGroup conf = db.ProductGroups.SingleOrDefault(x => x.ProductGroupId == model.Id);
                    conf.ProductGroupId = model.Id;
                    conf.Name = model.Name;
                    db.SaveChanges();
                }
                else
                {
                    //Insert
                    ProductGroup manu = new ProductGroup
                    {
                        Name = model.Name,
                    };
                    db.ProductGroups.Add(manu);
                    db.SaveChanges();
                }
                return View(model);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AddEditProductGroup(int Id)
        {
            List<ProductGroup> list = db.ProductGroups.ToList();
            ProductGroupModelView model = new ProductGroupModelView();

            if (Id > 0)
            {

                ProductGroup manu = db.ProductGroups.SingleOrDefault(x => x.ProductGroupId == Id);
                model.Id = manu.ProductGroupId;
                model.Name = manu.Name;
            }
            return PartialView("Detail", model);
        }


        public JsonResult DeleteProductGroup(int Id)
        {
            bool result = false;
            ProductGroup ProductGroup = db.ProductGroups.Find(Id);
            db.ProductGroups.Remove(ProductGroup);
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