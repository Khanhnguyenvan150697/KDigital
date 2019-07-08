using DuAnCuoiKhoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuAnCuoiKhoa.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        DuAnCuoiKhoaEntities db = new DuAnCuoiKhoaEntities();
        // GET: ChiTietSanPham
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ChiTietSanPham(int id)
        {

            //Lấy ra sản phẩm
            var ChiTietSP = db.SanPhams.Find(id);

            if (ChiTietSP == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ChiTietSP);
            }            
        }
    }
}