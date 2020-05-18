using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace abc.Areas.Admin.Models
{
	public class LoginModel
	{
		[Required(ErrorMessage ="Mời nhập lại user name")]
		public string Email { set; get; }

		
		public string Pass { set; get; }
		public bool RememberMe { set; get; }
	}
}