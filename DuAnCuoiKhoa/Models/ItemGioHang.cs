using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DuAnCuoiKhoa.Models
{
    public class ItemGioHang
    {
        public int ID_SanPham { get; set; }

        public string TenSanPham { get; set; }

        public int SoLuong { get; set; }

        public decimal DonGia { get; set; }

        public string HinhAnh { get; set; }

        public decimal ThanhTien { get; set; }

        public ItemGioHang(int id_sp)
        {
            using (DuAnCuoiKhoaEntities db = new DuAnCuoiKhoaEntities())
            {
                this.ID_SanPham = id_sp;
                SanPham sp = db.SanPhams.Single(x => x.ID_SanPham == id_sp);
                this.TenSanPham = sp.TenSanPham;
                this.HinhAnh = sp.Avatar;
                this.DonGia = sp.GiaGoc.Value;
                this.SoLuong = 1;
                this.ThanhTien = DonGia * SoLuong;

            }
        }
        public ItemGioHang(int id_sp, int sl)
        {
            using (DuAnCuoiKhoaEntities db = new DuAnCuoiKhoaEntities())
            {
                this.ID_SanPham = id_sp;
                SanPham sp = db.SanPhams.Single(x => x.ID_SanPham == id_sp);
                this.TenSanPham = sp.TenSanPham;
                this.HinhAnh = sp.Avatar;
                this.DonGia = sp.GiaGoc.Value;
                this.SoLuong = sl;
                this.ThanhTien = DonGia * SoLuong;

            }
        }
        public ItemGioHang()
        {

        }
    }
}