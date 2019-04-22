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
    public class ArticleGroupsController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index()
        {
            List<ArticleGroupModelView> listManu = db.ArticleGroups.Select(x => new ArticleGroupModelView { Id = x.ArticleGroupId, Name = x.Name, Description = x.Description }).ToList();
            ViewBag.ArticleGroupList = listManu;
            return View();
        }

        [HttpPost]
        public ActionResult Index(ArticleGroupModelView model)
        {
            try
            {
                List<ArticleGroup> list = db.ArticleGroups.ToList();
                if (model.Id > 0)
                {
                    //update
                    ArticleGroup conf = db.ArticleGroups.SingleOrDefault(x => x.ArticleGroupId == model.Id);
                    conf.ArticleGroupId = model.Id;
                    conf.Name = model.Name;
                    conf.Description = model.Description;
                    db.SaveChanges();
                }
                else
                {
                    //Insert
                    ArticleGroup manu = new ArticleGroup
                    {
                        Name = model.Name,
                        Description = model.Description
                    };
                    db.ArticleGroups.Add(manu);
                    db.SaveChanges();
                }
                return View(model);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AddEditArticleGroup(int Id)
        {
            List<ArticleGroup> list = db.ArticleGroups.ToList();
            ArticleGroupModelView model = new ArticleGroupModelView();

            if (Id > 0)
            {

                ArticleGroup manu = db.ArticleGroups.SingleOrDefault(x => x.ArticleGroupId == Id);
                model.Id = manu.ArticleGroupId;
                model.Name = manu.Name;
                model.Description = manu.Description;
            }
            return PartialView("Partial2", model);
        }


        public JsonResult DeleteArticleGroup(int Id)
        {
            bool result = false;
            ArticleGroup ArticleGroup = db.ArticleGroups.Find(Id);
            db.ArticleGroups.Remove(ArticleGroup);
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
