using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DuAnCuoiKhoa.Models;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace DuAnCuoiKhoa.Controllers
{
    public class TrangQuanTriController : Controller
    {
        DuAnCuoiKhoaEntities db = new DuAnCuoiKhoaEntities();

        public ActionResult DanhSachSP(int page = 1)
        { 
            return View(db.SanPhams.OrderBy(x=>x.ID_SanPham).ToPagedList(page,10));
        }

        //thêm mới sản phẩm
        [HttpGet]
        public ActionResult ThemMoiSP()
        {
            ViewBag.ID_DanhMuc = new SelectList(db.DanhMucSPs.OrderBy(x => x.TenDanhMuc), "ID_DanhMuc", "TenDanhMuc");
            ViewBag.ID_ThuongHieu = new SelectList(db.ThuongHieux.OrderBy(x => x.TenThuongHieu), "ID_ThuongHieu", "TenThuongHieu");
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ThemMoiSP(SanPham sp ,HttpPostedFileBase upload) // upload trùng tên với name của thẻ <input />
        {
            ViewBag.ID_DanhMuc = new SelectList(db.DanhMucSPs.OrderBy(x => x.TenDanhMuc),"ID_DanhMuc", "TenDanhMuc");
            ViewBag.ID_ThuongHieu = new SelectList(db.ThuongHieux.OrderBy(x => x.TenThuongHieu), "ID_ThuongHieu", "TenThuongHieu");
            
            
            if (upload.ContentLength > 0)
            {
                var fileName = Path.GetFileName(upload.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnhSP"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    return View();
                }
                else
                {
                    upload.SaveAs(path);
                    sp.Avatar = fileName;
                }
            }
            
            db.SanPhams.Add(sp);
            db.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult ChinhSuaSP(int? ID_SanPham)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.ID_SanPham == ID_SanPham);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.ID_DanhMuc = new SelectList(db.DanhMucSPs.OrderBy(x => x.TenDanhMuc), "ID_DanhMuc", "TenDanhMuc", sp.ID_DanhMuc);
            ViewBag.ID_ThuongHieu = new SelectList(db.ThuongHieux.OrderBy(x => x.TenThuongHieu), "ID_ThuongHieu", "TenThuongHieu",sp.ID_ThuongHieu);
            return View(sp);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSuaSP(SanPham sp)
        {
            
            //thêm vào csdl
            if (ModelState.IsValid)
            {
                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.ID_DanhMuc = new SelectList(db.DanhMucSPs.OrderBy(x => x.TenDanhMuc), "ID_DanhMuc", "TenDanhMuc", sp.ID_DanhMuc);
            ViewBag.ID_ThuongHieu = new SelectList(db.ThuongHieux.OrderBy(x => x.TenThuongHieu), "ID_ThuongHieu", "TenThuongHieu", sp.ID_ThuongHieu);
            return RedirectToAction("DanhSachSP");
        }
        public ActionResult XoaSP(int? id)
        {
            var sp = db.SanPhams.Find(id);
            if (sp != null)
            {
                db.SanPhams.Remove(sp);
                db.SaveChanges();
            }
            return RedirectToAction("DanhSachSP");
        }
    }
}