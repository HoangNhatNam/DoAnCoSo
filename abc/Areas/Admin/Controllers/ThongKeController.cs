using Models.Dao;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace abc.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
		// GET: Admin/ThongKe
		DoAnDbContext db = new DoAnDbContext();
		public ActionResult Index(string searchYeuCau, string searchTinhTrang, string searchName,
			string searchTenDanhMuc, string searchTenDonVi, string searchTenLop, string searchEmail, string searchTenVaiTro, int page = 1, int pageSize = 10)
        {
			var dao = new HocVuDao();
			var model = dao.ListThongKe(searchYeuCau, searchTinhTrang, searchName, searchTenDanhMuc,
				searchTenDonVi, searchTenLop, searchEmail, searchTenVaiTro, page, pageSize);
			var dao1 = new DonViDao();
			ViewBag.listDonVi = new SelectList(dao1.ListAll(), "TenDonVi", "TenDonVi");
			ViewBag.listTinhTrang = new SelectList(dao.ListAll(), "TinhTrang", "TinhTrang");
			var lop = new LopDao();
			ViewBag.listLop = new SelectList(lop.ListAll(), "TenLop", "TenLop");
			var vaitro = new VaiTroDao();
			ViewBag.listVaiTro = new SelectList(vaitro.ListAll(), "TenVaiTro", "TenVaiTro");
			ViewBag.searchYeuCau = searchYeuCau;
			ViewBag.searchTinhTrang = searchTinhTrang;
			ViewBag.searchName = searchName;
			ViewBag.searchTenDanhMuc = searchTenDanhMuc;
			ViewBag.searchTenDonVi = searchTenDonVi;
			ViewBag.searchTenLop = searchTenLop;
			ViewBag.searchEmail = searchEmail;
			ViewBag.searchTenVaiTro = searchTenVaiTro;
			return View(model);
		}
		public JsonResult GetDanhMucList(string TenDonVi)
		{
			db.Configuration.ProxyCreationEnabled = false;
			var getDVID = from q in db.DonVis
						  where (q.TenDonVi == TenDonVi)
						  select q.DonViID;
			List<DanhMuc> DanhMucList = db.DanhMucs.Where(x => x.DonViID == getDVID.Max()).ToList();
			return Json(DanhMucList, JsonRequestBehavior.AllowGet);
		}
	}
}