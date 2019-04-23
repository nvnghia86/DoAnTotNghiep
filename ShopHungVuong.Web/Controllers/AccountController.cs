using DataAccess;
using DataAccess.Models;
using Newtonsoft.Json;
//using ShopHungVuong.CustomAuthentication;
using ShopHungVuong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace ShopHungVuong.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        

        //[HttpGet]
        //public ActionResult Login(string path)
        //{
        //    Models.LoginView model = new Models.LoginView();

        //    HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName.ToString()];
        //    if (authCookie != null)
        //    {
        //        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        //        if (authTicket != null & !authTicket.Expired)
        //        {
        //            model.UserName = authTicket.UserData;
        //        }
        //    }
        //    return PartialView(path, model);
        //}

        //[HttpPost]
        //public ActionResult Login(Models.LoginView loginView, string ReturnUrl = "")
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Membership.ValidateUser(loginView.UserName, loginView.Password))
        //        {
        //            var user = (CustomMembershipUser)Membership.GetUser(loginView.UserName, false);
        //            if (user != null)
        //            {
        //                CustomSerializeModel userModel = new Models.CustomSerializeModel()
        //                {
        //                    UserId = user.UserId,
        //                    FirstName = user.FirstName,
        //                    LastName = user.LastName,
        //                    RoleName = user.Roles.Select(r => r.RoleName).ToList()
        //                };

        //                string userData = JsonConvert.SerializeObject(userModel);
        //                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
        //                    (
        //                    1, loginView.UserName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
        //                    );

        //                string enTicket = FormsAuthentication.Encrypt(authTicket);
        //                HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
        //                Response.Cookies.Add(faCookie);
        //            }

        //            if (Url.IsLocalUrl(ReturnUrl))
        //            {
        //                return Redirect(ReturnUrl);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index","Home");
        //            }
        //        }
        //    }
        //    ModelState.AddModelError("", " Tài khoản hoặc mật khẩu không đúng");
        //    return View(loginView);
        //}

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationView registrationView)
        {
            bool statusRegistration = false;
            string messageRegistration = string.Empty;

            if (ModelState.IsValid)
            {
                // Email Verification
                string userName = Membership.GetUserNameByEmail(registrationView.Email);
                if (!string.IsNullOrEmpty(userName))
                {
                    ModelState.AddModelError("Warning Email", "Sorry: Email already Exists");
                    return View(registrationView);
                }

                //Save User Data 
                using (DataContext dbContext = new DataContext())
                {
                    var user = new DataAccess.Models.User()
                    {
                        Username = registrationView.Username,
                        FirstName = registrationView.FirstName,
                        LastName = registrationView.LastName,
                        Email = registrationView.Email,
                        Password = registrationView.Password,
                    };

                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                }

                //Verification Email
                VerificationEmail(registrationView.Email, registrationView.ActivationCode.ToString());
                messageRegistration = "Your account has been created successfully. ^_^";
                statusRegistration = true;
            }
            else
            {
                messageRegistration = "Something Wrong!";
            }
            ViewBag.Message = messageRegistration;
            ViewBag.Status = statusRegistration;

            return View(registrationView);
        }

        //[HttpGet]
        //public ActionResult ActivationAccount(string id)
        //{
        //    bool statusAccount = false;
        //    using (DataContext dbContext = new DataContext())
        //    {
        //        var userAccount = dbContext.Users.Where(u => u.ActivationCode.ToString().Equals(id)).FirstOrDefault();

        //        if (userAccount != null)
        //        {
        //            userAccount.IsActive = true;
        //            dbContext.SaveChanges();
        //            statusAccount = true;
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Something Wrong !!";
        //        }

        //    }
        //    ViewBag.Status = statusAccount;
        //    return View();
        //}

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("Cookie1", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }

        [NonAction]
        public void VerificationEmail(string email, string activationCode)
        {
            var url = string.Format("/Account/ActivationAccount/{0}", activationCode);
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

            var fromEmail = new MailAddress("nghianv@smartosc.com", "Activation Account - AKKA");
            var toEmail = new MailAddress(email);

            var fromEmailPassword = "******************";
            string subject = "Activation Account !";

            string body = "<br/> Please click on the following link in order to activate your account" + "<br/><a href='" + link + "'> Activation Account ! </a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            })

                smtp.Send(message);

        }
    }
}