using DuAnCuoiKhoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DuAnCuoiKhoa.Controllers
{
    
    public class DangKyController : Controller
    {
        DuAnCuoiKhoaEntities db = new DuAnCuoiKhoaEntities();
       
        //đăng ký
        [HttpGet]
        public ActionResult DangKy()
        {
            ViewBag.CauHoi = new SelectList(db.CauHoiBiMats.OrderBy(x => x.CauHoi), "ID_CauHoi", "CauHoi");
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(NguoiDung nd)
        {
            ViewBag.CauHoi = new SelectList(db.CauHoiBiMats.OrderBy(x => x.CauHoi), "ID_CauHoi", "CauHoi");
            db.NguoiDungs.Add(nd);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}