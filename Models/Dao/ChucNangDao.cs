using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
using PagedList;

namespace Models.Dao
{
	public class ChucNangDao
	{
		DoAnDbContext db = null;
		public ChucNangDao()
		{
			db = new DoAnDbContext();
		}
		public List<ChucNangPhanMem> ListAll()
		{
			return db.ChucNangPhanMems.ToList();
		}

		public int Insert(ChucNangPhanMem entity)
		{
			db.ChucNangPhanMems.Add(entity);
			db.SaveChanges();
			return entity.ChucNangID;
		}

		//public bool Update(ChucNangPhanMem entity)
		//{

		//	try
		//	{
		//		var donvi = db.ChucNangPhanMems.Find(entity.ChucNangID);
		//		donvi.TenChucNang = entity.TenChucNang;
		//		db.SaveChanges();
		//		return true;
		//	}
		//	catch (Exception ex)
		//	{
		//		return false;
		//	}
		//}
		public IEnumerable<ChucNangPhanMem> ListAllPaging(string searchString, int page, int pageSize)
		{

			IQueryable<ChucNangPhanMem> model1 = db.ChucNangPhanMems;
			if (!string.IsNullOrEmpty(searchString))
			{
				model1 = model1.Where(x => x.TenChucNang.Contains(searchString)|| x.TenTrang.Contains(searchString));
			}
			return model1.OrderBy(x => x.ChucNangID).ToPagedList(page, pageSize);
		}


		public ChucNangPhanMem GetById(string tenChucNang)
		{
			return db.ChucNangPhanMems.SingleOrDefault(x => x.TenChucNang == tenChucNang);
		}
		public ChucNangPhanMem ViewDetail(int id)
		{
			return db.ChucNangPhanMems.Find(id);
		}

		public bool Delete(int id)
		{

			try
			{
				var chucnang = db.ChucNangPhanMems.Find(id);
				db.ChucNangPhanMems.Remove(chucnang);
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
