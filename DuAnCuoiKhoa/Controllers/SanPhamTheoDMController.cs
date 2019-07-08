using DuAnCuoiKhoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace DuAnCuoiKhoa.Controllers
{
    public class SanPhamTheoDMController : Controller
    {
        DuAnCuoiKhoaEntities db = new DuAnCuoiKhoaEntities();

        // GET: SanPhamTheoDM
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SanPhamMoi(int page = 1)
        {
            //load sản phẩm mới
            var SPMoi = db.SanPhams.OrderBy(x => x.ID_SanPham).ToPagedList(page, 18);

            return PartialView(SPMoi);
        }

        [HttpGet]
        public ActionResult SanPhamTheoDanhMuc(int id=0, int page = 1)
        {                 
            var sp = db.SanPhams.OrderBy(x=>x.ID_DanhMuc).Where(x => x.ID_DanhMuc == id).ToPagedList(page,9);

            var danhMuc = db.DanhMucSPs.FirstOrDefault(x => x.ID_DanhMuc == id);

            if(danhMuc != null)
            {
                ViewBag.DanhMuc = danhMuc;
                
            }
            return View(sp);
        }
    }
}