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
    public class ThuongHieuController : Controller
    {
        private readonly MayTinhDaiPhatEntities db = new MayTinhDaiPhatEntities();
        private readonly ThuongHieuDAO dao = new ThuongHieuDAO();

        // GET: /Admin/ThuongHieu/
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var list = dao.DanhSachThuongHieu(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(list);
        }
        //public ActionResult Index()
        //{
        //    var list = dao.DanhSach();
        //    return View(list);
        //}

        // GET: /Admin/ThuongHieu/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuongHieu thuonghieu = dao.XemThuongHieu(id);
            if (thuonghieu == null)
            {
                return HttpNotFound();
            }
            return View(thuonghieu);
        }

        // GET: /Admin/ThuongHieu/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThuongHieu thuonghieu)
        {
            if (ModelState.IsValid)
            {
                var result = dao.ThemThuongHieu(thuonghieu);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi! vui lòng kiểm tra lại");
                }
            }

            return View(thuonghieu);
        }

        // GET: /Admin/ThuongHieu/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuongHieu thuonghieu = dao.XemThuongHieu(id);
            if (thuonghieu == null)
            {
                return HttpNotFound();
            }
            return View(thuonghieu);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ThuongHieu thuonghieu)
        {
            if (ModelState.IsValid)
            {
                var result = dao.SuaThuongHieu(thuonghieu);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View(thuonghieu);
        }

             [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = dao.XemThuongHieu(id);
            return View(result);
        }
        //[HttpPost, ActionName("Delete")]
        //public ActionResult DelSeteAction(int id)
        //{
        //    var result = dao.XoaThuongHieu(id);
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
