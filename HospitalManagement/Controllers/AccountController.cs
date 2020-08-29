using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HospitalManagement.Models;

namespace HospitalManagement.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private HmsDb db = new HmsDb();
        // GET: Account
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(Patient model)
        {
            using (var context = new HmsDb())
            {

                bool isValid = context.Patients.Any(x => x.Email == model.Email && x.Password == model.Password);
                if (isValid)
                {
                    Session["email"] = model.Email;
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index", "MasterDetails");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and password");
                }
            }
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Patient model)
        {
            using (var context = new HmsDb())
            {
                context.Patients.Add(model);
                context.SaveChanges();
                ModelState.AddModelError("", "Something Went Wrong!!");
            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin model)
        {
            using (var context = new HmsDb())
            {
                bool isValid = context.Admins.Any(x => x.Email == model.Email && x.Password == model.Password);
                if (isValid)
                {
                    Session["admin"] = model.Email;
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index", "Patients");
                }

                ModelState.AddModelError("", "Invalid username and password");
                return View();
            }
            //return View();
        }

        public ActionResult forgot()
        {
            return View();
        }
        [HttpPost]
        public ActionResult forgot(Patient model)
        {
            using (var context = new HmsDb())
            {
                var a = context.Patients.FirstOrDefault(x => x.Email == model.Email);
                if (a != null)
                {
                    Random random = new Random();
                    int value = random.Next(10000);
                    Session["otp"] = value;
                    Session["forgotmail"] = model.Email;
                    //  ViewBag["forgot"]= model.email;
                    var senderEmail = new MailAddress("drashtimenpara1612@gmail.com", "Drashti Menpara");
                    var receiverEmail = new MailAddress(model.Email, "Receiver");
                    var password = "Mdv@251601";
                    var sub = "test";
                    var body = "Your OTP Is!!" + value;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password),
                        Timeout=20000
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = "Reset OTP",
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return RedirectToAction("forgotone", "Account");
                    // Response.Write("<script>alert('Main Sef')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Email Not Found')</script>");
                }

            }
            return View();
        }
        public ActionResult forgotone()
        {
            return View();
        }
        [HttpPost]
        public ActionResult forgotone(string otp)
        {
            string so = Session["otp"].ToString();
            if (otp != so)
            {
                Response.Write("<script>alert('Not Matched')</script>");
            }
            else
            {
                Response.Write("<script>alert('Matche Found')</script>");
                return RedirectToAction("changepass", "Account");
            }
            return View();
        }

        public ActionResult changepass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult changepass(Patient model)
        {


            string a = Session["forgotmail"].ToString();

            Patient uname = db.Patients.FirstOrDefault(x => x.Email == a);
            if (uname != null)
            {
                uname.Password = model.Password;
                uname.ConfirmPassword = model.ConfirmPassword;
                db.SaveChanges();
                Response.Write("<script>alert('Password Updated Successfully')</script>");
            }

            return View();
        }

    }
}