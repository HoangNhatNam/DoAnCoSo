using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
	public class CustomCreateVaiTro
	{
		public string TenVaiTro { get; set; }
		//public int? VaiTroID { get; set; }
		public int? ChucNangID { get; set; }

		public bool? xem { get; set; }

		public bool? them { get; set; }

		public bool? sua { get; set; }

		public bool? xoa { get; set; }
	}
}
