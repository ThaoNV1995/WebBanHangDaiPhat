using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MayTinhDaiPhat.Model;

namespace NhapXuat.DAO
{
    public class DanhMucDAO
    {
        private readonly MayTinhDaiPhatEntities _db = new MayTinhDaiPhatEntities();

        //Lấy danh sách danh mục
        public IEnumerable<DanhMuc> DanhSachDanhMuc(string text, int page, int pageSize)
        {
            IQueryable<DanhMuc> model = _db.DanhMuc;
            if (!string.IsNullOrEmpty(text))
            {
                model = model.Where(x => x.TenDM.ToString().Contains(text));
            }
            return model.OrderByDescending(x => x.MaDM).ToPagedList(page, pageSize);
        }
        public List<DanhMuc> DanhSach()
        {
            return _db.DanhMuc.ToList();
        }
        // Thêm Danh mục
        public int ThemDanhMuc(DanhMuc dx)
        {
            _db.DanhMuc.Add(dx);
            _db.SaveChanges();
            return dx.MaDM;
        }

        // Xem chi tiết một danh mục
        public DanhMuc XemDanhMuc(int id)
        {
            return _db.DanhMuc.Find(id);
        }

        // Sửa danh mục
        public bool SuaDanhMuc(DanhMuc dx)
        {
            try
            {
                var ab = _db.DanhMuc.Find(dx.MaDM);
                ab.MaDM = dx.MaDM;
                ab.MaCha = dx.MaCha;
                ab.TenDM = dx.TenDM;
                ab.Icon = dx.Icon;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Xóa  danh mục
        public bool XoaDanhMuc(int id)
        {
            try
            {
                var dx = _db.DanhMuc.Find(id);
                _db.DanhMuc.Remove(dx);
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