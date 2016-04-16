using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MayTinhDaiPhat.Model;

namespace NhapXuat.DAO
{
    public class QuangCaoDAO
    {
        private readonly MayTinhDaiPhatEntities _db = new MayTinhDaiPhatEntities();

        //Lấy danh sách quảng cáo
        public IEnumerable<QuangCao> DanhSachQuangCao(string text, int page, int pageSize)
        {
            IQueryable<QuangCao> model = _db.QuangCao;
            if (!string.IsNullOrEmpty(text))
            {
                model = model.Where(x => x.TenQC.ToString().Contains(text));
            }
            return model.OrderByDescending(x => x.MaQC).ToPagedList(page, pageSize);
        }
        public List<QuangCao> DanhSach()
        {
            return _db.QuangCao.ToList();
        }
        // Thêm quảng cáo
        public int ThemQuangCao(QuangCao dx)
        {
            _db.QuangCao.Add(dx);
            _db.SaveChanges();
            return dx.MaQC;
        }

        // Xem chi tiết một quảng cáo
        public QuangCao XemQuangCao(int id)
        {
            return _db.QuangCao.Find(id);
        }

        // Sửa quảng cáo
        public bool SuaQuangCao(QuangCao dx)
        {
            try
            {
                var ab = _db.QuangCao.Find(dx.MaQC);
                ab.MaQC = dx.MaQC;
                ab.TenQC = dx.TenQC;
                ab.AnhQC = dx.AnhQC;
                ab.STT = dx.STT;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Xóa  quảng cáo
        public bool XoaQuangCao(int id)
        {
            try
            {
                var dx = _db.QuangCao.Find(id);
                _db.QuangCao.Remove(dx);
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