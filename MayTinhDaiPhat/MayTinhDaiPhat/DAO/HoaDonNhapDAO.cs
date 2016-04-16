using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web;
using PagedList;
using MayTinhDaiPhat.Model;

namespace NhapXuat.DAO
{
    public class HoaDonNhapDAO
    {
        private readonly MayTinhDaiPhatEntities _db = new MayTinhDaiPhatEntities();

        //Lấy danh sách hóa đơn nhập
        public IEnumerable<HoaDonNhap> DanhSachHoaDonNhap(string text, int page, int pageSize)
        {
            IQueryable<HoaDonNhap> model = _db.HoaDonNhap;
            if (!string.IsNullOrEmpty(text))
            {
                model = model.Where(x => x.MaNV.ToString().Contains(text));
            }
            return model.OrderByDescending(x => x.MaHDN).ToPagedList(page, pageSize);
        }
        public List<HoaDonNhap> DanhSach()
        {
            return _db.HoaDonNhap.ToList();
        }
        // Thêm hóa đơn nhập
        public int ThemHoaDonNhap(HoaDonNhap dx)
        {
            _db.HoaDonNhap.Add(dx);
            _db.SaveChanges();
            return dx.MaHDN;
        }

        // Xem chi tiết một hóa đơn nhập
        public HoaDonNhap XemHoaDonNhap(int id)
        {
            return _db.HoaDonNhap.Find(id);
        }

        // Sửa xóa hóa đơn nhập
        public bool SuaHoaDonNhap(HoaDonNhap dx)
        {
            try
            {
                var ab = _db.HoaDonNhap.Find(dx.MaHDN);
                ab.MaHDN = dx.MaHDN;
                ab.MaNPP = dx.MaNPP;
                ab.MaNV = dx.MaNV;
                ab.NgayNhap = dx.NgayNhap;
                ab.NhanVien = dx.NhanVien;
                ab.NhaPhanPhoi = dx.NhaPhanPhoi;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Xóa  hóa đơn nhập
        public bool XoaHoaDonNhap(int id)
        {
            try
            {
                var dx = _db.HoaDonNhap.Find(id);
                _db.HoaDonNhap.Remove(dx);
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