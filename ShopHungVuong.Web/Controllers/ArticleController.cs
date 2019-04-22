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

        // GET: News
        public ActionResult Index()
        {
            List<ArticleModelView> listArticle = db.Articles.Select(x => new ArticleModelView { ArticleId = x.ArticleId, ArticleGroupId = x.ArticleGroupId, Author = x.Author, Detail = x.Detail, Photo = x.Photo, PostDate = x.PostDate, Sumary = x.Sumary, Title = x.Title, ArticleGroupName = x.ArticleGroup.Name }).ToList();
            ViewBag.ArticleList = listArticle;
            return View();
        }

        public ActionResult ListArticle(int? page)
        {

            List<ArticleModelView> listArticle = db.Articles.Select(x => new ArticleModelView { ArticleId = x.ArticleId, ArticleGroupId = x.ArticleGroupId, Author = x.Author, Detail = x.Detail, Photo = x.Photo, PostDate = x.PostDate, Sumary = x.Sumary, Title = x.Title, ArticleGroupName = x.ArticleGroup.Name }).OrderByDescending(x => x.PostDate).ToList();
            ViewBag.ArticleList = listArticle;
            return View();
        }
        //public ViewResult ListArticle(string sortOrder, string currentFilter, string searchString, int? page)
        //{
        //    ViewBag.CurrentSort = sortOrder;
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
        //    if (searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewBag.CurrentFilter = searchString;

        //    var article = from s in db.Articles
        //                   select s;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        article = article.Where(s => s.Title.Contains(searchString)
        //                               || s.Detail.Contains(searchString));
        //    }
        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            article = article.OrderByDescending(s => s.Title);
        //            break;
        //        case "Date":
        //            article = article.OrderBy(s => s.PostDate);
        //            break;
        //        default:  // Name ascending 
        //            article = article.OrderBy(s => s.PostDate);
        //            break;
        //    }

        //    int pageSize = 3;
        //    int pageNumber = (page ?? 1);
        //    return View(article.ToPagedList(pageNumber, pageSize));
        //}
        public ActionResult RecentPost()
        {
            var recentPost = new List<ArticleModelView>();
            recentPost = db.Articles.Select(x => new ArticleModelView { ArticleId = x.ArticleId, ArticleGroupId = x.ArticleGroupId, Author = x.Author, Detail = x.Detail, Photo = x.Photo, PostDate = x.PostDate, Sumary = x.Sumary, Title = x.Title, ArticleGroupName = x.ArticleGroup.Name }).OrderByDescending(x=> x.PostDate).Take(5).ToList();
            ViewBag.RecentPostList = recentPost;
            return View();
        }

        //[HttpPost]
        //public ActionResult Index(ArticleModelView model)
        //{
        //    try
        //    {
        //        List<Article> list = db.Articles.ToList();
        //        if (model.ArticleId > 0)
        //        {
        //            //update
        //            Article article = db.Articles.SingleOrDefault(x => x.ArticleId == model.ArticleId);
        //            article.ArticleId = model.ArticleId;
        //            article.ArticleGroupId = model.ArticleGroupId;
        //            article.Author = model.Author;
        //            article.Title = model.Title;
        //            article.Sumary = model.Sumary;
        //            article.Photo = model.Photo;
        //            article.Detail = model.Detail;
        //            article.PostDate = model.PostDate;
        //            db.SaveChanges();
        //        }
        //        else
        //        {
        //            //Insert
        //            Article article = new Article
        //            {
        //                ArticleGroupId = model.ArticleGroupId,
        //                Author = model.Author,
        //                Title = model.Title,
        //                Sumary = model.Sumary,
        //                Photo = model.Photo,
        //                Detail = model.Detail,
        //                PostDate = model.PostDate
        //            };
        //            db.Articles.Add(article);
        //            db.SaveChanges();
        //        }
        //        return View(model);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        [HttpGet]
        public ActionResult EditArticle(int Id)
        {
            List<Article> list = db.Articles.ToList();
            ArticleModelView model = new ArticleModelView();
            if (Id > 0)
            {
                Article article = db.Articles.SingleOrDefault(x => x.ArticleId == Id);
                model.ArticleId = article.ArticleId;
                model.ArticleGroupId = article.ArticleGroupId;
                model.Author = article.Author;
                model.Title = article.Title;
                model.Sumary = article.Sumary;
                model.Photo = article.Photo;
                model.Detail = article.Detail;
                model.PostDate = article.PostDate;
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult EditArticle()
        {
            return View();
        }

        // GET: News/Details/5
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
        
        public ActionResult Create()
        {
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
        //[ValidateInput(false)]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(int Id)
        //{
        //    List<Article> articles = db.Articles.ToList();
        //    ArticleModelView model = new ArticleModelView();
        //    Article manu = db.Articles.SingleOrDefault(x => x.ArticleId == Id);
        //        model.ArticleGroupId = manu.ArticleGroupId;
        //        model.ArticleId = manu.ArticleId;
        //        model.Author = manu.Author;
        //        model.Detail = manu.Detail;
        //        model.Photo = manu.Photo;
        //        model.PostDate = manu.PostDate;
        //        model.Sumary = manu.Sumary;
        //        model.Title = manu.Title;
        //    return View(model);
        //}
        //GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article  = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArticleGroupId = new SelectList(db.ArticleGroups, "ArticleGroupId", "Name", article.ArticleGroup);
            return View(article);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleId,ArticleGroupId,Author,Title,Sumary,Photo,Detail,PostDate")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArticleGroupId = new SelectList(db.ArticleGroups, "ArticleGroupId", "Name", article.ArticleGroup);
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