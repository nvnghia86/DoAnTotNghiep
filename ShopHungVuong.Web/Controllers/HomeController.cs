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
        public ActionResult Blog()
        {
            List<ArticleModelView> listArticle = db.Articles.Select(x => new ArticleModelView { ArticleId = x.ArticleId, ArticleGroupId = x.ArticleGroupId, Author = x.Author, Detail = x.Detail, Photo = x.Photo, PostDate = x.PostDate, Sumary = x.Sumary, Title = x.Title, ArticleGroupName = x.ArticleGroup.Name }).OrderByDescending(x => x.PostDate).ToList();
            ViewBag.ArticleList = listArticle;
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
        
        public ActionResult MainFooter()
        {
            return View();
        }
        public void SendEmail(string email, string userName, string password)
        {

            try
            {
                StringBuilder Body = new StringBuilder();
                Body.Append("<p>Kính chào quí khách </ p > ");
                Body.Append("<table>");
                Body.Append("<tr><td colspan='2'><h4>Bạn đã đặt lịch thành công :</h4></td></tr>");
                Body.Append("<tr><td>Tài khoản của bạn đã được tạo tự động </td></tr>");
                Body.Append("<tr><td>Tài khoản của bạn là: "+ userName + " </td></tr>");
                Body.Append("<tr><td>Mật khẩu là: " + password + "</td></tr>");
                Body.Append("<tr><td></td></tr>");
                Body.Append("<tr><td>Cảm ơn bạn đã tin tưởng sử dụng dịch vụ của chúng tôi.</td></tr>");
                Body.Append("</table>");

                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("nghiakho97@gmail.com");
                mail.Subject = "Đặt lịch thành công !!";
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
        public void SendEmailUser(string email)
        {

            try
            {
                StringBuilder Body = new StringBuilder();
                Body.Append("<p>Kính chào quí khách </ p > ");
                Body.Append("<table>");
                Body.Append("<tr><td colspan='2'><h4>Bạn đã đặt lịch thành công :</h4></td></tr>");
                Body.Append("<tr><td></td></tr>");
                Body.Append("<tr><td>Cảm ơn bạn đã tin tưởng sử dụng dịch vụ của chúng tôi.</td></tr>");
                Body.Append("</table>");
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("nghiakho97@gmail.com");
                mail.Subject = "Đặt lịch thành công !!";
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
            if(Session["userID"] == null)
            {
                try
                {
                    User user = new User
                    {
                        RoleId = 3,
                        Username = model.PhoneNumber,
                        Password = "123456",
                        FirstName = model.CustomerName,
                        LastName = model.CustomerName,
                        Address = model.CustomerAddress,
                        Email = model.CustomerEmail,
                        Phone = Int32.Parse(model.PhoneNumber),
                        CreateDate = DateTime.Now.ToString("dd-MM-yyyy"),
                        IsActive = true
                    };
                    db.Users.Add(user);
                    OrderRepair orderRepair = new OrderRepair
                    {
                        Content = model.Content,
                        CustomerAddress = model.CustomerAddress,
                        CustomerEmail = model.CustomerEmail,
                        CustomerName = model.CustomerName,
                        Device = model.Device,
                        OrderDate = model.OrderDate,
                        OrderRepairId = model.OrderRepairId,
                        PhoneNumber = model.PhoneNumber
                    };
                    db.OrderRepairs.Add(orderRepair);
                    db.SaveChanges();
                    SendEmail(model.CustomerEmail, model.PhoneNumber, "123456");
                    return View(model);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                try
                {
                    OrderRepair orderRepair = new OrderRepair
                    {
                        Content = model.Content,
                        CustomerAddress = model.CustomerAddress,
                        CustomerEmail = model.CustomerEmail,
                        CustomerName = model.CustomerName,
                        Device = model.Device,
                        OrderDate = model.OrderDate,
                        OrderRepairId = model.OrderRepairId,
                        PhoneNumber = model.PhoneNumber
                    };
                    db.OrderRepairs.Add(orderRepair);
                    db.SaveChanges();
                    SendEmailUser(model.CustomerEmail);
                    return View(model);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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
        public ActionResult Account()
        {
            return View();
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