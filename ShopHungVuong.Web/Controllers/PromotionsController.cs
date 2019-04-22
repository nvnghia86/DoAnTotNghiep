using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Models;
using DataAccess;
using ShopHungVuong.Web.Models;

namespace ShopHungVuong.Web.Controllers
{
    public class PromotionsController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index()
        {
            List<PromotionModelView> listPromotion = db.Promotions.Select(x => new PromotionModelView { Id = x.PromotionId, Name = x.Name, SaleOff = x.SaleOff }).ToList();
            ViewBag.PromotionList = listPromotion;
            return View();
        }

        [HttpPost]
        public ActionResult Index(PromotionModelView model)
        {
            try
            {
                List<Promotion> list = db.Promotions.ToList();
                if (model.Id > 0)
                {
                    //update
                    Promotion promotion = db.Promotions.SingleOrDefault(x => x.PromotionId == model.Id);
                    promotion.PromotionId = model.Id;
                    promotion.Name = model.Name;
                    promotion.SaleOff = model.SaleOff;
                    db.SaveChanges();
                }
                else
                {
                    //Insert
                    Promotion promotion = new Promotion
                    {
                        Name = model.Name,
                        SaleOff = model.SaleOff
                    };
                    db.Promotions.Add(promotion);
                    db.SaveChanges();
                }
                return View(model);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AddEditPromotion(int Id)
        {
            List<Promotion> listPromotion = db.Promotions.ToList();
            PromotionModelView model = new PromotionModelView();

            if (Id > 0)
            {

                Promotion promotion = db.Promotions.SingleOrDefault(x => x.PromotionId == Id);
                model.Id = promotion.PromotionId;
                model.Name = promotion.Name;
                model.SaleOff = promotion.SaleOff;
            }
            return PartialView("Details", model);
        }


        public JsonResult DeletePromotion(int Id)
        {
            bool result = false;
            Promotion Promotion = db.Promotions.Find(Id);
            db.Promotions.Remove(Promotion);
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