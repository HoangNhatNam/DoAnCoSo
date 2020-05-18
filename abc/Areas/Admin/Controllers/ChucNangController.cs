using abc.Models;
using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace abc.Areas.Admin.Controllers
{
    public class ChucNangController : BaseController
    {
		// GET: Admin/ChucNang
		[KiemTraQuyen(PermissionName = "Admin")]
		public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
		{
			var dao = new ChucNangDao();
			var model = dao.ListAllPaging(searchString, page, pageSize);
			ViewBag.SearchString = searchString;
			return View(model);
		}
	}
}