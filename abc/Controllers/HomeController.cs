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
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
			SetViewBag();
			SetViewBagDanhMuc();
			return View();
        }
		[HttpPost]
		public ActionResult Index(HocVu hocvu)
		{
			User a = CheckAuthorize.Instance.XuatUserID();
			if (ModelState.IsValid)
			{
				var dao = new HocVuDao();
				int id = dao.Insert2(hocvu, a);
				if (id > 0)
				{
					SetAlert("Thêm học vụ thành công", "success");
				}
				else
				{
					ModelState.AddModelError("", "Thêm học vụ không thành công");
				}
				SetViewBag();
				SetViewBagDanhMuc();
			}
			return View();
		}
		public void SetViewBag(int? DonViID = null)
		{
			var dao = new DonViDao();
			ViewBag.DonViID = new SelectList(dao.ListAll(), "DonViID", "TenDonVi", DonViID);
		}
		public void SetViewBagDanhMuc(int? DanhMucID = null)
		{
			var dao = new DanhMucDao();
			//string strDDLValue = Request.Form["ddlVendor"].ToString();
			ViewBag.DanhMucID = new SelectList(dao.ListAll(), "DanhMucID", "TenDanhMuc", DanhMucID);
		}
		protected void SetAlert(string massage, string type)
		{
			TempData["AlertMassage"] = massage;
			if (type == "success")
			{
				TempData["AlertType"] = "alert-success";
			}
			else if (type == "warning")
			{
				TempData["AlertType"] = "alert-warning";
			}
			else if (type == "error")
			{
				TempData["AlertType"] = "alert - danger";
			}
		}
	}
}