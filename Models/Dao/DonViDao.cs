using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ViewModel;
using PagedList;

namespace Models.Dao
{
	public class DonViDao
	{
		DoAnDbContext db = null;
		public DonViDao()
		{
			db = new DoAnDbContext();
		}
		public List<DonVi> ListAll()
		{
			return db.DonVis.ToList();
		}

		public int Insert(DonVi entity)
		{
			db.DonVis.Add(entity);
			db.SaveChanges();
			return entity.DonViID;
		}

		public bool Update(DonVi entity)
		{

			try
			{
				var donvi = db.DonVis.Find(entity.DonViID);
				donvi.TenDonVi = entity.TenDonVi;
				db.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public IEnumerable<DonVi> ListAllPaging(string searchString, int page, int pageSize)
		{
			
			IQueryable<DonVi> model1 = db.DonVis;
			if (!string.IsNullOrEmpty(searchString))
			{
				model1 = model1.Where(x => x.TenDonVi.Contains(searchString));
			}
			return model1.OrderBy(x => x.DonViID).ToPagedList(page, pageSize);
		}

		
		public DonVi GetById(string tenDonVi)
		{
			return db.DonVis.SingleOrDefault(x => x.TenDonVi == tenDonVi);
		}
		public DonVi ViewDetail(int id)
		{
			return db.DonVis.Find(id);
		}
		
		public bool Delete(int id)
		{

			try
			{
				var donvi = db.DonVis.Find(id);
				db.DonVis.Remove(donvi);
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
