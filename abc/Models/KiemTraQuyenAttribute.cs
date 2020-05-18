
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using abc.Models;

namespace abc.Models
{
	public class KiemTraQuyenAttribute : AuthorizeAttribute
	{
		public string PermissionName { get; set; }
		
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			//Lấy thông tin người dùng được lưu vào session
			User objUser = (User)HttpContext.Current.Session["useronline"];
			if (objUser != null)
			{
				// Lấy được mã vai trò
				int? roleID = objUser.VaiTroID;
				ChucNangPhanMem objChucNang = Comon.Instance.ChucNangPhanMems.Where(p => p.TenTrang.Equals(PermissionName)).First<ChucNangPhanMem>();
				VaiTro objVT = Comon.Instance.VaiTroes.Where(p => p.VaiTroID == roleID).First<VaiTro>();
				VaiTroChucNangPhanMem objVTCN = Comon.Instance.VaiTroChucNangPhanMems.Where(p => p.VaiTroID == objVT.VaiTroID).First<VaiTroChucNangPhanMem>();
				ChucNangPhanMem objCN = Comon.Instance.ChucNangPhanMems.Where(p => p.ChucNangID == objVTCN.ChucNangID).First<ChucNangPhanMem>();
				if (objChucNang != null)
				{
					if (objCN.TenTrang.Trim() == "Admin")
					{
						return true;
					}
					VaiTroChucNangPhanMem objQuyen = null;
						try
						{
							objQuyen = Comon.Instance.VaiTroChucNangPhanMems.Where(p => p.VaiTroID != null && p.VaiTroID == roleID &&
							p.ChucNangID == objChucNang.ChucNangID).First<VaiTroChucNangPhanMem>();
							if (objQuyen != null && objQuyen.xem.HasValue)
							{
								if (objQuyen.xem.Value)
								{
									return true;
								}
							}
						}
						catch
						{
							return false;
						}
				}
				

			}
			return false;
		}
		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			filterContext.Result = new ViewResult()
			{
				ViewName = "~/Areas/Admin/Views/Login/NotAuthorize.cshtml"
			};
		}
		
	}
}