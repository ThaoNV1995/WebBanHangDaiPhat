//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MayTinhDaiPhat.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class TinTuc
    {
        public int MaTin { get; set; }
        public Nullable<int> MaDM { get; set; }
        public string TieuDe { get; set; }
        public string HinhAnh { get; set; }
        public string NoiDung { get; set; }
        public Nullable<int> LuotXem { get; set; }
        public Nullable<System.DateTime> NgayDang { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public Nullable<bool> HienThi { get; set; }
    
        public virtual DanhMuc DanhMuc { get; set; }
    }
}
