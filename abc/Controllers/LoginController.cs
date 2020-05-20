using abc.Areas.Admin.Models;
using abc.Models;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace abc.Controllers
{
    public class LoginController : Controller
    {
		// GET: Login
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
				var login = Comon.Instance.Users.Where(n => n.Email.Equals(model.Email)).FirstOrDefault<User>();
				if (login != null)
				{
					Session["SessionID"] = login.UserID.ToString();
					Session["SessionHoTen"] = login.UserName;
					Session["useronline"] = login;
					return RedirectToAction("Index", "Home");
				}
				else
				{
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
