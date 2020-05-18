using abc.Models;
using Models.Dao;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace abc.Areas.Admin.Controllers
{
    public class LopController : BaseController
	{
		// GET: Admin/Lop
		[KiemTraQuyen(PermissionName = "DanhSachLop")]
		public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
		{
			var dao = new LopDao();
			var model = dao.ListLop(searchString, page, pageSize);
			ViewBag.SearchString = searchString;
			return View(model);
		}
		[KiemTraQuyen(PermissionName = "DanhSachLop")]
		[HttpGet]
		public ActionResult Create()
		{
			SetViewBag();
			return View();
		}
		[HttpPost]
		public ActionResult Create(Lop lop)
		{
			if (ModelState.IsValid)
			{
				var dao = new LopDao();
				int id = dao.Insert(lop);
				if (id > 0)
				{
					SetAlert("Thêm lớp thành công", "success");
					return RedirectToAction("Index", "Lop");
				}
				else
				{
					ModelState.AddModelError("", "Thêm lóp không thành công");
				}
				SetViewBag();
			}

			return View("Index");
		}
		[KiemTraQuyen(PermissionName = "DanhSachLop")]
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var lop = new LopDao();
			var content = lop.ViewDetail(id);
			SetViewBag(content.DonViID);
			return View(content);
		}

		[HttpPost]
		public ActionResult Edit(Lop lop)
		{
			if (ModelState.IsValid)
			{
				var dao = new LopDao();

				var result = dao.Update(lop);

				if (result)
				{
					SetAlert("Chỉnh sửa lớp thành công", "success");
					return RedirectToAction("Index", "Lop");
				}
				else
				{
					ModelState.AddModelError("", "cập nhật không thành công");
				}
				SetViewBag(lop.DonViID);
			}
			return View("Index");
		}

		[HttpDelete]
		public ActionResult Delete(int id)
		{
			new LopDao().Delete(id);
			return RedirectToAction("Index");
		}

		public void SetViewBag(int? DonViID = null)
		{
			var dao = new DonViDao();
			ViewBag.DonViID = new SelectList(dao.ListAll(), "DonViID", "TenDonVi", DonViID);
		}
	}
}