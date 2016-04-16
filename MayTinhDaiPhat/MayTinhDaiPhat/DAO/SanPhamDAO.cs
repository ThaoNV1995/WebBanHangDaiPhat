using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MayTinhDaiPhat.Model;

namespace NhapXuat.DAO
{
    public class SanPhamDAO
    {
        private readonly MayTinhDaiPhatEntities _db = new MayTinhDaiPhatEntities();

        //Lấy danh sách sản phẩm
        public IEnumerable<SanPham> DanhSachSanPham(string text, int page, int pageSize)
        {
            IQueryable<SanPham> model = _db.SanPham;
            if (!string.IsNullOrEmpty(text))
            {
                model = model.Where(x => x.TenSP.ToString().Contains(text));
            }
            return model.OrderByDescending(x => x.MaSP).ToPagedList(page, pageSize);
        }
        public List<SanPham> DanhSach()
        {
            return _db.SanPham.ToList();
        }
        // Thêm sản phẩm
        public int ThemSanPham(SanPham dx)
        {
            _db.SanPham.Add(dx);
            _db.SaveChanges();
            return dx.MaSP;
        }

        // Xem chi tiết một sản phẩm
        public SanPham XemSanPham(int id)
        {
            return _db.SanPham.Find(id);
        }

        // Sửa sản phẩm
        public bool SuaSanPham(SanPham dx)
        {
            try
            {
                var ab = _db.SanPham.Find(dx.MaSP);
                ab.MaSP = dx.MaSP;
                ab.MaTH = dx.MaTH;
                ab.MaDM = dx.MaDM;
                ab.TenSP = dx.TenSP;
                ab.AnhSP = dx.AnhSP;
                ab.MoTa = dx.MoTa;
                ab.KhuyenMai = dx.KhuyenMai;
                ab.ChiTiet = dx.ChiTiet;
                ab.GiaBan = dx.GiaBan;
                ab.GiaCu = dx.GiaCu;
                ab.BaoHanh = dx.BaoHanh;
                ab.SoLuong = dx.SoLuong;
                ab.LuotXem = dx.LuotXem;
                ab.NgayDang = dx.NgayDang;
                ab.NgayCapNhat = DateTime.Now;
                ab.HienThi = dx.HienThi;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Xóa  sản phẩm
        public bool XoaSanPham(int id)
        {
            try
            {
                var dx = _db.SanPham.Find(id);
                _db.SanPham.Remove(dx);
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