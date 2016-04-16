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
    public class NhanVienController : Controller
    {
        private readonly MayTinhDaiPhatEntities db = new MayTinhDaiPhatEntities();
        private readonly NhanVienDAO dao = new NhanVienDAO();

        // GET: /Admin/NhanVien/
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var list = dao.DanhSachNhanVien(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(list);
        }
        //public ActionResult Index()
        //{
        //    var resurt = dao.DanhSach();
        //    return View(resurt);
        //}

        // GET: /Admin/NhanVien/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanvien = dao.XemNhanVien(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(nhanvien);
        }

        // GET: /Admin/NhanVien/Create
        public ActionResult Create()
        {
            ViewBag.MaQuyen = new SelectList(db.Quyen, "MaQuyen", "TenQuyen");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NhanVien nhanvien)
        {
            if (ModelState.IsValid)
            {
                var result = dao.ThemNhanVien(nhanvien);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi! vui lòng kiểm tra lại");
                }
            }

            ViewBag.MaQuyen = new SelectList(db.Quyen, "MaQuyen", "TenQuyen", nhanvien.MaQuyen);
            return View(nhanvien);
        }

        // GET: /Admin/NhanVien/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanvien = dao.XemNhanVien(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaQuyen = new SelectList(db.Quyen, "MaQuyen", "TenQuyen", nhanvien.MaQuyen);
            return View(nhanvien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( NhanVien nhanvien)
        {
            if (ModelState.IsValid)
            {
                var result = dao.SuaNhanVien(nhanvien);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            ViewBag.MaQuyen = new SelectList(db.Quyen, "MaQuyen", "TenQuyen", nhanvien.MaQuyen);
            return View(nhanvien);
        }

             [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = dao.XemNhanVien(id);
            return View(result);
        }
        //[HttpPost, ActionName("Delete")]
        //public ActionResult DelSeteAction(int id)
        //{
        //    var result = dao.XoaNhanVien(id);
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
