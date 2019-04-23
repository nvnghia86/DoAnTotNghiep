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
    public class GuaranteeProductController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<GuaranteeProductModelView> listManu = db.GuaranteeProducts.Select(x => new GuaranteeProductModelView { Id = x.GuaranteeProductId, Time = x.Time, Description = x.Description, Status = x.Status }).ToList();
            ViewBag.GuaranteeProductList = listManu;
            return View();
        }

        [HttpPost]
        public ActionResult Index(GuaranteeProductModelView model)
        {
            try
            {
                List<GuaranteeProduct> list = db.GuaranteeProducts.ToList();
                if (model.Id > 0)
                {
                    //update
                    GuaranteeProduct guaranteeProduct = db.GuaranteeProducts.SingleOrDefault(x => x.GuaranteeProductId == model.Id);
                    guaranteeProduct.GuaranteeProductId = model.Id;
                    guaranteeProduct.Status = model.Status;
                    guaranteeProduct.Description = model.Description;
                    guaranteeProduct.Time = model.Time;
                    db.SaveChanges();
                }
                else
                {
                    //Insert
                    GuaranteeProduct guaranteeProduct = new GuaranteeProduct
                    {
                    GuaranteeProductId = model.Id,
                    Status = model.Status,
                    Description = model.Description,
                    Time = model.Time
                };
                    db.GuaranteeProducts.Add(guaranteeProduct);
                    db.SaveChanges();
                }
                return View(model);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AddEditGuaranteeProduct(int Id)
        {
            List<GuaranteeProduct> list = db.GuaranteeProducts.ToList();
            GuaranteeProductModelView model = new GuaranteeProductModelView();
            
            if (Id > 0)
            {
                GuaranteeProduct guaranteeProduct = db.GuaranteeProducts.SingleOrDefault(x => x.GuaranteeProductId == Id);
                model.Id = guaranteeProduct.GuaranteeProductId;
                model.Status = guaranteeProduct.Status;
                model.Description = guaranteeProduct.Description;
                model.Time = guaranteeProduct.Time;
            }
            return PartialView("Details", model);
        }


        public JsonResult DeleteGuaranteeProduct(int Id)
        {
            bool result = false;
            GuaranteeProduct GuaranteeProduct = db.GuaranteeProducts.Find(Id);
            db.GuaranteeProducts.Remove(GuaranteeProduct);
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