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
    public class HocVuController : BaseController
	{
		// GET: Admin/HocVu
		[KiemTraQuyen(PermissionName = "DanhSachHocVu")]
		public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
		{
			var dao = new HocVuDao();
			var model = dao.ListHocVu(searchString, page, pageSize);
			ViewBag.SearchString = searchString;
			return View(model);
		}
		[KiemTraQuyen(PermissionName = "DanhSachHocVu")]
		[HttpGet]
		public ActionResult Create()
		{
			SetViewBag();
			SetViewBagDanhMuc();
			SetViewBagUser();
			return View();
		}
		[HttpPost]
		public ActionResult Create(HocVu hocvu)
		{
			if (ModelState.IsValid)
			{
				var dao = new HocVuDao();
				int id = dao.Insert(hocvu);
				if (id > 0)
				{
					SetAlert("Thêm học vụ thành công", "success");
					return RedirectToAction("Index", "HocVu");
				}
				else
				{
					ModelState.AddModelError("", "Thêm học vụ không thành công");
				}
				SetViewBag();
				SetViewBagDanhMuc();
				SetViewBagUser();
			}
			return View("Index");
		}
		[KiemTraQuyen(PermissionName = "DanhSachHocVu")]
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var hocvu = new HocVuDao();
			var content = hocvu.ViewDetail(id);
			SetViewBag(content.DonViID);
			SetViewBagDanhMuc(content.DanhMucID);
			SetViewBagUser(content.UserID);
			return View(content);
		}
		[HttpPost]
		public ActionResult Edit(HocVu hocvu)
		{
			if (ModelState.IsValid)
			{
				var dao = new HocVuDao();

				var result = dao.Update(hocvu);

				if (result)
				{
					SetAlert("Sửa học vụ thành công", "success");
					return RedirectToAction("Index", "HocVu");
				}
				else
				{
					ModelState.AddModelError("", "cập nhật không thành công");
				}
				SetViewBag(hocvu.DonViID);
				SetViewBagDanhMuc(hocvu.DanhMucID);
				SetViewBagUser(hocvu.UserID);
			}
			return View("Index");
		}
		[HttpDelete]
		public ActionResult Delete(int id)
		{
			new HocVuDao().Delete(id);
			return RedirectToAction("Index");
		}
		public void SetViewBag(int? DonViID = null)
		{
			var dao = new DonViDao();
			ViewBag.DonViID = new SelectList(dao.ListAll(), "DonViID", "TenDonVi", DonViID);
		}
		public void SetViewBagDanhMuc(int? DanhMucID = null)
		{
			var dao = new DanhMucDao();
			ViewBag.DanhMucID = new SelectList(dao.ListAll(), "DanhMucID", "TenDanhMuc",DanhMucID);
		}
		public void SetViewBagUser(int? UserID = null)
		{
			var dao = new UserDao();
			ViewBag.UserID = new SelectList(dao.ListAll(), "UserID", "UserName", UserID);
		}
	}
}