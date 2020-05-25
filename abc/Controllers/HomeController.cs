using abc.Common;
using abc.Models;
using Models.Dao;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace abc.Controllers
{
    public class HomeController : Controller
    {
		// GET: Home
		DoAnDbContext db = new DoAnDbContext();

		public ActionResult Index()
        {
			SetViewBag();
			return View();
        }
		[HttpPost]
		public ActionResult Index(HocVu hocvu)
		{
			User a = CheckAuthorize.Instance.XuatUserID();
			
			if (ModelState.IsValid)
			{
				var dao = new HocVuDao();
				int dem ;	
				var child = db.HocVus.Where(x => x.UserID + x.DanhMucID == a.UserID + hocvu.DanhMucID && x.UserID == a.UserID);
				if(child.Count() > 0)
				{
					dem = child.Count() + 1;
				}
				else
				{
					dem = 1;
				}
				int id = dao.Insert2(hocvu, a, dem);
				if (id > 0)
				{
					SetAlert("Thêm học vụ thành công", "success");
				}
				else
				{
					ModelState.AddModelError("", "Thêm học vụ không thành công");
				}
				SetViewBag();
			}
			return View();
		}
		public void SetViewBag(int? DonViID = null)
		{
			var dao = new DonViDao();
			ViewBag.DonViID = new SelectList(dao.ListAll(), "DonViID", "TenDonVi", DonViID);
		}
		public JsonResult GetDanhMucList(int DonViID)
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<DanhMuc> DanhMucList = db.DanhMucs.Where(x => x.DonViID == DonViID).ToList();
			return Json(DanhMucList, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetParentID(int DanhMucID)
		{
			db.Configuration.ProxyCreationEnabled = false;
			User a = CheckAuthorize.Instance.XuatUserID();
			List<HocVu> child = db.HocVus.Where(x => x.UserID + x.DanhMucID == a.UserID + DanhMucID && x.UserID == a.UserID).ToList();
			return Json(child, JsonRequestBehavior.AllowGet);
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