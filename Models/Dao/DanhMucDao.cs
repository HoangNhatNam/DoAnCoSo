using Models.Framework;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Models.Dao
{
	public class DanhMucDao
	{
		DoAnDbContext db = null;
		public DanhMucDao()
		{
			db = new DoAnDbContext();
		}
		public List<DanhMuc> ListAll()
		{
			return db.DanhMucs.ToList();
		}
		public List<DanhMuc> ListCustom(int donviID)
		{
			return db.DanhMucs.Where(x => x.DonViID == donviID).ToList();
		}
		public int Insert(DanhMuc entity)
		{
			db.DanhMucs.Add(entity);
			db.SaveChanges();
			return entity.DanhMucID;
		}

		public bool Update(DanhMuc entity)
		{

			try
			{
				var danhmuc = db.DanhMucs.Find(entity.DanhMucID);
				danhmuc.TenDanhMuc = entity.TenDanhMuc;
				danhmuc.DonViID = entity.DonViID;			
				db.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public IEnumerable<DanhMucViewModel> ListDanhMuc(string searchString, int page, int pageSize)
		{
			IQueryable<DanhMucViewModel> model;
			model = from a in db.DanhMucs
					join b in db.DonVis on a.DonViID equals b.DonViID
					select new DanhMucViewModel()
					{
						 DanhMucID = a.DanhMucID,
						TenDanhMuc = a.TenDanhMuc,
						TenDonVi = b.TenDonVi,
						
					};
			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.TenDanhMuc.Contains(searchString) || x.TenDonVi.Contains(searchString));
			}
			return model.OrderBy(x => x.DanhMucID).ToPagedList(page, pageSize);
		}

		public DanhMuc GetById(int id)
		{
			return db.DanhMucs.Find(id);
		}

		public DanhMuc ViewDetail(int id)
		{
			return db.DanhMucs.Find(id);
		}

		public bool Delete(int id)
		{

			try
			{
				var danhmuc = db.DanhMucs.Find(id);
				db.DanhMucs.Remove(danhmuc);
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
