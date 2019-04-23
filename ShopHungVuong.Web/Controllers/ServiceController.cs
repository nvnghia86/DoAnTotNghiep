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
    public class ServiceController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<ServiceModelView> listService = db.Services.Select(x => new ServiceModelView { Id = x.ServicerId, ServiceName = x.ServiceName, Description = x.Description, Price = x.Price, WarrantyPeriod = x.WarrantyPeriod }).ToList();
            ViewBag.ServiceList = listService;
            return View();
        }

        [HttpPost]
        public ActionResult Index(ServiceModelView model)
        {
            try
            {
                List<Service> list = db.Services.ToList();
                if (model.Id > 0)
                {
                    //update
                    Service service = db.Services.SingleOrDefault(x => x.ServicerId == model.Id);
                    service.ServicerId = model.Id;
                    service.ServiceName = model.ServiceName;
                    service.WarrantyPeriod = model.WarrantyPeriod;
                    service.Price = model.Price;
                    service.Description = model.Description;
                    db.SaveChanges();
                }
                else
                {
                    //Insert
                    Service service = new Service
                    {
                        Description = model.Description,
                        Price = model.Price,
                        WarrantyPeriod = model.WarrantyPeriod,
                        ServiceName = model.ServiceName,

                    };
                    db.Services.Add(service);
                    db.SaveChanges();
                }
                return View(model);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AddEditService(int Id)
        {
            List<Service> services = db.Services.ToList();
            ServiceModelView model = new ServiceModelView();

            if (Id > 0)
            {

                Service service = db.Services.SingleOrDefault(x => x.ServicerId == Id);
                model.Id = service.ServicerId;
                model.ServiceName = service.ServiceName;
                model.Description = service.Description;
                model.Price = service.Price;
                model.WarrantyPeriod = service.WarrantyPeriod;
            }
            return PartialView("Details", model);
        }


        public JsonResult DeleteService(int Id)
        {
            bool result = false;
            Service Service = db.Services.Find(Id);
            db.Services.Remove(Service);
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