using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace abc
{
	[Serializable]
	public class UserLogin
	{
		public int UserID { set; get; }
		public string Email { set; get; }
	}
}