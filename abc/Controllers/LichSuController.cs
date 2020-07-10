using abc.Common;
using Models.Dao;
using Models.Framework;
using Models.ViewModel;
using PagedList;
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
		DoAnDbContext db = new DoAnDbContext();
		public ActionResult Index(string searchString, int page = 1, int pageSize = 100)
		{
			User a = CheckAuthorize.Instance.XuatUserID();
			var dao = new HocVuDao();
			var model = dao.ListLichSu(searchString, page, pageSize, a);
			ViewBag.SearchString = searchString;
			List<HocVuViewModel> list = new List<HocVuViewModel>();
			using(DoAnDbContext db = new DoAnDbContext())
			{
				list = list.OrderBy(x => x.HocVuID).ToList();
			}
			return View(model);
		}
		[HttpGet]
		public ActionResult Chat(int id)
		{
			var hocvu = new HocVuDao();
			var content = hocvu.ViewDetail(id);
			return View(content);
		}
		[HttpPost]
		public ActionResult Chat(HocVu hocvu)
		{
			User a = CheckAuthorize.Instance.XuatUserID();
			if (ModelState.IsValid)
			{
				var dao = new HocVuDao();
				int id = dao.Insert2(hocvu, a, hocvu.HocVuID);
				if (id > 0)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Thêm không thành công");
				}
				
			}
			return View("Index");
		}

	}
}