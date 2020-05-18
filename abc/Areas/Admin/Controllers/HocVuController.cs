using abc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace abc.Areas.Admin.Controllers
{
    public class HocVuController : BaseController
	{
		// GET: Admin/HocVu
		[KiemTraQuyen(PermissionName = "DanhSachHocVu")]
		public ActionResult Index()
        {
            return View();
        }
    }
}