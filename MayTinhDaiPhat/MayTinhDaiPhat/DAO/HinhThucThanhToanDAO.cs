using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MayTinhDaiPhat.Model;

namespace MayTinhDaiPhat.DAO
{
    public class HinhThucThanhToanDAO
    {
        private readonly MayTinhDaiPhatEntities _db = new MayTinhDaiPhatEntities();

        //Lấy danh sách hình thức thanh toán
        public IEnumerable<HinhThucThanhToan> DanhSachHinhThucThanhToan(string text, int page, int pageSize)
        {
            IQueryable<HinhThucThanhToan> model = _db.HinhThucThanhToan;
            if (!string.IsNullOrEmpty(text))
            {
                model = model.Where(x => x.TenHTTT.ToString().Contains(text));
            }
            return model.OrderByDescending(x => x.MaHTTT).ToPagedList(page, pageSize);
        }

        // Thêm hình thức thanh toán
        public int ThemHinhThucThanhToan(HinhThucThanhToan dx)
        {
            _db.HinhThucThanhToan.Add(dx);
            _db.SaveChanges();
            return dx.MaHTTT;
        }

        // Xem chi tiết một hình thức thanh toán
        public HinhThucThanhToan XemHinhThucThanhToan(int id)
        {
            return _db.HinhThucThanhToan.Find(id);
        }

        // Sửa hình thức thanh toán
        public bool SuaHinhThucThanhToan(HinhThucThanhToan dx)
        {
            try
            {
                var ab = _db.HinhThucThanhToan.Find(dx.MaHTTT);
                ab.MaHTTT = dx.MaHTTT;
                ab.TenHTTT = dx.TenHTTT;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Xóa  hình thức thanh toán
        public bool XoaHinhThucThanhToan(int id)
        {
            try
            {
                var dx = _db.HinhThucThanhToan.Find(id);
                _db.HinhThucThanhToan.Remove(dx);
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