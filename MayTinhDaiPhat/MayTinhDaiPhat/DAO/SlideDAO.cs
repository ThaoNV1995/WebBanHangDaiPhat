using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MayTinhDaiPhat.Model;

namespace NhapXuat.DAO
{
    public class SlideDAO
    {
        //private readonly DataContext _db = new DataContext();
        private readonly MayTinhDaiPhatEntities _db = new MayTinhDaiPhatEntities();
        //Lấy danh sách slide
        public IEnumerable<Slide> DanhSachSlide(string text, int page, int pageSize)
        {
            IQueryable<Slide> model = _db.Slide;
            if (!string.IsNullOrEmpty(text))
            {
                model = model.Where(x => x.TenSlide.ToString().Contains(text));
            }
            return model.OrderByDescending(x => x.MaSlide).ToPagedList(page, pageSize);
        }
        public List<Slide> DanhSach()
        {
            return _db.Slide.ToList();
        }
        // Thêm slide
        public int ThemSlide(Slide dx)
        {
            _db.Slide.Add(dx);
            _db.SaveChanges();
            return dx.MaSlide;
        }

        // Xem chi tiết một slide
        public Slide XemSlide(int id)
        {
            return _db.Slide.Find(id);
        }

        // Sửa slide
        public bool SuaSlide(Slide dx)
        {
            try
            {
                var ab = _db.Slide.Find(dx.MaSlide);
                ab.MaSlide = dx.MaSlide;
                ab.TenSlide = dx.TenSlide;
                ab.AnhSlide = dx.AnhSlide;
                ab.STT = dx.STT;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Xóa  slide
        public bool XoaSlide(int id)
        {
            try
            {
                var dx = _db.Slide.Find(id);
                _db.Slide.Remove(dx);
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