using DataAccess;
using DataAccess.Dao;
using DataAccess.Models;
using ShopHungVuong.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ShopHungVuong.Web.Controllers
{
    public class ArticleController : Controller
    {
        private DataContext db = new DataContext();

        //GET: News
        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<ArticleModelView> listArticle = db.Articles.Select(x => new ArticleModelView { ArticleId = x.ArticleId, ArticleGroupId = x.ArticleGroupId, Author = x.Author, Detail = x.Detail, Photo = x.Photo, PostDate = x.PostDate, Sumary = x.Sumary, Title = x.Title, ArticleGroupName = x.ArticleGroup.Name }).ToList();
            ViewBag.ArticleList = listArticle;
            return View();
        }
        public ActionResult ArticleTulanh()
        {
            List<ArticleModelView> listArticle = db.Articles.Select(x => new ArticleModelView { ArticleId = x.ArticleId, ArticleGroupId = x.ArticleGroupId, Author = x.Author, Detail = x.Detail, Photo = x.Photo, PostDate = x.PostDate, Sumary = x.Sumary, Title = x.Title, ArticleGroupName = x.ArticleGroup.Name }).Where(x =>x.ArticleGroupId == 2).ToList();
            ViewBag.ArticleList = listArticle;
            return View();
        }
        public ActionResult ArticleDieuhoa()
        {
            List<ArticleModelView> listArticle = db.Articles.Select(x => new ArticleModelView { ArticleId = x.ArticleId, ArticleGroupId = x.ArticleGroupId, Author = x.Author, Detail = x.Detail, Photo = x.Photo, PostDate = x.PostDate, Sumary = x.Sumary, Title = x.Title, ArticleGroupName = x.ArticleGroup.Name }).Where(x => x.ArticleGroupId == 5).ToList();
            ViewBag.ArticleList = listArticle;
            return View();
        }
        public ActionResult ArticleMaygiat()
        {
            List<ArticleModelView> listArticle = db.Articles.Select(x => new ArticleModelView { ArticleId = x.ArticleId, ArticleGroupId = x.ArticleGroupId, Author = x.Author, Detail = x.Detail, Photo = x.Photo, PostDate = x.PostDate, Sumary = x.Sumary, Title = x.Title, ArticleGroupName = x.ArticleGroup.Name }).Where(x => x.ArticleGroupId == 6).ToList();
            ViewBag.ArticleList = listArticle;
            return View();
        }
        public ActionResult ArticleTivi()
        {
            List<ArticleModelView> listArticle = db.Articles.Select(x => new ArticleModelView { ArticleId = x.ArticleId, ArticleGroupId = x.ArticleGroupId, Author = x.Author, Detail = x.Detail, Photo = x.Photo, PostDate = x.PostDate, Sumary = x.Sumary, Title = x.Title, ArticleGroupName = x.ArticleGroup.Name }).Where(x => x.ArticleGroupId == 1).ToList();
            ViewBag.ArticleList = listArticle;
            return View();
        }
        public ActionResult ListArticle()
        {
            List<ArticleModelView> listArticle = db.Articles.Select(x => new ArticleModelView { ArticleId = x.ArticleId, ArticleGroupId = x.ArticleGroupId, Author = x.Author, Detail = x.Detail, Photo = x.Photo, PostDate = x.PostDate, Sumary = x.Sumary, Title = x.Title, ArticleGroupName = x.ArticleGroup.Name }).OrderByDescending(x => x.PostDate).ToList();
            ViewBag.ArticleList = listArticle;
            return View();
        }
     
        public ActionResult RecentPost()
        {
            var recentPost = new List<ArticleModelView>();
            recentPost = db.Articles.Select(x => new ArticleModelView { ArticleId = x.ArticleId, ArticleGroupId = x.ArticleGroupId, Author = x.Author, Detail = x.Detail, Photo = x.Photo, PostDate = x.PostDate, Sumary = x.Sumary, Title = x.Title, ArticleGroupName = x.ArticleGroup.Name }).OrderByDescending(x=> x.PostDate).Take(5).ToList();
            ViewBag.RecentPostList = recentPost;
            return View();
        }
        
        public ActionResult Create()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ArticleGroupId = new SelectList(db.ArticleGroups, "ArticleGroupId", "Name");
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleId,ArticleGroupId,Author,Title,Sumary,Photo,Detail,PostDate")] Article article)
        {

            if (ModelState.IsValid)
            {

                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArticleGroupId = new SelectList(db.ArticleGroups, "ArticleGroupId", "Name", article.ArticleGroupId);
            return View(article);
        }
       
        public ActionResult Edit(int? id)
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArticleGroupId = new SelectList(db.ArticleGroups, "ArticleGroupId", "Name", article.ArticleGroup);
            return View(article);
        }


        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleId,ArticleGroupId,Author,Title,Sumary,Photo,Detail,PostDate")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArticleGroupId = new SelectList(db.ArticleGroups, "ArticleGroupId", "Name", article.ArticleGroupId);
            return View(article);
        }
        public JsonResult Delete(int Id)
        {
            bool result = false;
            Article article = db.Articles.Find(Id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article @new = db.Articles.Find(id);
            if (@new == null)
            {
                return HttpNotFound();
            }
            return View(@new);
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