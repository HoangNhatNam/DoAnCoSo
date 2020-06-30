using abc.Areas.Admin.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Models.Dao;
using abc.Common;
using abc.Models;
using Models.Framework;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace abc.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
		// GET: Admin/Login
		public ActionResult Index()
        {
            return View();
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				string pass = Request.Form["txtBox1"];
				MailMessage mail = new MailMessage();
				SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

				var login = Comon.Instance.Users.Where(n => n.Email.Equals(model.Email)).FirstOrDefault<User>();
				if(login != null && pass != "")
				{
					try
					{
						Session["SessionID"] = login.UserID.ToString();
						Session["SessionHoTen"] = login.UserName;
						Session["useronline"] = login;
						mail.From = new MailAddress("1710223@dlu.edu.vn");
						mail.To.Add("1710223@dlu.edu.vn");
						mail.Subject = "Verification";
						mail.Body = $"Đăng nhập thành công {DateTime.Now}";
						SmtpServer.Port = 587;
						SmtpServer.Credentials = new System.Net.NetworkCredential(login.Email, pass);
						SmtpServer.EnableSsl = true;
						SmtpServer.Send(mail);
						return RedirectToAction("Index", "Home");
					}
					catch (Exception ex)
					{
						ModelState.AddModelError("", "Đăng nhập không đúng");
						return RedirectToAction("Index", "Login");
					}
				}
				else
				{
					ModelState.AddModelError("", "Đăng nhập không đúng");
					return RedirectToAction("Index", "Login");
				}
			}
			return View(model);
		}
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Login");
		}
	}
}