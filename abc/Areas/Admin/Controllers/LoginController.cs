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
				//var dao = new UserDao();
				//var result = dao.Login(model.Email);
				//if (result)
				//{
				//	var user = dao.GetById(model.Email);
				//	var userSession = new UserLogin();
				//	userSession.Email = user.Email;
				//	userSession.UserID = user.UserID;
				//	Session.Add(CommonConstant.USER_SESSION, userSession);

				//	return RedirectToAction("Index", "Home");
				//}
				//else
				//{
				//	ModelState.AddModelError("", "Đăng nhập không đúng");
				//}
				var login = Comon.Instance.Users.Where(n => n.Email.Equals(model.Email)).FirstOrDefault<User>();
				if(login != null)
				{
					Session["SessionID"] = login.UserID.ToString();
					Session["SessionHoTen"] = login.UserName;
					Session["useronline"] = login;
					return RedirectToAction("Index", "Home");
				}
				else
				{
					//ModelState.AddModelError("", "Đăng nhập không đúng");
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