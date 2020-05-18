using abc.Models;
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
	
	public class UserController : BaseController
	{
		// GET: Admin/User
		[KiemTraQuyen(PermissionName = "DanhSachNguoiDung")]
		public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
			var dao = new UserDao();
			var model = dao.ListUser(searchString, page, pageSize);
			ViewBag.SearchString = searchString;
			return View(model);
        }
		[KiemTraQuyen(PermissionName = "DanhSachNguoiDung")]
		[HttpGet]
		public ActionResult Create()
		{
			SetViewBag();
			SetViewBagLop();
			SetViewBagVaiTro();
			return View();
		}
		[KiemTraQuyen(PermissionName = "DanhSachNguoiDung")]
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var user = new UserDao();
			var content = user.ViewDetail(id);
			SetViewBag(content.DonViID);
			SetViewBagLop(content.LopID);
			SetViewBagVaiTro(content.VaiTroID);
			return View(content);
		}
		[HttpPost]
		public ActionResult Create(User user)
		{
			if (ModelState.IsValid)
			{
				var dao = new UserDao();
				int id = dao.Insert(user);
				if (id > 0)
				{
					SetAlert("Thêm user thành công", "success");
					return RedirectToAction("Index", "User");
				}
				else
				{
					ModelState.AddModelError("", "Thêm user không thành công");
				}
				SetViewBag();
				SetViewBagLop();
				SetViewBagVaiTro();
			}

			return View("Index");
		}
		[HttpPost]
		public ActionResult Edit(User user)
		{
			if (ModelState.IsValid)
			{
				var dao = new UserDao();

				var result = dao.Update(user);
				
				if (result)
				{
					SetAlert("Sửa user thành công", "success");
					return RedirectToAction("Index", "User");
				}
				else
				{
					ModelState.AddModelError("", "cập nhật không thành công");
				}
				SetViewBag(user.DonViID);
				SetViewBagLop(user.LopID);
				SetViewBagVaiTro(user.VaiTroID);
			}
			return View("Index");
		}
		[HttpDelete]
		public ActionResult Delete(int id)
		{
			new UserDao().Delete(id);
			return RedirectToAction("Index");
		}

		public void SetViewBag(int? DonViID = null)
		{
			var dao = new DonViDao();
			ViewBag.DonViID = new SelectList(dao.ListAll(), "DonViID", "TenDonVi", DonViID);
		}
		public void SetViewBagLop(int? LopID = null)
		{
			var dao = new LopDao();
			ViewBag.LopID = new SelectList(dao.ListAll(), "LopID", "TenLop", LopID);
		}
		public void SetViewBagVaiTro(int? VaiTroID = null)
		{
			var dao = new VaiTroDao();
			ViewBag.VaiTroID = new SelectList(dao.ListAll(), "VaiTroID", "TenVaiTro", VaiTroID);
		}
	}
}