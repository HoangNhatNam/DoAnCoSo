using Models.Framework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
	public class VaiTroDao
	{
		DoAnDbContext db = null;
		public VaiTroDao()
		{
			db = new DoAnDbContext();
		}
		public List<VaiTro> ListAll()
		{
			return db.VaiTroes.ToList();
		}

		public int Insert(VaiTro entity)
		{
			db.VaiTroes.Add(entity);
			db.SaveChanges();
			return entity.VaiTroID;
		}

		public bool Update(VaiTro entity)
		{

			try
			{
				var vaitro = db.VaiTroes.Find(entity.VaiTroID);
				vaitro.TenVaiTro = entity.TenVaiTro;
				db.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public IEnumerable<VaiTro> ListAllPaging(string searchString, int page, int pageSize)
		{

			IQueryable<VaiTro> model1 = db.VaiTroes;
			if (!string.IsNullOrEmpty(searchString))
			{
				model1 = model1.Where(x => x.TenVaiTro.Contains(searchString));
			}
			return model1.OrderBy(x => x.TenVaiTro).ToPagedList(page, pageSize);
		}


		public VaiTro GetById(string tenVaiTro)
		{
			return db.VaiTroes.SingleOrDefault(x => x.TenVaiTro == tenVaiTro);
		}
		public VaiTro ViewDetail(int id)
		{
			return db.VaiTroes.Find(id);
		}

		public bool Delete(int id)
		{

			try
			{
				var vaitro = db.VaiTroes.Find(id);
				db.VaiTroes.Remove(vaitro);
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
