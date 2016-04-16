using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MayTinhDaiPhat.Model;

namespace NhapXuat.DAO
{
    public class ChiTietHoaDonNhapDAO
    {
        private readonly MayTinhDaiPhatEntities _db = new MayTinhDaiPhatEntities();
 
        //Lấy danh sách chi tiết hóa đơn nhập
        public IEnumerable<ChiTietHoaDonNhap> DanhSachChiTietHoaDonNhap(string text, int page, int pageSize)
        {
            IQueryable<ChiTietHoaDonNhap> model = _db.ChiTietHoaDonNhap;
            if (!string.IsNullOrEmpty(text))
            {
                model = model.Where(x => x.DonGia.ToString().Contains(text));
            }
            return model.OrderByDescending(x => x.DonGia).ToPagedList(page, pageSize);
        }

        public List<ChiTietHoaDonNhap> DanhSach()
        {
            return _db.ChiTietHoaDonNhap.ToList();
        }
        
        //Thêm chi tiết hóa đơn nhập
        public int ThemChiTietHoaDonNhap(ChiTietHoaDonNhap ct)
        {
            _db.ChiTietHoaDonNhap.Add(ct);
            _db.SaveChanges();
            return ct.MaCTHDN;
        }

        //Xem chi tiết 1 hóa đơn
        public ChiTietHoaDonNhap XemChiTiet(int id)
        {
            return _db.ChiTietHoaDonNhap.Find(id);
        }

        // Sửa chi tiết hóa đơn
        public bool CapNhatChiTietHoaDon(ChiTietHoaDonNhap ct)
        {
            try {
                var ab = _db.ChiTietHoaDonNhap.Find(ct.MaCTHDN);
                ab.MaCTHDN = ct.MaCTHDN;
                ab.HoaDonNhap = ct.HoaDonNhap;
                ab.MaHDN = ct.MaHDN;
                ab.MaSP = ct.MaSP;
                ab.SoLuong = ct.SoLuong;
                ab.DonGia = ct.DonGia;
                ab.ChietKhau = ct.ChietKhau;
                _db.SaveChanges();
                return true;
            }
            catch(Exception) {
                return false;
            }
        }

        // Xóa chi tiết hóa đơn
        public bool XoaChiTietHoaDonNhap(int id)
        {
            try {
                var ct = _db.ChiTietHoaDonNhap.Find(id);
                _db.ChiTietHoaDonNhap.Remove(ct);
                _db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}