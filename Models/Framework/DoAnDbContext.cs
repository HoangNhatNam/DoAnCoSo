namespace Models.Framework
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class DoAnDbContext : DbContext
	{
		public DoAnDbContext()
			: base("name=DoAnDbContext")
		{
		}

		public virtual DbSet<ChucNangPhanMem> ChucNangPhanMems { get; set; }
		public virtual DbSet<DanhMuc> DanhMucs { get; set; }
		public virtual DbSet<DonVi> DonVis { get; set; }
		public virtual DbSet<HocVu> HocVus { get; set; }
		public virtual DbSet<Lop> Lops { get; set; }
		public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<VaiTro> VaiTroes { get; set; }
		public virtual DbSet<VaiTroChucNangPhanMem> VaiTroChucNangPhanMems { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ChucNangPhanMem>()
				.Property(e => e.TenTrang)
				.IsFixedLength();

			modelBuilder.Entity<User>()
				.Property(e => e.Email)
				.IsUnicode(false);

			modelBuilder.Entity<User>()
				.Property(e => e.UserName)
				.IsUnicode(false);
		}
	}
}
