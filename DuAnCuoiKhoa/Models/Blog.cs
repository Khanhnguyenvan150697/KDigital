//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DuAnCuoiKhoa.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Blog
    {
        public int ID_Blog { get; set; }
        public string TieuDe { get; set; }
        public string Image { get; set; }
        public string NoiDung { get; set; }
        public Nullable<System.DateTime> NgayDang { get; set; }
        public string Hastag { get; set; }
        public string NguoiVietBai { get; set; }
    }
}