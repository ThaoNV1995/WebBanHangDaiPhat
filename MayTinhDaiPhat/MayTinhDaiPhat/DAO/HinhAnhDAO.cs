using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MayTinhDaiPhat.Model;

namespace NhapXuat.DAO
{
    public class HinhAnhDAO
    {
        private readonly MayTinhDaiPhatEntities _db = new MayTinhDaiPhatEntities();

        //Lấy danh sách hình ảnh
        public IEnumerable<HinhAnh> DanhSachHinhAnh(string text, int page, int pageSize)
        {
            IQueryable<HinhAnh> model = _db.HinhAnh;
            if (!string.IsNullOrEmpty(text))
            {
                model = model.Where(x => x.MaSP.ToString().Contains(text));
            }
            return model.OrderByDescending(x => x.MaHA).ToPagedList(page, pageSize);
        }
        public List<HinhAnh> DanhSach()
        {
            return _db.HinhAnh.ToList();
        }
        // Thêm hình ảnh
        public int ThemHinhAnh(HinhAnh dx)
        {
            _db.HinhAnh.Add(dx);
            _db.SaveChanges();
            return dx.MaHA;
        }

        // Xem chi tiết một hình ảnh
        public HinhAnh XemHinhAnh(int id)
        {
            return _db.HinhAnh.Find(id);
        }

        // Sửa xóa hình ảnh
        public bool SuaHinhAnh(HinhAnh dx)
        {
            try
            {
                var ab = _db.HinhAnh.Find(dx.MaHA);
                ab.MaHA = dx.MaHA;
                ab.MaSP = dx.MaSP;
                ab.AnhSP = dx.AnhSP;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Xóa  hình ảnh
        public bool XoaHinhAnh(int id)
        {
            try
            {
                var dx = _db.HinhAnh.Find(id);
                _db.HinhAnh.Remove(dx);
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