using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DuAnCuoiKhoa.Models;

namespace DuAnCuoiKhoa.Controllers
{
    public class GioHangController : Controller
    {

        DuAnCuoiKhoaEntities db = new DuAnCuoiKhoaEntities();
        
        // Phương thức lấy giỏ hàng
        public List<ItemGioHang> LayGioHang()
        {            
            //Giỏ hàng đã tồn tại
            List<ItemGioHang> lstGiohang = (List<ItemGioHang>)Session["GioHang"];

            if(lstGiohang == null)
            {
                //Nếu session GioHang chưa tồn tại thì khởi tạo giỏ hàng
                lstGiohang = new List<ItemGioHang>();
                Session["GioHang"] = lstGiohang;
            }
            return lstGiohang;

        }
        // Phương thức thêm item giỏ hàng bằng load lại trang
        public ActionResult ThemGioHang(int ID_SanPham, string strURL)
        {
            //Kiểm tra sản phẩm có tồn tại tròn csdl hay không
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.ID_SanPham == ID_SanPham);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng
            List<ItemGioHang> lstGioHang = LayGioHang();

            //Nếu sản phẩm đã tồn tại trong giỏ hàng
            ItemGioHang spcheck = lstGioHang.SingleOrDefault(x => x.ID_SanPham == ID_SanPham);
            if (spcheck != null)
            {
                //Kiểm tra số lượng tồn trước khi cho khách đặt mua
                if (sp.SoLuongTon < spcheck.SoLuong)
                {
                    return View("ThongBao");
                }
                spcheck.SoLuong++;
                spcheck.ThanhTien = spcheck.SoLuong * spcheck.DonGia;
                return Redirect(strURL);
            }
            ItemGioHang itemGH = new ItemGioHang(ID_SanPham);
            if (sp.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            lstGioHang.Add(itemGH);
            return Redirect(strURL);
        }

        //Phương thức tính tổng số lượng
        public double TinhTongSoLuong()
        {
            //Lấy giỏ hàng
            List<ItemGioHang> lstGioHang = (List<ItemGioHang>)Session["GioHang"];
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(x => x.SoLuong);
        }
        //Phương thức tính tổng tiền
        public decimal TinhTongTien()
        {
            List<ItemGioHang> lstGioHang = (List<ItemGioHang>)Session["GioHang"];
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(x => x.ThanhTien);
        }
        //Giỏ hàng header
        public ActionResult GioHangPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }

            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien()
;
            return PartialView();
        }

        // Xem giỏ hàng
        public ActionResult XemGioHang()
        {
            //lấy giỏ hàng

            List<ItemGioHang> lstGioHang = LayGioHang();
            decimal phiVanChuyen = 40000;
            ViewBag.TongTien = TinhTongTien() + phiVanChuyen;
            ViewBag.TongSoLuong = TinhTongSoLuong();
            return View(lstGioHang);     
        }

        // Chỉnh sửa giỏ hàng
        [HttpGet]
        public ActionResult ChinhSuaGioHang(int? ID_SanPham)
        {
            // Kiểm tra session GioHang đã tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Kiểm tra sản phẩm đã tồn tại trong csdl hay chưa            
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.ID_SanPham == ID_SanPham);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //Lấy ra list GioHang từ session GioHang
            List<ItemGioHang> lstGioHang = LayGioHang();

            //Kiểm tra sản phẩm có tồn tại trong lstGioHang hay chưa
            ItemGioHang spcheck = lstGioHang.SingleOrDefault(x => x.ID_SanPham == ID_SanPham);

            if (spcheck == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Lấy lstGioHang để tạo giao diện
            ViewBag.GIoHang = lstGioHang;

            // Nếu sản phẩm đã tồn tại trong giỏ hàng thì return về model
            return View(spcheck);
        }
        [HttpPost]
        public ActionResult CapNhatGioHang(ItemGioHang itemGH)
        {
            //Kiểm tra số lượng tồn
            SanPham spcheck = db.SanPhams.Single(x => x.ID_SanPham == itemGH.ID_SanPham);

            if (spcheck.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            //Cập nhật số lượng trong session GioHang

            //Lấy list GioHang từ Session["GioHang"]
            List<ItemGioHang> lstGH = LayGioHang();

            //Lấy sản phẩm từ List<GioHang> ra
            ItemGioHang itemGHUpdate = lstGH.Find(x => x.ID_SanPham == itemGH.ID_SanPham);

            //Cập nhật lại số lượng sản phẩm
            itemGHUpdate.SoLuong = itemGH.SoLuong;

            //Cập nhật lại thành tiền
            itemGHUpdate.ThanhTien = itemGHUpdate.SoLuong * itemGHUpdate.DonGia;

            return RedirectToAction("XemGioHang");
        }

        //Xóa giỏ hàng
        public ActionResult XoaGioHang(int ID_SanPham)
        {

            // Kiểm tra session GioHang đã tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Kiểm tra sản phẩm đã tồn tại trong csdl hay chưa
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.ID_SanPham == ID_SanPham);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //Lấy ra list GioHang từ session GioHang
            List<ItemGioHang> lstGioHang = LayGioHang();

            //Kiểm tra sản phẩm có tồn tại trong lstGioHang hay chưa
            ItemGioHang spcheck = lstGioHang.SingleOrDefault(x => x.ID_SanPham == ID_SanPham);

            if (spcheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Xóa item giỏ hàng ra khỏi lstGIoHang
            lstGioHang.Remove(spcheck);

            return RedirectToAction("XemGioHang");
        }

        //Xây dựng chức năng đặt hàng
        public ActionResult DatHang(KhachHang kh)
        {
            // Kiểm tra session GioHang đã tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            KhachHang khang = new KhachHang();
            if (Session["TaiKhoan"] == null)
            {
                //Thêm khách hàng vào bảng khách hàng đối với khách hàng chưa có tài khoản
                khang = kh;
                db.KhachHangs.Add(khang);
                db.SaveChanges();
            }
            else
            {
                //Đối với khách hàng là thành viên
                NguoiDung tv = (NguoiDung)Session["TaiKhoan"];
                khang.HoVaTen = tv.HoVaTen;
                khang.DiaChi = tv.DiaChi;
                khang.Email = tv.Email;
                khang.SoDienThoai = tv.SoDienThoai;
                db.KhachHangs.Add(khang);
                db.SaveChanges();
            }

            //Đối với tài khoản admin
            if (Session["taikhoanAdmin"] == null)
            {
                khang = kh;
                db.KhachHangs.Add(khang);
                db.SaveChanges();
            }
            else
            {
                NguoiDung admin = (NguoiDung)Session["taikhoanAdmin"];
                khang.HoVaTen = admin.HoVaTen;
                khang.DiaChi = admin.DiaChi;
                khang.Email = admin.Email;
                khang.SoDienThoai = admin.SoDienThoai;
                db.KhachHangs.Add(khang);
                db.SaveChanges();
            }

            //Thêm đơn hàng
            DonDatHang ddh = new DonDatHang();
            ddh.ID_KhachHang = khang.ID_KhachHang; 
            ddh.NgayDatHang = DateTime.Now;
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            ddh.DaHuy = false;
            ddh.DaXoa = false;

            db.DonDatHangs.Add(ddh);
            db.SaveChanges();

            //Thêm chi tiết đơn đặt hàng
            List<ItemGioHang> lstGH = LayGioHang();
            foreach (var item in lstGH)
            {
                ChiTietDonDatHang ctdh = new ChiTietDonDatHang();
                ctdh.ID_DonDatHang = ddh.ID_DonDatHang;
                ctdh.ID_SanPham = item.ID_SanPham;
                ctdh.TenSanPham = item.TenSanPham;
                ctdh.SoLuong = item.SoLuong;
                ctdh.DonGia = item.DonGia;
                db.ChiTietDonDatHangs.Add(ctdh);
            }
            db.SaveChanges();

            Session["GioHang"] = null;

            return RedirectToAction("XemGioHang");
        }
    }
}
