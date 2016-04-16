using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MayTinhDaiPhat.Model;

namespace NhapXuat.DAO
{
    public class QuyenDAO
    {
        private readonly MayTinhDaiPhatEntities _db = new MayTinhDaiPhatEntities();

        //Lấy danh sách quyền
        public IEnumerable<Quyen> DanhSachQuyen(string text, int page, int pageSize)
        {
            IQueryable<Quyen> model = _db.Quyen;
            if (!string.IsNullOrEmpty(text))
            {
                model = model.Where(x => x.TenQuyen.ToString().Contains(text));
            }
            return model.OrderByDescending(x => x.MaQuyen).ToPagedList(page, pageSize);
        }

        // Thêm quyền
        public int ThemQuyen(Quyen dx)
        {
            _db.Quyen.Add(dx);
            _db.SaveChanges();
            return dx.MaQuyen;
        }

        // Xem chi tiết một quyền
        public Quyen XemQuyen(int id)
        {
            return _db.Quyen.Find(id);
        }

        // Sửa quyền
        public bool SuaQuyen(Quyen dx)
        {
            try
            {
                var ab = _db.Quyen.Find(dx.MaQuyen);
                ab.MaQuyen = dx.MaQuyen;
                ab.TenQuyen = dx.TenQuyen;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Xóa  quyền
        public bool XoaQuyen(int id)
        {
            try
            {
                var dx = _db.Quyen.Find(id);
                _db.Quyen.Remove(dx);
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