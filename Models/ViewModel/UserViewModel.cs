using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
	public class UserViewModel
	{
		public int UserID { get; set; }
		public string Email { get; set; }
		
		public string UserName { get; set; }
		public string TenDonVi { get; set; }
		public string TenLop { get; set; }
		public string TenVaiTro { get; set; }
		
		public int? DonViID { get; set; }
		
		public int? VaiTroID { get; set; }
		
		public int? LopID { get; set; }
	}
}
