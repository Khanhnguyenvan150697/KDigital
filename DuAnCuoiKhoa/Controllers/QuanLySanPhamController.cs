using DuAnCuoiKhoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace DuAnCuoiKhoa.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        DuAnCuoiKhoaEntities db = new DuAnCuoiKhoaEntities(); 

        public ActionResult QuanLySanPham()
        {
            return View(db.SanPhams);
        }


        [HttpGet]
        public ActionResult ThemMoiSP()
        {
            ViewBag.ID_DanhMuc = new SelectList(db.DanhMucSPs.OrderBy(x => x.ID_DanhMuc), "ID_DanhMuc", "TenDanhMuc");
            ViewBag.ID_ThuongHieu = new SelectList(db.ThuongHieux.OrderBy(x => x.ID_ThuongHieu), "ID_ThuongHieu", "TenThuongHieu");
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiSP(SanPham sp, HttpPostedFileBase Avatar)
        {
            ViewBag.ID_DanhMuc = new SelectList(db.DanhMucSPs.OrderBy(x => x.TenDanhMuc), "ID_DanhMuc", "TenDanhMuc");
            ViewBag.ID_ThuongHieu = new SelectList(db.ThuongHieux.OrderBy(x => x.TenThuongHieu), "ID_ThuongHieu", "TenThuongHieu");

            if (Avatar.ContentLength > 0)
            {
                //Lấy tên hình ảnh
                var fileName = Path.GetFileName(Avatar.FileName);

                //lưu hình ảnh vào đường dẫn "~/Content/HinhAnhSP"
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnhSP"), fileName);

                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBaoUpLoad = "Hình ảnh đã tồn tại";
                }
                else
                {
                    //lấy hình ảnh đưa vào thư mục HinhAnhSP
                    Avatar.SaveAs(path);
                    return View();
                }
            }
            db.SanPhams.Add(sp);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public ActionResult ChinhSua(int? id)
        {
            //lấy sản phẩm cần chỉnh sửa dựa vào id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.ID_SanPham == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            ViewBag.ID_DanhMuc = new SelectList(db.DanhMucSPs.OrderBy(x => x.ID_DanhMuc), "ID_DanhMuc", "TenDanhMuc",sp.ID_DanhMuc);
            ViewBag.ID_ThuongHieu = new SelectList(db.ThuongHieux.OrderBy(x => x.ID_ThuongHieu), "ID_ThuongHieu", "TenThuongHieu", sp.ID_ThuongHieu);
            return View();
        }
        [HttpPost]
        public ActionResult ChinhSua(SanPham model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}