using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MayTinhDaiPhat.Model;

namespace NhapXuat.DAO
{
    public class KhachHangDAO
    {
        private readonly MayTinhDaiPhatEntities _db = new MayTinhDaiPhatEntities();

        //Lấy danh sách khách hàng
        public IEnumerable<KhachHang> DanhSachKhachHang(string text, int page, int pageSize)
        {
            IQueryable<KhachHang> model = _db.KhachHang;
            if (!string.IsNullOrEmpty(text))
            {
                model = model.Where(x => x.TenKH.ToString().Contains(text));
            }
            return model.OrderByDescending(x => x.MaKH).ToPagedList(page, pageSize);
        }

        // Thêm khách hàng
        public int ThemKhachHang(KhachHang dx)
        {
            _db.KhachHang.Add(dx);
            _db.SaveChanges();
            return dx.MaKH;
        }

        // Xem chi tiết một khách hàng
        public KhachHang XemKhachHang(int id)
        {
            return _db.KhachHang.Find(id);
        }

        // Sửa khách hàng
        public bool SuaKhachHang(KhachHang dx)
        {
            try
            {
                var ab = _db.KhachHang.Find(dx.MaKH);
                ab.MaKH = dx.MaKH;
                ab.TenKH = dx.TenKH;
                ab.DiaChi = dx.DiaChi;
                ab.DienThoai = dx.DienThoai;
                ab.Email = dx.Email;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Xóa  khách hàng
        public bool XoaKhachHang(int id)
        {
            try
            {
                var dx = _db.KhachHang.Find(id);
                _db.KhachHang.Remove(dx);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}