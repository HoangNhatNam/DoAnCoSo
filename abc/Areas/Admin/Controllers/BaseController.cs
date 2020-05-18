using abc.Common;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace abc.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var session = (User)Session["useronline"];
			if(session == null)
			{
				filterContext.Result = new RedirectToRouteResult(new
					RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
			}
			base.OnActionExecuting(filterContext);
		}
		protected void SetAlert(string massage, string type)
		{
			TempData["AlertMassage"] = massage;
			if(type == "success")
			{
				TempData["AlertType"] = "alert-success";
			}
			else if(type == "warning")
			{
				TempData["AlertType"] = "alert-warning";
			}
			else if(type == "error")
			{
				TempData["AlertType"] = "alert - danger"; 
			}
		}
	}
}