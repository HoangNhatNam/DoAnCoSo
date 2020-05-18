using abc.Models;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace abc.Common
{
	public class CheckAuthorize
	{
		public bool isThem { get; set; }
		public bool isSua { get; set; }
		public bool isXoa { get; set; }
		public bool isXem { get; set; }
		public CheckAuthorize()
		{

		}
		private static CheckAuthorize _Instance = new CheckAuthorize();
		public static CheckAuthorize Instance
		{
			get { return CheckAuthorize._Instance; }
			set { CheckAuthorize._Instance = value; }
		}
		public int KiemTra()
		{
			User objUser = (User)HttpContext.Current.Session["useronline"];
			VaiTro objVT = Comon.Instance.VaiTroes.Where(p => p.VaiTroID == objUser.VaiTroID).First<VaiTro>();
			VaiTroChucNangPhanMem objVTCN = Comon.Instance.VaiTroChucNangPhanMems.Where(p => p.VaiTroID == objVT.VaiTroID).First<VaiTroChucNangPhanMem>();
			ChucNangPhanMem objCNPM = Comon.Instance.ChucNangPhanMems.Where(p => p.ChucNangID == objVTCN.ChucNangID).First<ChucNangPhanMem>();
			switch (objCNPM.TenTrang.Trim())
			{
				case "Admin":
					return 1;
				case "DanhSachNguoiDung":
					return 2;
				case "DanhSachDonVi":
					return 3;
				case "DanhSachLop":
					return 4;
				case "DanhSachHocVu":
					return 5;
				case "DanhSachDanhMuc":
					return 6;
				default:
					return 7;
			}
		}
		public string XuatQuyen()
		{
			User objUser = (User)HttpContext.Current.Session["useronline"];
			VaiTro objVT = Comon.Instance.VaiTroes.Where(p => p.VaiTroID == objUser.VaiTroID).First<VaiTro>();
			VaiTroChucNangPhanMem objVTCN = Comon.Instance.VaiTroChucNangPhanMems.Where(p => p.VaiTroID == objVT.VaiTroID).First<VaiTroChucNangPhanMem>();
			ChucNangPhanMem objCNPM = Comon.Instance.ChucNangPhanMems.Where(p => p.ChucNangID == objVTCN.ChucNangID).First<ChucNangPhanMem>();
			return objCNPM.TenTrang;
		}
		public string XuatEmailUser()
		{
			User objUser = (User)HttpContext.Current.Session["useronline"];
			return objUser.Email;
		}
		public void CheckPermission(string fromName)
		{
			User objUser = (User)HttpContext.Current.Session["useronline"];
			if (objUser != null)
			{
				VaiTro objVT = Comon.Instance.VaiTroes.Where(p => p.VaiTroID == objUser.VaiTroID).First<VaiTro>();
				ChucNangPhanMem objCN = Comon.Instance.ChucNangPhanMems.Where(p => p.TenTrang.Equals(fromName)).First<ChucNangPhanMem>();

				VaiTroChucNangPhanMem objVTCN = Comon.Instance.VaiTroChucNangPhanMems.Where(p => p.VaiTroID == objVT.VaiTroID).First<VaiTroChucNangPhanMem>();
				ChucNangPhanMem objCNPM = Comon.Instance.ChucNangPhanMems.Where(p => p.ChucNangID == objVTCN.ChucNangID).First<ChucNangPhanMem>();
				if (objCN != null)
				{
					int? roleID = objUser.VaiTroID;
					VaiTroChucNangPhanMem objQuyen = null;
					if (objCNPM.TenTrang.Trim() == "Admin")
					{
						this.isSua = true;
						this.isThem = true;
						this.isXem = true;
						this.isXoa = true;
					}
					try
					{
						objQuyen = Comon.Instance.VaiTroChucNangPhanMems.Where(p => p.VaiTroID != null && p.VaiTroID == objVT.VaiTroID &&
						p.ChucNangID == objCN.ChucNangID).First<VaiTroChucNangPhanMem>();
						
						if (objQuyen != null && objQuyen.xem.HasValue)
						{
							if (objQuyen.them.Value)
							{
								this.isThem = true;
							}
							else
							{
								this.isThem = false;
							}
							if (objQuyen.sua.Value)
							{
								this.isSua = true;
							}
							else
							{
								this.isSua = false;
							}
							if (objQuyen.xoa.Value)
							{
								this.isXoa = true;
							}
							else
							{
								this.isXoa = false;
							}
							if (objQuyen.xem.Value)
							{
								this.isXem = true;
							}
							else
							{
								this.isXem = false;
							}
						}
					}
					catch(Exception ex)
					{
						throw ex;
					}
				}
			}
		}
	}
}