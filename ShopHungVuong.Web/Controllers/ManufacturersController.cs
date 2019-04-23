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
    public class ManufacturersController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index()
        {
            List<ManufacturerModelView> listManu = db.Manufacturers.Select(x => new ManufacturerModelView { Id = x.ManufacturerId, Name = x.Name, Logo = x.Logo }).ToList();
            ViewBag.ManufacturerList = listManu;
            return View();
        }

        [HttpPost]
        public ActionResult Index(ManufacturerModelView model)
        {
            try
            {
                List<Manufacturer> list = db.Manufacturers .ToList();
                if (model.Id > 0)
                {
                    //update
                    Manufacturer conf = db.Manufacturers.SingleOrDefault( x => x.ManufacturerId == model.Id);
                    conf.ManufacturerId = model.Id;
                    conf.Name = model.Name;
                    conf.Logo = model.Logo;
                    db.SaveChanges();
                }
                else
                {
                    //Insert
                    Manufacturer manu = new Manufacturer
                    {
                        Name = model.Name,
                        Logo = model.Logo
                    };
                    db.Manufacturers.Add(manu);
                    db.SaveChanges();
                }
                return View(model);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AddEditManufacturer(int Id)
        {
            List<Manufacturer> list = db.Manufacturers.ToList();
            ManufacturerModelView model = new ManufacturerModelView();

            if (Id > 0)
            {
                Manufacturer manu = db.Manufacturers.SingleOrDefault(x => x.ManufacturerId == Id);
                model.Id = manu.ManufacturerId;
                model.Name = manu.Name;
                model.Logo = manu.Logo;
            }
            return PartialView("Details", model);
        }
        

        public JsonResult DeleteManufacturer(int Id)
        {
            bool result = false;
            Manufacturer manufacturer = db.Manufacturers.Find(Id);
            db.Manufacturers.Remove(manufacturer);
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
