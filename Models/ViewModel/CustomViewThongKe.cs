using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
	public class CustomViewThongKe
	{
		public int HocVuID { get; set; }
		[DisplayName("Ngày tạo")]
		public DateTime? NgayTao { get; set; }

		[StringLength(250)]
		[DisplayName("Yêu cầu thêm")]
		public string YeuCauThem { get; set; }
		[DisplayName("Tình trạng")]
		public bool TinhTrang { get; set; }

		public int ParentID { get; set; }

		public int? ChuyenVienID { get; set; }

		[Column(TypeName = "date")]
		[DataType(DataType.Date)]
		[DisplayName("Ngày hẹn")]
		public DateTime? NgayHen { get; set; }

		public int DanhMucID { get; set; }

		public int? UserID { get; set; }

		public int? DonViID { get; set; }
		[DisplayName("Vai trò")]
		public int? VaiTroID { get; set; }

		[StringLength(30)]
		public string Email { get; set; }
		[DisplayName("Lớp")]
		public int? LopID { get; set; }

		[StringLength(75)]
		[DisplayName("Tên người dùng")]
		public string UserName { get; set; }
		[DisplayName("Tên lớp")]
		public string TenLop { get; set; }
		[DisplayName("Tên đơn vị")]
		public string TenDonVi { get; set; }
		[DisplayName("Tên danh mục")]
		public string TenDanhMuc { get; set; }
		[DisplayName("Tên vai trò")]
		public string TenVaiTro { get; set; }
	}
}
