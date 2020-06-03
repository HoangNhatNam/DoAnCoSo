using Models.Framework;
using Models.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
	public class LopDao
	{
		DoAnDbContext db = null;
		public LopDao()
		{
			db = new DoAnDbContext();
		}
		public List<Lop> ListAll()
		{
			return db.Lops.ToList();
		}
		public int Insert(Lop entity)
		{
			db.Lops.Add(entity);
			db.SaveChanges();
			return entity.LopID;
		}

		public bool Update(Lop entity)
		{

			try
			{
				var lop = db.Lops.Find(entity.LopID);
				lop.TenLop = entity.TenLop;
				lop.DonViID = entity.DonViID;
				db.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public IEnumerable<LopViewModel> ListLop(string searchString, int page, int pageSize)
		{
			IQueryable<LopViewModel> model;
			model = from a in db.Lops
					join b in db.DonVis on a.DonViID equals b.DonViID
					
					select new LopViewModel()
					{
						LopID = a.LopID,
						TenLop = a.TenLop,
						DonViID = b.DonViID,
						TenDonVi = b.TenDonVi,
					};
			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.TenLop.Contains(searchString) || x.TenDonVi.Contains(searchString));
			}
			return model.OrderBy(x => x.TenDonVi).ToPagedList(page, pageSize);
		}


		public Lop GetById(string tenLop)
		{
			return db.Lops.SingleOrDefault(x => x.TenLop == tenLop);
		}
		public Lop ViewDetail(int id)
		{
			return db.Lops.Find(id);
		}

		public bool Delete(int id)
		{

			try
			{
				var lop = db.Lops.Find(id);
				db.Lops.Remove(lop);
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
