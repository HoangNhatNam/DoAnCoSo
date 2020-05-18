using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace abc.Models
{
	public class Comon
	{
		private static DoAnDbContext _Instance = new DoAnDbContext();
		public static DoAnDbContext Instance
		{
			get { return Comon._Instance; }
			set { Comon._Instance = value; }

		}
	}
}