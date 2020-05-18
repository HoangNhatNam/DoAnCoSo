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
    public class VaiTroChucNangController : BaseController
	{
		// GET: Admin/VaiTroChucNang
		[KiemTraQuyen(PermissionName = "Admin")]
		public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
		{
			var dao = new VaiTroChucNangDao();
			var model = dao.ListVTCN(searchString, page, pageSize);
			ViewBag.SearchString = searchString;
			return View(model);
		}
		[KiemTraQuyen(PermissionName = "Admin")]
		[HttpGet]
		public ActionResult Create()
		{
			SetViewBagVaiTro();
			SetViewBagChucNang();
			return View();
		}
		[HttpPost]
		public ActionResult Create(VaiTroChucNangPhanMem vtcn)
		{
			if (ModelState.IsValid)
			{
				var dao = new VaiTroChucNangDao();
				int id = dao.Insert(vtcn);
				if (id > 0)
				{
					SetAlert("Thêm thành công", "success");
					return RedirectToAction("Index", "VaiTroChucNang");
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
			var vtcn = new VaiTroChucNangDao();
			var content = vtcn.ViewDetail(id);
			SetViewBagVaiTro(content.VaiTroID);
			SetViewBagChucNang(content.ChucNangID);
			return View(content);
		}
		[HttpPost]
		public ActionResult Edit(VaiTroChucNangPhanMem vtcn)
		{
			if (ModelState.IsValid)
			{
				var dao = new VaiTroChucNangDao();

				var result = dao.Update(vtcn);

				if (result)
				{
					SetAlert("Chỉnh sửa thành công", "success");
					return RedirectToAction("Index", "VaiTroChucNang");
				}
				else
				{
					ModelState.AddModelError("", "cập nhật không thành công");
				}
				SetViewBagVaiTro(vtcn.VaiTroID);
				SetViewBagChucNang(vtcn.ChucNangID);
			}
			return View("Index");
		}

		[HttpDelete]
		public ActionResult Delete(int id)
		{
			new VaiTroChucNangDao().Delete(id);
			return RedirectToAction("Index");
		}

		public void SetViewBagVaiTro(int? VaiTroID = null)
		{
			var dao = new VaiTroDao();
			ViewBag.VaiTroID = new SelectList(dao.ListAll(), "VaiTroID", "TenVaiTro", VaiTroID);
		}
		public void SetViewBagChucNang(int? ChucNangID = null)
		{
			var dao = new ChucNangDao();
			ViewBag.ChucNangID = new SelectList(dao.ListAll(), "ChucNangID", "TenChucNang", ChucNangID);
		}
	}
}