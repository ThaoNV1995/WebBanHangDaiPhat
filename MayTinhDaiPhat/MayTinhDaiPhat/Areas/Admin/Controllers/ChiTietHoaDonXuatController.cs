using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MayTinhDaiPhat.Model;
using NhapXuat.DAO;

namespace MayTinhDaiPhat.Areas.Admin.Controllers
{
    public class ChiTietHoaDonXuatController : Controller
    {
        private readonly MayTinhDaiPhatEntities db = new MayTinhDaiPhatEntities();
        private readonly ChiTietHoaDonXuatDAO dao = new ChiTietHoaDonXuatDAO();

        // GET: /Admin/ChiTietHoaDonXuat/
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var list = dao.DanhSachChiTietHoaDonXuat(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(list);
        }
        //public ActionResult Index()
        //{
        //    return View(dao.DanhSach());
        //}
        // GET: /Admin/ChiTietHoaDonXuat/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDonXuat chitiethoadonxuat = dao.XemChiTietHoaDonXuat(id);
            if (chitiethoadonxuat == null)
            {
                return HttpNotFound();
            }
            return View(chitiethoadonxuat);
        }

        // GET: /Admin/ChiTietHoaDonXuat/Create
        public ActionResult Create()
        {
            ViewBag.MaHDX = new SelectList(db.HoaDonXuat, "MaHDX", "MaHDX");
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MaCTHDX,MaHDX,MaSP,SoLuong,DonGia,ChietKhau")] ChiTietHoaDonXuat chitiethoadonxuat)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietHoaDonXuat.Add(chitiethoadonxuat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHDX = new SelectList(db.HoaDonXuat, "MaHDX", "MaHDX", chitiethoadonxuat.MaHDX);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chitiethoadonxuat.MaSP);
            return View(chitiethoadonxuat);
        }

        // GET: /Admin/ChiTietHoaDonXuat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDonXuat chitiethoadonxuat = db.ChiTietHoaDonXuat.Find(id);
            if (chitiethoadonxuat == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHDX = new SelectList(db.HoaDonXuat, "MaHDX", "MaHDX", chitiethoadonxuat.MaHDX);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chitiethoadonxuat.MaSP);
            return View(chitiethoadonxuat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MaCTHDX,MaHDX,MaSP,SoLuong,DonGia,ChietKhau")] ChiTietHoaDonXuat chitiethoadonxuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chitiethoadonxuat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHDX = new SelectList(db.HoaDonXuat, "MaHDX", "MaHDX", chitiethoadonxuat.MaHDX);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chitiethoadonxuat.MaSP);
            return View(chitiethoadonxuat);
        }

        // GET: /Admin/ChiTietHoaDonXuat/Delete/5
             [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = dao.XemChiTietHoaDonXuat(id);
            return View(result);
        }
        //[HttpPost, ActionName("Delete")]
        //public ActionResult DelSeteAction(int id)
        //{
        //    var result = dao.XoaChiTietHoaDonXuat(id);
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
