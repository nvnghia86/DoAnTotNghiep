using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Models;
using PagedList;
using ShopHungVuong.Web.Models;
using ShopHungVuong.Web.Controllers;
using System.Configuration;
using System.Net.Mail;
using System.Text;

namespace ShopHungVuong.Web.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/assets/images/product"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            // after successfully uploading redirect the user
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MainHeader()
        {
            return View();
        }
       
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult PageBanner()
        {
            return View();
        }
        public ActionResult Blog(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var article = from s in db.Articles
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                article = article.Where(s => s.Title.Contains(searchString)
                                       || s.Detail.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    article = article.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    article = article.OrderBy(s => s.PostDate);
                    break;
                default:  // Name ascending 
                    article = article.OrderBy(s => s.PostDate);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(article.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Faq()
        {
            return View();
        }
        
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Blogcomment(int? id)
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
        
        public ActionResult Policy()
        {
            return View();
        }
        public ActionResult ArticleDetail(int? id)
        {
            return View();
        }
        public ActionResult HeroBanner()
        {
            List<ProductModelView> listProduct =
                db.Products.Select(x => new ProductModelView
                {
                    Id = x.ProductId,
                    Name = x.Name,
                    Description = x.Description,
                    ImportPrice = x.ImportPrice,
                    Price = x.Price,
                    Status = x.Status,
                    MainPhoto1 = x.MainPhoto1,
                    SecondaryPhoto1 = x.SecondaryPhoto1,
                    Amount = x.Amount,
                    Point = x.Point,
                    ProductGroupId = x.ProductGroupId,
                    ProductGroupName = x.ProductGroup.Name,
                    GuaranteeProductId = x.GuaranteeProductId,
                    GuaranteeProductTime = x.GuaranteeProduct.Time,
                    PromotionId = x.PromotionId,
                    PromotionName = x.Promotion.Name,
                    PromotionSaleOff = x.Promotion.SaleOff,
                    ManufacturerId = x.ManufacturerId,
                    ManufacturerName = x.Manufacturer.Name
                }).Where(x => x.PromotionSaleOff >= 35).Take(3).ToList();
            ViewBag.HeroBanner = listProduct;
            return View();
        }
        public void SendEmail(string email)
        {

            try
            {

                StringBuilder Body = new StringBuilder();
                Body.Append("<p>Hi pro: </ p > ");
                Body.Append("<table>");
                Body.Append("<tr><td colspan='2'><h4>Notification :</h4></td></tr>");
                Body.Append("<tr><td>Admin </td></tr>");
                Body.Append("<tr><td>YourTask has been updated </td></tr>");
                Body.Append("<tr><td>Please check again</td></tr>");
                Body.Append("<tr><td>Please feel free to contact <b> sondh@smartosc.com<b> in case you remain any questions.</td></tr>");
                Body.Append("<tr><td>Thank you for watching our newsletter </td></tr>");
                Body.Append("</table>");

                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("nghiakho97@gmail.com");
                mail.Subject = "Updating Task";
                mail.Body = Body.ToString();
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("nghiakho97@gmail.com", "nguyenvannghia");
                smtp.EnableSsl = true;
                smtp.Send(mail);

            }
            catch (Exception ex)
            {
                Response.Write("Exception in sendEmail:" + ex.Message);
            }

        }
        public ActionResult MainFooter()
        {
            return View();
        }

        //POST: OrderRepairs/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult OrderRepair()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MainHeader(OrderRepairModelView model)
        {
            try
            {
                OrderRepair orderRepair = new OrderRepair
                {
                    Content = model.Content,
                    CustomerAddress = model.CustomerAddress,
                    CustomerEmail = model.CustomerEmail,
                    CustomerName = model.CustomerName,
                    OrderDate = model.OrderDate,
                    OrderRepairId = model.OrderRepairId,
                    PhoneNumber = model.PhoneNumber
                };
                db.OrderRepairs.Add(orderRepair);
                db.SaveChanges();
                return View(model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult AddOrderRepair()
        {
            ManufacturerModelView model = new ManufacturerModelView();
            return PartialView("OrderRepair", model);
        }
        //[HttpPost]
        //public JsonResult OrderRepair(OrderRepair orderRepairData)
        //{
        //    OrderRepair orderRepair = new OrderRepair
        //    {
        //        Content = orderRepairData.Content,
        //        CustomerAddress = orderRepairData.CustomerAddress,
        //        CustomerEmail = orderRepairData.CustomerEmail,
        //        CustomerName = orderRepairData.CustomerName,
        //        OrderDate = orderRepairData.OrderDate,
        //        PhoneNumber = orderRepairData.PhoneNumber
        //    };
        //    return Json(String.Format("'Success': 'true'"));
        //}

       
        [HttpPost]
        public ActionResult Login(User userModel)
        {
            using (db)
            {
                var userDetails = db.Users.Where(x => x.Username == userModel.Username && x.Password == userModel.Password && x.IsActive == true).FirstOrDefault();

                if (userDetails == null)
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng.");
                    return View();
                }
                else if(userDetails.IsActive == false)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa ");
                    return View();
                }
                else
                {
                    Session["userID"] = userDetails.UserId;
                    Session["userName"] = userDetails.Username;
                    Session["firsName"] = userDetails.FirstName;
                    Session["lastName"] = userDetails.LastName;
                    Session["address"] = userDetails.Address;
                    Session["role"] = userDetails.RoleId;
                    if (userDetails.Avatar == null)
                    {
                        Session["avatar"] = "/Assets/Images/noimage.JPG";
                    }
                    else
                    {
                        Session["avatar"] = userDetails.Avatar;
                    }
                    Session["email"] = userDetails.Email;
                    if(userDetails.RoleId == 3)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
            }
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