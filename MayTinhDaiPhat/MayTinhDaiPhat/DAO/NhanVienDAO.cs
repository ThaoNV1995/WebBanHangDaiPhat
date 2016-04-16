using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MayTinhDaiPhat.Model;

namespace NhapXuat.DAO
{
    public class NhanVienDAO
    {
        private readonly MayTinhDaiPhatEntities _db = new MayTinhDaiPhatEntities();

        //Lấy danh sách nhân viên
        public IEnumerable<NhanVien> DanhSachNhanVien(string text, int page, int pageSize)
        {
            IQueryable<NhanVien> model = _db.NhanVien;
            if (!string.IsNullOrEmpty(text))
            {
                model = model.Where(x => x.TenNV.ToString().Contains(text));
            }
            return model.OrderByDescending(x => x.MaNV).ToPagedList(page, pageSize);
        }
        public List<NhanVien> DanhSach()
        {
            return _db.NhanVien.ToList();
        }

        // Thêm nhân viên
        public int ThemNhanVien(NhanVien dx)
        {
            _db.NhanVien.Add(dx);
            _db.SaveChanges();
            return dx.MaNV;
        }

        // Xem chi tiết một nhân viên
        public NhanVien XemNhanVien(int id)
        {
            return _db.NhanVien.Find(id);
        }

        // Sửa nhân viên
        public bool SuaNhanVien(NhanVien dx)
        {
            try
            {
                var ab = _db.NhanVien.Find(dx.MaNV);
                ab.MaNV = dx.MaNV;
                ab.TenNV = dx.TenNV;
                ab.MaQuyen = dx.MaQuyen;
                ab.TenDangNhap = dx.TenDangNhap;
                ab.MatKhau = dx.MatKhau;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Xóa  nhân viên
        public bool XoaNhanVien(int id)
        {
            try
            {
                var dx = _db.NhanVien.Find(id);
                _db.NhanVien.Remove(dx);
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