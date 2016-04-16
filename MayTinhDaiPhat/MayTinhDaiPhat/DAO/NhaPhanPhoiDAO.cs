using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MayTinhDaiPhat.Model;

namespace NhapXuat.DAO
{
    public class NhaPhanPhoiDAO
    {
        private readonly MayTinhDaiPhatEntities _db = new MayTinhDaiPhatEntities();

        //Lấy danh sách nhà phân phối
        public IEnumerable<NhaPhanPhoi> DanhSachNhaPhanPhoi(string text, int page, int pageSize)
        {
            IQueryable<NhaPhanPhoi> model = _db.NhaPhanPhoi;
            if (!string.IsNullOrEmpty(text))
            {
                model = model.Where(x => x.TenNPP.ToString().Contains(text));
            }
            return model.OrderByDescending(x => x.MaNPP).ToPagedList(page, pageSize);
        }
        public List<NhaPhanPhoi> DanhSach()
        {
            return _db.NhaPhanPhoi.ToList();
        }
        // Thêm nhà phân phối
        public int ThemNhaPhanPhoi(NhaPhanPhoi dx)
        {
            _db.NhaPhanPhoi.Add(dx);
            _db.SaveChanges();
            return dx.MaNPP;
        }

        // Xem chi tiết một nhà phân phối
        public NhaPhanPhoi XemNhaPhanPhoi(int id)
        {
            return _db.NhaPhanPhoi.Find(id);
        }

        // Sửa nhà phân phối
        public bool SuaNhaPhanPhoi(NhaPhanPhoi dx)
        {
            try
            {
                var ab = _db.NhaPhanPhoi.Find(dx.MaNPP);
                ab.MaNPP = dx.MaNPP;
                ab.TenNPP = dx.TenNPP;
                ab.NguoiLienHe = dx.NguoiLienHe;
                ab.DiaChi = dx.DiaChi;
                ab.DienThoai = dx.DienThoai;
                ab.Fax = dx.Fax;
                ab.Email = dx.Email;
                ab.Website = dx.Website;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Xóa  nhà phân phối
        public bool XoaNhaPhanPhoi(int id)
        {
            try
            {
                var dx = _db.NhaPhanPhoi.Find(id);
                _db.NhaPhanPhoi.Remove(dx);
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