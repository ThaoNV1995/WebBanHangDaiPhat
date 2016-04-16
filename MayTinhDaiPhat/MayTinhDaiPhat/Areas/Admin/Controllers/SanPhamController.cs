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
    public class SanPhamController : Controller
    {
        private readonly MayTinhDaiPhatEntities db = new MayTinhDaiPhatEntities();
        private readonly SanPhamDAO dao = new SanPhamDAO();
        //GET: /Admin/SanPham/
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var list = dao.DanhSachSanPham(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(list);
        }
        //public ActionResult Index()
        //{
        //    var list = dao.DanhSach();
        //    return View(list);
        //}

        // GET: /Admin/SanPham/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanpham = dao.XemSanPham(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // GET: /Admin/SanPham/Create
        public ActionResult Create()
        {
            ViewBag.MaDM = new SelectList(db.DanhMuc, "MaDM", "TenDM");
            ViewBag.MaTH = new SelectList(db.ThuongHieu, "MaTH", "TenTH");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SanPham sanpham)
        {
            if (ModelState.IsValid)
            {
                var result = dao.ThemSanPham(sanpham);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi! vui lòng kiểm tra lại");
                }
            }

            ViewBag.MaDM = new SelectList(db.DanhMuc, "MaDM", "TenDM", sanpham.MaDM);
            ViewBag.MaTH = new SelectList(db.ThuongHieu, "MaTH", "TenTH", sanpham.MaTH);
            return View(sanpham);
        }

        // GET: /Admin/SanPham/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanpham = dao.XemSanPham(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDM = new SelectList(db.DanhMuc, "MaDM", "TenDM", sanpham.MaDM);
            ViewBag.MaTH = new SelectList(db.ThuongHieu, "MaTH", "TenTH", sanpham.MaTH);
            return View(sanpham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SanPham sanpham)
        {
            if (ModelState.IsValid)
            {
                var result = dao.SuaSanPham(sanpham);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            ViewBag.MaDM = new SelectList(db.DanhMuc, "MaDM", "TenDM", sanpham.MaDM);
            ViewBag.MaTH = new SelectList(db.ThuongHieu, "MaTH", "TenTH", sanpham.MaTH);
            return View(sanpham);
        }

             [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = dao.XoaSanPham(id);
            return View(result);
        }
        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteAction(int id)
        //{
        //    var result = dao.XoaSanPham(id);
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
