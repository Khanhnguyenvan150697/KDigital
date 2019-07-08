using DuAnCuoiKhoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuAnCuoiKhoa.Controllers
{
    public class DanhMucController : Controller
    {
        DuAnCuoiKhoaEntities db = new DuAnCuoiKhoaEntities();
        // GET: DanhMuc
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DanhMucSP_Partial()
        {
            var lstDanhMuc = db.DanhMucSPs;
            return PartialView(lstDanhMuc);
        }

        public ActionResult ThuongHieuSP()
        {
            var lstThuongHieu = db.ThuongHieux;
            return PartialView(lstThuongHieu);
        }
    }
}