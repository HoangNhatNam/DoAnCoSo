using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
	public class CustomCreateVaiTro
	{
		[DisplayName("Tên vai trò")]
		public string TenVaiTro { get; set; }
		//public int? VaiTroID { get; set; }
		[DisplayName("Chức năng")]
		public int? ChucNangID { get; set; }
		[DisplayName("Xem")]
		public bool? xem { get; set; }
		[DisplayName("Thêm")]
		public bool? them { get; set; }
		[DisplayName("Sửa")]
		public bool? sua { get; set; }
		[DisplayName("Xóa")]
		public bool? xoa { get; set; }
	}
}
