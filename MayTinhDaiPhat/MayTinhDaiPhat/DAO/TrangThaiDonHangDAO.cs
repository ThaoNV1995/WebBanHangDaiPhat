using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MayTinhDaiPhat.Model;

namespace MayTinhDaiPhat.DAO
{
    public class TrangThaiDonHangDAO
    {
        private readonly MayTinhDaiPhatEntities _db = new MayTinhDaiPhatEntities();

        //Lấy danh sách trạng thái đơn hàng
        public IEnumerable<TrangThaiDonHang> DanhSachTrangThaiDonHang(string text, int page, int pageSize)
        {
            IQueryable<TrangThaiDonHang> model = _db.TrangThaiDonHang;
            if (!string.IsNullOrEmpty(text))
            {
                model = model.Where(x => x.TenTTDH.ToString().Contains(text));
            }
            return model.OrderByDescending(x => x.MaTTDH).ToPagedList(page, pageSize);
        }

        // Thêm trạng thái đơn hàng
        public int ThemTrangThaiDonHang(TrangThaiDonHang dx)
        {
            _db.TrangThaiDonHang.Add(dx);
            _db.SaveChanges();
            return dx.MaTTDH;
        }

        // Xem chi tiết một trạng thái đơn hàng
        public TrangThaiDonHang XemTrangThaiDonHang(int id)
        {
            return _db.TrangThaiDonHang.Find(id);
        }

        // Sửa trạng thái đơn hàng
        public bool SuaTrangThaiDonHang(TrangThaiDonHang dx)
        {
            try
            {
                var ab = _db.TrangThaiDonHang.Find(dx.MaTTDH);
                ab.MaTTDH = dx.MaTTDH;
                ab.TenTTDH = dx.TenTTDH;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Xóa  trạng thái đơn hàng
        public bool XoaTrangThaiDonHang(int id)
        {
            try
            {
                var dx = _db.TrangThaiDonHang.Find(id);
                _db.TrangThaiDonHang.Remove(dx);
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