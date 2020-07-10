using Models.Dao;
using Models.Framework;
using Models.ViewModel;
using Rotativa;
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

			var list = dao.ListAll();
			List<int> repart = new List<int>();
			var userID = list.Select(x => x.UserID).Distinct();
			var listUser = from a in userID
						  join b in db.Users on a.Value equals b.UserID
						  select b.UserName;
			foreach (var item in userID)
			{
				repart.Add(list.Count(x => x.UserID == item));
			}
			var rep = repart;
			ViewBag.UserID = listUser;
			ViewBag.REP = repart.ToList();


			var danhmucID = list.Select(x => x.DanhMucID).Distinct();
			var listDM = from a in danhmucID
						join b in db.DanhMucs on a.ToString() equals b.DanhMucID.ToString()
						   select b.TenDanhMuc;
			List<int> listDanhMuc = new List<int>();
			foreach (var item in danhmucID)
			{
				listDanhMuc.Add(list.Count(x => x.DanhMucID == item));
			}
			ViewBag.danhmucID = listDM;
			ViewBag.listDanhMuc = listDanhMuc.ToList();
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
		public ActionResult PrintAll(string searchYeuCau, string searchTinhTrang, string searchName,
			string searchTenDanhMuc, string searchTenDonVi, string searchTenLop, string searchEmail, string searchTenVaiTro)
		{
			var dao = new HocVuDao();
			var model = dao.ListThongKe(searchYeuCau, searchTinhTrang, searchName, searchTenDanhMuc,
				searchTenDonVi, searchTenLop, searchEmail, searchTenVaiTro, 1, 10);

			var q = new ActionAsPdf("Index", model);
			var b = new ViewAsPdf("Index", model);
			var a = new PartialViewAsPdf("Index", model);
			return b;
		}
		public ActionResult GetData()
		{
			int hoanthanh = db.HocVus.Where(x => x.TinhTrang.ToString() == "True").Count();
			int chuahoanthanh = db.HocVus.Where(x => x.TinhTrang.ToString() == "False").Count();
			Ratio obj = new Ratio();
			obj.HoanThanh = hoanthanh;
			obj.ChuaHoanThanh = chuahoanthanh;

			return Json(obj, JsonRequestBehavior.AllowGet);
		}
		public class Ratio
		{
			public int HoanThanh { get; set; }
			public int ChuaHoanThanh { get; set; }
		}
	}
}