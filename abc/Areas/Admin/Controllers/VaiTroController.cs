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
    public class VaiTroController : BaseController
	{
		// GET: Admin/VaiTro
		[KiemTraQuyen(PermissionName = "Admin")]
		public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
		{
			var dao = new VaiTroDao();
			var model = dao.ListAllPaging(searchString, page, pageSize);
			ViewBag.SearchString = searchString;
			return View(model);
		}
		[KiemTraQuyen(PermissionName = "Admin")]
		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Create(VaiTro vaitro)
		{
			if (ModelState.IsValid)
			{
				var dao = new VaiTroDao();
				int id = dao.Insert(vaitro);
				if (id > 0)
				{
					SetAlert("Thêm vai trò thành công", "success");
					return RedirectToAction("Index", "VaiTro");
				}
				else
				{
					ModelState.AddModelError("", "Thêm không thành công");
				}

			}
			return View("Index");
		}
		[KiemTraQuyen(PermissionName = "Admin")]
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var vaitro = new VaiTroDao().ViewDetail(id);
			return View(vaitro);
		}
		[HttpPost]
		public ActionResult Edit(VaiTro vaitro)
		{
			if (ModelState.IsValid)
			{
				var dao = new VaiTroDao();

				var result = dao.Update(vaitro);

				if (result)
				{
					SetAlert("Chỉnh sửa vai trò thành công", "success");
					return RedirectToAction("Index", "VaiTro");
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
			new VaiTroDao().Delete(id);
			return RedirectToAction("Index");
		}
	}
}