using abc.Common;
using Models.Dao;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace abc.Controllers
{
    public class LichSuController : Controller
    {
		// GET: LichSu
		public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
		{
			User a = CheckAuthorize.Instance.XuatUserID();
			var dao = new HocVuDao();
			var model = dao.ListLichSu(searchString, page, pageSize, a);
			ViewBag.SearchString = searchString;
			return View(model);
		}
	}
}