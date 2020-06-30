using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
	public class HocVuViewModel
	{
		public int HocVuID { get; set; }

		public DateTime? NgayTao { get; set; }
		public string YeuCauThem { get; set; }

		public bool? TinhTrang { get; set; }

		public int? ParentID { get; set; }

		public int? ChuyenVienID { get; set; }
		public DateTime? NgayHen { get; set; }

		public int? DanhMucID { get; set; }

		public int? UserID { get; set; }

		public int? DonViID { get; set; }
		public string TenDanhMuc { get; set; }

		public string UserName { get; set; }
		public string TenDonVi { get; set; }

		public string TenVaiTro { get; set; }
		
	}
}

