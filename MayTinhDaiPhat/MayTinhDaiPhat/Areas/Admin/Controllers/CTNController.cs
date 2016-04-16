using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MayTinhDaiPhat.Model;

namespace MayTinhDaiPhat.Areas.Admin.Controllers
{
    public class CTNController : Controller
    {
        private MayTinhDaiPhatEntities db = new MayTinhDaiPhatEntities();

        // GET: /Admin/CTN/
        public ActionResult Index()
        {
            var chitiethoadonnhap = db.ChiTietHoaDonNhap.Include(c => c.HoaDonNhap).Include(c => c.SanPham);
            return View(chitiethoadonnhap.ToList());
        }

        // GET: /Admin/CTN/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDonNhap chitiethoadonnhap = db.ChiTietHoaDonNhap.Find(id);
            if (chitiethoadonnhap == null)
            {
                return HttpNotFound();
            }
            return View(chitiethoadonnhap);
        }

        // GET: /Admin/CTN/Create
        public ActionResult Create()
        {
            ViewBag.MaHDN = new SelectList(db.HoaDonNhap, "MaHDN", "MaHDN");
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP");
            return View();
        }

        // POST: /Admin/CTN/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MaCTHDN,MaHDN,MaSP,SoLuong,DonGia,ChietKhau")] ChiTietHoaDonNhap chitiethoadonnhap)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietHoaDonNhap.Add(chitiethoadonnhap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHDN = new SelectList(db.HoaDonNhap, "MaHDN", "MaHDN", chitiethoadonnhap.MaHDN);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chitiethoadonnhap.MaSP);
            return View(chitiethoadonnhap);
        }

        // GET: /Admin/CTN/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDonNhap chitiethoadonnhap = db.ChiTietHoaDonNhap.Find(id);
            if (chitiethoadonnhap == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHDN = new SelectList(db.HoaDonNhap, "MaHDN", "MaHDN", chitiethoadonnhap.MaHDN);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chitiethoadonnhap.MaSP);
            return View(chitiethoadonnhap);
        }

        // POST: /Admin/CTN/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MaCTHDN,MaHDN,MaSP,SoLuong,DonGia,ChietKhau")] ChiTietHoaDonNhap chitiethoadonnhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chitiethoadonnhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHDN = new SelectList(db.HoaDonNhap, "MaHDN", "MaHDN", chitiethoadonnhap.MaHDN);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chitiethoadonnhap.MaSP);
            return View(chitiethoadonnhap);
        }

        // GET: /Admin/CTN/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDonNhap chitiethoadonnhap = db.ChiTietHoaDonNhap.Find(id);
            if (chitiethoadonnhap == null)
            {
                return HttpNotFound();
            }
            return View(chitiethoadonnhap);
        }

        // POST: /Admin/CTN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietHoaDonNhap chitiethoadonnhap = db.ChiTietHoaDonNhap.Find(id);
            db.ChiTietHoaDonNhap.Remove(chitiethoadonnhap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
