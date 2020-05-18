using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Framework;
using abc.Models;
using System.Web.Security;

namespace abc.Areas.Admin.Controllers
{
    public class DonViController : BaseController
	{
		// GET: Admin/DonVi
		[KiemTraQuyen(PermissionName = "DanhSachDonVi")]
		public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
			var dao = new DonViDao();
			var model = dao.ListAllPaging(searchString, page, pageSize);
			ViewBag.SearchString = searchString;
			return View(model);
        }
		[KiemTraQuyen(PermissionName = "DanhSachDonVi")]
		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Create(DonVi donvi)
		{
			if (ModelState.IsValid)
			{
				var dao = new DonViDao();
				int id = dao.Insert(donvi);
				if (id > 0)
				{
					SetAlert("Thêm đơn vị thành công", "success");
					return RedirectToAction("Index", "DonVi");
				}
				else
				{
					ModelState.AddModelError("", "Thêm đơn vị không thành công");
				}

			}
			return View("Index");
		}
		[KiemTraQuyen(PermissionName = "DanhSachDonVi")]
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var user = new DonViDao().ViewDetail(id);
			return View(user);
		}

		[HttpPost]
		public ActionResult Edit(DonVi donvi)
		{
			if (ModelState.IsValid)
			{
				var dao = new DonViDao();

				var result = dao.Update(donvi);

				if (result)
				{
					SetAlert("Chỉnh sửa đơn vị thành công", "success");
					return RedirectToAction("Index", "DonVi");
				}
				else
				{
					ModelState.AddModelError("", "cập nhật không thành công");
				}

			}
			return View("Index");
		}
		[HttpDelete]
		public ActionResult Delete(int id)
		{
			new DonViDao().Delete(id);
			return RedirectToAction("Index");
		}
	}
}