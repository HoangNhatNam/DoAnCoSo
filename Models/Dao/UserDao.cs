using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
using Models.ViewModel;
using PagedList;

namespace Models.Dao
{
	public class UserDao	
	{
		DoAnDbContext db = null;
		public UserDao()
		{
			db = new DoAnDbContext();
		}
		public List<User> ListAll()
		{
			return db.Users.ToList();
		}
		public int Insert(User entity)
		{	
			db.Users.Add(entity);		
			db.SaveChanges();
			return entity.UserID;
		}

		public bool Update(User entity)
		{
			
			try
			{
				var user = db.Users.Find(entity.UserID);
				user.Email = entity.Email;
				user.UserName = entity.UserName;
				user.LopID = entity.LopID;
				user.DonViID = entity.DonViID;
				user.VaiTroID = entity.VaiTroID;
				db.SaveChanges();
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}
		public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
		{
			IQueryable<User> model = db.Users;
			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.UserName.Contains(searchString) || x.Email.Contains(searchString));
			}
			return model.OrderBy(x => x.UserID).ToPagedList(page, pageSize);
		}

		public IEnumerable<UserViewModel> ListUser(string searchString, int page, int pageSize)
		{
			IQueryable<UserViewModel> model;
						model = from a in db.Users
						join b in db.DonVis on a.DonViID equals b.DonViID
						join c in db.Lops on a.LopID equals c.LopID
						join d in db.VaiTroes on a.VaiTroID equals d.VaiTroID
						select new UserViewModel()
						{
							UserID = a.UserID,
							Email = a.Email,
							UserName = a.UserName,
							TenDonVi = b.TenDonVi,
							TenLop = c.TenLop,
							TenVaiTro = d.TenVaiTro
						};
			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.UserName.Contains(searchString) || x.Email.Contains(searchString));
			}
			return model.OrderBy(x => x.TenVaiTro).ToPagedList(page, pageSize);
		}
		public User GetById(string userName)
		{
			return db.Users.SingleOrDefault(x => x.Email == userName);
		}
		public User ViewDetail(int id)
		{
			return db.Users.Find(id);
		}
		public bool Login(string userName)
		{
			var result = db.Users.Count(x => x.Email == userName);
			if(result > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public bool Delete(int id)
		{
			
			try
			{
				var user = db.Users.Find(id);
				db.Users.Remove(user);
				db.SaveChanges();
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}
	}
}
