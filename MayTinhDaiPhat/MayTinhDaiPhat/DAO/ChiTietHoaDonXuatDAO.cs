using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MayTinhDaiPhat.Model;

namespace NhapXuat.DAO
{
    public class ChiTietHoaDonXuatDAO
    {
        private readonly MayTinhDaiPhatEntities _db = new MayTinhDaiPhatEntities();

        //Lấy danh sách hóa đơn xuất
        public IEnumerable<ChiTietHoaDonXuat> DanhSachChiTietHoaDonXuat(string text, int page, int pageSize)
        {
            IQueryable<ChiTietHoaDonXuat> model = _db.ChiTietHoaDonXuat;
            if (!string.IsNullOrEmpty(text))
            {
                model = model.Where(x => x.DonGia.ToString().Contains(text));
            }
            return model.OrderByDescending(x => x.MaHDX).ToPagedList(page, pageSize);
        }
        public List<ChiTietHoaDonXuat> DanhSach()
        {
            return _db.ChiTietHoaDonXuat.ToList();
        }
        // Thêm Hóa đơn xuất
        public int ThemChiTietHoaDonXuat(ChiTietHoaDonXuat dx)
        {
            _db.ChiTietHoaDonXuat.Add(dx);
            _db.SaveChanges();
            return (int)dx.MaHDX;
        }

        // Xem chi tiết một hóa đơn xuất
        public ChiTietHoaDonXuat XemChiTietHoaDonXuat(int id)
        {
            return _db.ChiTietHoaDonXuat.Find(id);
        }

        // Sửa xóa đơn xuất
        public bool SuaChiTietHoaDonXuat(ChiTietHoaDonXuat dx)
        {
            try 
            {
                var ab = _db.ChiTietHoaDonXuat.Find(dx.MaHDX);
                ab.MaHDX = dx.MaHDX;
                ab.MaSP = dx.MaSP;
                ab.SoLuong = dx.SoLuong;
                ab.HoaDonXuat = dx.HoaDonXuat;
                _db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        //Xóa chi tiết hóa đơn xuất
        public bool XoaChiTietHoaDonXuat(int id)
        {
            try {
                var dx = _db.ChiTietHoaDonXuat.Find(id);
                _db.ChiTietHoaDonXuat.Remove(dx);
                _db.SaveChanges();
                return true;
            }
            catch(Exception) {
                return false;
            }
        }
    }
}