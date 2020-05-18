using abc.Models;
using Models;
using Models.Dao;
using Models.Framework;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace abc.Areas.Admin.Controllers
{
    public class DanhMucController : BaseController
	{
		// GET: Admin/DanhMuc
		[KiemTraQuyen(PermissionName = "DanhSachDanhMuc")]
		public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
			var dao = new DanhMucDao();
			var model = dao.ListDanhMuc(searchString, page, pageSize);
			ViewBag.SearchString = searchString;
			return View(model);
        }
		[KiemTraQuyen(PermissionName = "DanhSachDanhMuc")]
		[HttpGet]
		public ActionResult Create()
		{
			SetViewBag();
			return View();
		}
		[KiemTraQuyen(PermissionName = "DanhSachDanhMuc")]
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var danhmuc = new DanhMucDao();
			var content = danhmuc.GetById(id);
			SetViewBag(content.DonViID);
			return View(content);
		}
		[HttpPost]
		public ActionResult Create(DanhMuc danhmuc)
		{
			if (ModelState.IsValid)
			{
				var dao = new DanhMucDao();
				int id = dao.Insert(danhmuc);
				if (id > 0)
				{
					SetAlert("Thêm danh mục thành công", "success");
					return RedirectToAction("Index", "DanhMuc");
				}
				else
				{
					ModelState.AddModelError("", "Thêm danh mục không thành công");
				}
				SetViewBag();
			}
			return View("Index");
		}
		[HttpPost]
		public ActionResult Edit(DanhMuc danhmuc)
		{
			if (ModelState.IsValid)
			{
				var dao = new DanhMucDao();

				var result = dao.Update(danhmuc);

				if (result)
				{
					SetAlert("Chỉnh sửa danh mục thành công", "success");
					return RedirectToAction("Index", "DanhMuc");
				}
				else
				{
					ModelState.AddModelError("", "cập nhật không thành công");
				}
				SetViewBag(danhmuc.DonViID);
			}
			return View("Index");
		}
		[HttpDelete]
		public ActionResult Delete(int id)
		{
			new DanhMucDao().Delete(id);
			return RedirectToAction("Index");
		}

		public void SetViewBag(int? DonViID = null)
		{
			var dao = new DonViDao();
			ViewBag.DonViID = new SelectList(dao.ListAll(), "DonViID", "TenDonVi", DonViID);
		}
	}
}
