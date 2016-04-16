using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MayTinhDaiPhat.Model;

namespace NhapXuat.DAO
{
    public class TinTucDAO
    {
        private readonly MayTinhDaiPhatEntities _db = new MayTinhDaiPhatEntities();

        //Lấy danh sách tin tức
        public IEnumerable<TinTuc> DanhSachTinTuc(string text, int page, int pageSize)
        {
            IQueryable<TinTuc> model = _db.TinTuc;
            if (!string.IsNullOrEmpty(text))
            {
                model = model.Where(x => x.TieuDe.ToString().Contains(text));
            }
            return model.OrderByDescending(x => x.MaTin).ToPagedList(page, pageSize);
        }
        public List<TinTuc> DanhSach()
        {
            return _db.TinTuc.ToList();
        }
        // Thêm tin tức
        public int ThemTinTuc(TinTuc dx)
        {
            _db.TinTuc.Add(dx);
            _db.SaveChanges();
            return dx.MaTin;
        }

        // Xem chi tiết một tin tức
        public TinTuc XemTinTuc(int id)
        {
            return _db.TinTuc.Find(id);
        }

        // Sửa tin tức
        public bool SuaTinTuc(TinTuc dx)
        {
            try
            {
                var ab = _db.TinTuc.Find(dx.MaTin);
                ab.MaTin = dx.MaTin;
                ab.MaDM = dx.MaDM;
                ab.TieuDe = dx.TieuDe;
                ab.HinhAnh = dx.HinhAnh;
                ab.NoiDung = dx.NoiDung;
                ab.LuotXem = dx.LuotXem;
                ab.NgayDang = dx.NgayDang;
                ab.NgayCapNhat = dx.NgayCapNhat;
                ab.HienThi = dx.HienThi;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Xóa  tin tức
        public bool XoaTinTuc(int id)
        {
            try
            {
                var dx = _db.TinTuc.Find(id);
                _db.TinTuc.Remove(dx);
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