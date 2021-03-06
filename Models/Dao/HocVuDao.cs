﻿using Models.Framework;
using Models.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.Dao
{
	public class HocVuDao
	{
		DoAnDbContext db = null;
		public HocVuDao()
		{
			db = new DoAnDbContext();
		}
		public List<HocVu> ListAll()
		{
			return db.HocVus.ToList();
		}
		public int Insert(HocVu entity, User a)
		{
			db.HocVus.Add(entity);
			entity.TinhTrang = false;
			entity.UserID = a.UserID;	
			entity.NgayTao = DateTime.Now;
			db.SaveChanges();
			return entity.HocVuID;
		}
		public int Insert2(HocVu entity, User a, int dem)
		{
			db.HocVus.Add(entity);
			entity.TinhTrang = false;
			entity.UserID = a.UserID;
			entity.NgayTao = DateTime.Now;
			entity.ParentID = dem;
			db.SaveChanges();
			return entity.HocVuID;
		}
		
		public bool Update(HocVu entity)
		{

			try
			{
				var hocvu = db.HocVus.Find(entity.HocVuID);
				hocvu.NgayTao = entity.NgayTao;
				hocvu.YeuCauThem = entity.YeuCauThem;
				hocvu.TinhTrang = entity.TinhTrang;
				hocvu.ParentID = entity.ParentID;
				hocvu.ChuyenVienID = entity.ChuyenVienID;
				hocvu.NgayHen = entity.NgayHen;
				hocvu.DanhMucID = entity.DanhMucID;
				hocvu.UserID = entity.UserID;
				hocvu.DonViID = entity.DonViID;
				db.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public bool ChangeTinhTrang(int id)
		{
			var hocvu = db.HocVus.Find(id);
			
			hocvu.TinhTrang = !hocvu.TinhTrang;
			
			db.SaveChanges();
			return hocvu.TinhTrang;
		}
		public IEnumerable<HocVuViewModel> ListHocVu(string searchString, string searchUser, int page, int pageSize)
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

			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.TenDonVi.Contains(searchString) || x.TenDanhMuc.Contains(searchString)
				|| x.TenVaiTro.Contains(searchString) || x.YeuCauThem.Contains(searchString) );
			}

			if (!string.IsNullOrEmpty(searchUser))
			{
				model = model.Where(x => x.UserName.Contains(searchUser) || x.TinhTrang.Value.ToString().Contains(searchUser));
			}
			return model.OrderBy(x => x.NgayTao).ToPagedList(page, pageSize);
		}

		public IEnumerable<HocVuViewModel> ListLichSu(string searchString, int page, int pageSize, User e)
		{
			IQueryable<HocVuViewModel> model;
			
			model = from a in db.HocVus
					join b in db.DonVis on a.DonViID equals b.DonViID
					join c in db.Users on a.UserID equals c.UserID where(c.UserID == e.UserID || c.VaiTroID != 17)
					join d in db.DanhMucs on a.DanhMucID equals d.DanhMucID
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
					};
			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.TenDonVi.Contains(searchString) || x.UserName.Contains(searchString) || x.TenDanhMuc.Contains(searchString));
			}
			return model.OrderBy(x => x.HocVuID).ToPagedList(page, pageSize);
		}

		public IEnumerable<CustomViewThongKe> ListThongKe(string searchYeuCau,string searchTinhTrang,string searchName,
			string searchTenDanhMuc, string searchTenDonVi, string searchTenLop, string searchEmail, string searchTenVaiTro, int page, int pageSize)
		{
			IQueryable<CustomViewThongKe> model;
			model = from a in db.HocVus
					join b in db.DonVis on a.DonViID equals b.DonViID
					join c in db.Users on a.UserID equals c.UserID
					join d in db.DanhMucs on a.DanhMucID equals d.DanhMucID
					join e in db.Lops on c.LopID equals e.LopID
					join g in db.VaiTroes on c.VaiTroID equals g.VaiTroID
					select new CustomViewThongKe()
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
						TenLop = e.TenLop,
						Email = c.Email,
						TenVaiTro = g.TenVaiTro
					};
			if (!string.IsNullOrEmpty(searchYeuCau))
			{
				model = model.Where(x => x.YeuCauThem.Contains(searchYeuCau));
			}
			if (!string.IsNullOrEmpty(searchTinhTrang))
			{
				model = model.Where(x => x.TinhTrang.ToString().Contains(searchTinhTrang));
			}
			if (!string.IsNullOrEmpty(searchName))
			{
				model = model.Where(x => x.UserName.Contains(searchName));
			}
			if (!string.IsNullOrEmpty(searchTenDanhMuc))
			{
				model = model.Where(x => x.TenDanhMuc.Contains(searchTenDanhMuc));
			}
			if (!string.IsNullOrEmpty(searchTenDonVi))
			{
				model = model.Where(x => x.TenDonVi.Contains(searchTenDonVi));
			}
			if (!string.IsNullOrEmpty(searchTenLop))
			{
				model = model.Where(x => x.TenLop.Contains(searchTenLop));
			}
			if (!string.IsNullOrEmpty(searchEmail))
			{
				model = model.Where(x => x.Email.Contains(searchEmail));
			}
			if (!string.IsNullOrEmpty(searchTenVaiTro))
			{
				model = model.Where(x => x.TenVaiTro.Contains(searchTenVaiTro));
			}
			return model.OrderBy(x => x.HocVuID).ToPagedList(page, pageSize);
		}

		public HocVu ViewDetail(int id)
		{
			return db.HocVus.Find(id);
		}
		public bool Delete(int id)
		{

			try
			{
				var hocvu = db.HocVus.Find(id);
				db.HocVus.Remove(hocvu);
				db.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
