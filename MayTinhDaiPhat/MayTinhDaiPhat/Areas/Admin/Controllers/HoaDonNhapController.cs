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
    public class HoaDonNhapController : Controller
    {
        private MayTinhDaiPhatEntities db = new MayTinhDaiPhatEntities();

        // GET: /Admin/HoaDonNhap/
        public ActionResult Index()
        {
            var hoadonnhap = db.HoaDonNhap.Include(h => h.NhaPhanPhoi).Include(h => h.NhanVien);
            return View(hoadonnhap.ToList());
        }

        // GET: /Admin/HoaDonNhap/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonNhap hoadonnhap = db.HoaDonNhap.Find(id);
            if (hoadonnhap == null)
            {
                return HttpNotFound();
            }
            return View(hoadonnhap);
        }

        // GET: /Admin/HoaDonNhap/Create
        public ActionResult Create()
        {
            ViewBag.MaNPP = new SelectList(db.NhaPhanPhoi, "MaNPP", "TenNPP");
            ViewBag.MaNV = new SelectList(db.NhanVien, "MaNV", "TenNV");
            return View();
        }

        // POST: /Admin/HoaDonNhap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MaHDN,MaNPP,MaNV,NgayNhap")] HoaDonNhap hoadonnhap)
        {
            if (ModelState.IsValid)
            {
                db.HoaDonNhap.Add(hoadonnhap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNPP = new SelectList(db.NhaPhanPhoi, "MaNPP", "TenNPP", hoadonnhap.MaNPP);
            ViewBag.MaNV = new SelectList(db.NhanVien, "MaNV", "TenNV", hoadonnhap.MaNV);
            return View(hoadonnhap);
        }

        // GET: /Admin/HoaDonNhap/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonNhap hoadonnhap = db.HoaDonNhap.Find(id);
            if (hoadonnhap == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNPP = new SelectList(db.NhaPhanPhoi, "MaNPP", "TenNPP", hoadonnhap.MaNPP);
            ViewBag.MaNV = new SelectList(db.NhanVien, "MaNV", "TenNV", hoadonnhap.MaNV);
            return View(hoadonnhap);
        }

        // POST: /Admin/HoaDonNhap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MaHDN,MaNPP,MaNV,NgayNhap")] HoaDonNhap hoadonnhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoadonnhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNPP = new SelectList(db.NhaPhanPhoi, "MaNPP", "TenNPP", hoadonnhap.MaNPP);
            ViewBag.MaNV = new SelectList(db.NhanVien, "MaNV", "TenNV", hoadonnhap.MaNV);
            return View(hoadonnhap);
        }

        // GET: /Admin/HoaDonNhap/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonNhap hoadonnhap = db.HoaDonNhap.Find(id);
            if (hoadonnhap == null)
            {
                return HttpNotFound();
            }
            return View(hoadonnhap);
        }

        // POST: /Admin/HoaDonNhap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoaDonNhap hoadonnhap = db.HoaDonNhap.Find(id);
            db.HoaDonNhap.Remove(hoadonnhap);
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
