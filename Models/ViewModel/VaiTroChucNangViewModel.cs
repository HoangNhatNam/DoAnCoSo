using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
	public class VaiTroChucNangViewModel
	{
		public int VaiTroChucNangID { get; set; }

		public bool? xem { get; set; }

		public bool? them { get; set; }

		public bool? sua { get; set; }

		public bool? xoa { get; set; }

		public string TenVaiTro { get; set; }

		public string TenChucNang { get; set; }
	}
}
