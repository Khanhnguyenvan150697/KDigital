using DuAnCuoiKhoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuAnCuoiKhoa.Controllers
{
    public class DangNhapController : Controller
    {
        DuAnCuoiKhoaEntities db = new DuAnCuoiKhoaEntities();
        [HttpGet]
        // GET: DemoDangNhap
        public ActionResult DangNhap()
        {

            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string stendangnhap = f["txtTenDangNhap"].ToString();
            string smatkhau = f["txtmatKhau"].ToString();

            NguoiDung tv = db.NguoiDungs.SingleOrDefault(x => x.UserName == stendangnhap && x.Password == smatkhau);
            if (tv != null)
            {
                if (tv.MaLoaiNguoiDung == 1)
                {
                    Session["taikhoanAdmin"] = tv;
                    return RedirectToAction("DanhSachSP", "TrangQuanTri");
                }
                else
                {
                    Session["taikhoan"] = tv;
                    return RedirectToAction("Index", "Home");
                }
            }
            
            return Content("Tên đăng nhập hoặc mật khẩu không đúng");


            //string stendangnhap = f["txtTenDangNhap"].ToString();
            //string smatkhau = f["txtMatKhau"].ToString();
            
            //NguoiDung tv = db.NguoiDungs.SingleOrDefault(x => x.UserName == stendangnhap && x.Password == smatkhau);

            //if (tv != null)
            //{
            //    Session["taikhoan"] = tv;
            //    return RedirectToAction("Index","Home");
            //}

            //return Content("Tên đăng nhập hoặc mật khẩu không đúng");
        }

        public ActionResult DangXuat()
        {
            Session["taikhoanAdmin"] = null;
            Session["taikhoan"] = null;

            return RedirectToAction("Index","Home");
        }
    }
}