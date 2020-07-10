using abc.Common;
using abc.Models;
using Models.Dao;
using Models.Framework;
using Models.ViewModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace abc.Areas.Admin.Controllers
{
    public class HocVuController : BaseController
	{
		// GET: Admin/HocVu
		DoAnDbContext db = new DoAnDbContext();
		[KiemTraQuyen(PermissionName = "DanhSachHocVu")]
		public ActionResult Index(string searchString, string searchUser, int page = 1, int pageSize = 100)
		{
			var dao = new HocVuDao();
			var model = dao.ListHocVu(searchString, searchUser, page, pageSize);
			ViewBag.SearchString = searchString;
			ViewBag.searchUser = searchUser;

			var dao1 = new DonViDao();
			ViewBag.listDonVi = new SelectList(dao1.ListAll(), "TenDonVi", "TenDonVi");
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
		[HttpGet]
		public ActionResult Chat(int id)
		{
			var hocvu = new HocVuDao();
			var content = hocvu.ViewDetail(id);
			return View(content);
		}
		public ActionResult Detai(int id)
		{
			var hocvu = new HocVuDao();
			var content = hocvu.ViewDetail(id);
			SetViewBag(content.DonViID);
			SetViewBagDanhMuc(content.DanhMucID);
			SetViewBagUser(content.UserID);
			return View(content);
		}
		[HttpPost]
		public ActionResult Chat(HocVu hocvu)
		{
			User a = CheckAuthorize.Instance.XuatUserID();
			if (ModelState.IsValid)
			{
				var dao = new HocVuDao();
				int id = dao.Insert2(hocvu, a, hocvu.HocVuID);
				if (id > 0)
				{
					SetAlert("Thêm thành công", "success");
					return RedirectToAction("Index", "HocVu");
				}
				else
				{
					ModelState.AddModelError("", "Thêm không thành công");
				}
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
		[HttpPost]
		public JsonResult ChangeTinhTrang(int id)
		{
			var result = new HocVuDao().ChangeTinhTrang(id);
			return Json(new
			{
				status = result
			});
		}
		public void ExportToExcel()
		{
			IQueryable<HocVuViewModel> model;
			model = from a in db.HocVus
					join b in db.DonVis on a.DonViID equals b.DonViID
					join c in db.Users on a.UserID equals c.UserID
					join d in db.DanhMucs on a.DanhMucID equals d.DanhMucID
					join e in db.VaiTroes on c.VaiTroID equals e.VaiTroID
					select new HocVuViewModel()
					{
						HocVuID = a.HocVuID,
						NgayTao = a.NgayTao,
						YeuCauThem = a.YeuCauThem,
						TinhTrang = a.TinhTrang,
						UserName = c.UserName,
						ParentID = a.ParentID,
						NgayHen = a.NgayHen,
						TenDanhMuc = d.TenDanhMuc,
						TenDonVi = b.TenDonVi,
						TenVaiTro = e.TenVaiTro
					};
			model.ToList();
			ExcelPackage pck = new ExcelPackage();
			ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

			

			ws.Cells["A3"].Value = "Date";
			ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

			ws.Cells["A6"].Value = "Ngày tạo";
			ws.Cells["B6"].Value = "Yêu cầu thêm";
			ws.Cells["C6"].Value = "Ngày hẹn";
			ws.Cells["D6"].Value = "Đơn vị";
			ws.Cells["E6"].Value = "Danh mục";
			ws.Cells["F6"].Value = "User";
			ws.Cells["G6"].Value = "Vai trò";
			//ws.Cells["H6"].Value = "Lần gửi";
			ws.Cells["I6"].Value = "Tình trạng";

			using (var range = ws.Cells["A6:I6"])
			{
				// Set PatternType
				range.Style.Fill.PatternType = ExcelFillStyle.DarkGray;
				// Set Màu cho Background
				range.Style.Fill.BackgroundColor.SetColor(Color.Aqua);
				// Canh giữa cho các text
				range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				// Set Font cho text  trong Range hiện tại
				range.Style.Font.SetFromFont(new Font("Arial", 10));
				// Set Border
				range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
				// Set màu ch Border
				range.Style.Border.Bottom.Color.SetColor(Color.Blue);
			}

			int rowStart = 7;
			foreach (var item in model)
			{
				ws.Cells[string.Format("A{0}", rowStart)].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", item.NgayTao);
				ws.Cells[string.Format("B{0}", rowStart)].Value = item.YeuCauThem;
				ws.Cells[string.Format("C{0}", rowStart)].Value = string.Format("{0:dd MMMM yyyy} ", item.NgayHen);
				ws.Cells[string.Format("D{0}", rowStart)].Value = item.TenDonVi;
				ws.Cells[string.Format("E{0}", rowStart)].Value = item.TenDanhMuc;
				ws.Cells[string.Format("F{0}", rowStart)].Value = item.UserName;
				ws.Cells[string.Format("G{0}", rowStart)].Value = item.TenVaiTro;
				//ws.Cells[string.Format("H{0}", rowStart)].Value = item.ParentID;
				ws.Cells[string.Format("I{0}", rowStart)].Value = item.TinhTrang == true ? "Hoàn thành" : "Chưa hoàn thành";
				
				rowStart++;
			}

			ws.Cells["A:AZ"].AutoFitColumns();
			Response.Clear();
			Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
			Response.BinaryWrite(pck.GetAsByteArray());
			Response.End();

		}
		
		public ActionResult PrintAll(string searchString, string searchUser)
		{
			var dao = new HocVuDao();
			var model = dao.ListHocVu(searchString,searchUser, 1, 10);

			var q = new ActionAsPdf("Index", model);
			var b = new ViewAsPdf("Index", model);
			var a = new PartialViewAsPdf("Index", model);
			return b;
		}
	}
}