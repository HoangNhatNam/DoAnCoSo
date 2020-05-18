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
	public class VaiTroChucNangDao
	{
		DoAnDbContext db = null;
		public VaiTroChucNangDao()
		{
			db = new DoAnDbContext();
		}
		public int Insert(VaiTroChucNangPhanMem entity)
		{
			db.VaiTroChucNangPhanMems.Add(entity);
			db.SaveChanges();
			return entity.VaiTroChucNangID;
		}

		public bool Update(VaiTroChucNangPhanMem entity)
		{

			try
			{
				var vtcn = db.VaiTroChucNangPhanMems.Find(entity.VaiTroChucNangID);
				vtcn.them = entity.them;
				vtcn.xoa = entity.xoa;
				vtcn.sua = entity.sua;
				vtcn.xem = entity.xem;
				vtcn.ChucNangID = entity.ChucNangID;
				vtcn.VaiTroID = entity.VaiTroID;
				db.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public IEnumerable<VaiTroChucNangViewModel> ListVTCN(string searchString, int page, int pageSize)
		{
			IQueryable<VaiTroChucNangViewModel> model;
			model = from a in db.VaiTroChucNangPhanMems
					join b in db.VaiTroes on a.VaiTroID equals b.VaiTroID
					join c in db.ChucNangPhanMems on a.ChucNangID equals c.ChucNangID
					select new VaiTroChucNangViewModel()
					{
						VaiTroChucNangID = a.VaiTroChucNangID,
						xem = a.xem,
						them = a.them,
						xoa = a.xoa,
						sua = a.sua,
						TenChucNang = c.TenChucNang,
						TenVaiTro = b.TenVaiTro
					};
			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.TenChucNang.Contains(searchString) || x.TenVaiTro.Contains(searchString));
			}
			return model.OrderBy(x => x.TenVaiTro).ToPagedList(page, pageSize);
		}
		public VaiTroChucNangPhanMem ViewDetail(int id)
		{
			return db.VaiTroChucNangPhanMems.Find(id);
		}
		public bool Delete(int id)
		{

			try
			{
				var vtcn = db.VaiTroChucNangPhanMems.Find(id);
				db.VaiTroChucNangPhanMems.Remove(vtcn);
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
