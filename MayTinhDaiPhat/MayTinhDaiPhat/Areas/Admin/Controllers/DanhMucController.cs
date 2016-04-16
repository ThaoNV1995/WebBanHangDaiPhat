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
    public class DanhMucController : Controller
    {
        private readonly MayTinhDaiPhatEntities db = new MayTinhDaiPhatEntities();
        private readonly DanhMucDAO dao = new DanhMucDAO();

        // GET: /Admin/DanhMuc/
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var list = dao.DanhSachDanhMuc(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(list);
        }
        //public ActionResult Index()
        //{
        //    var list = dao.DanhSach();
        //    return View(list);
        //}
        // GET: /Admin/DanhMuc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhmuc = db.DanhMuc.Find(id);
            if (danhmuc == null)
            {
                return HttpNotFound();
            }
            loadDrop();
            return View(danhmuc);
        }

        // GET: /Admin/DanhMuc/Create
        public ActionResult Create()
        {
            ViewBag.MaCha = new SelectList(db.DanhMuc, "MaDM", "TenDM");
            return View();
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DanhMuc danhmuc)
        {
            if (ModelState.IsValid)
            {
                var result = dao.ThemDanhMuc(danhmuc);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi! vui lòng kiểm tra lại");
                }
            }
            //else
            //{
            //    IEnumerable<SelectListItem> basetypes = db.DanhMuc.Select(
            //        b => new SelectListItem { Value = b.MaCha.ToString(), Text = b.TenDM });
            //    ViewData["MaCha"] = basetypes;
            //    return View(danhmuc);
            //}
            ViewBag.MaCha = new SelectList(db.DanhMuc, "MaDM", "TenDM", danhmuc.MaCha);
            return View(danhmuc);
        }

        // GET: /Admin/DanhMuc/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhmuc = dao.XemDanhMuc(id);
            if (danhmuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCha = new SelectList(db.DanhMuc, "MaDM", "TenDM", danhmuc.MaCha);
            return View(danhmuc);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DanhMuc danhmuc)
        {
            if (ModelState.IsValid)
            {
                var result = dao.SuaDanhMuc(danhmuc);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            //else
            //{
            //    IEnumerable<SelectListItem> basetypes = db.DanhMuc.Select(
            //        b => new SelectListItem { Value = b.MaCha.ToString(), Text = b.TenDM });
            //    ViewData["MaCha"] = basetypes;
            //    return View(danhmuc);
            //}
            ViewBag.MaCha = new SelectList(db.DanhMuc, "MaDM", "TenDM", danhmuc.MaCha);
            return View(danhmuc);
        }

             [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = dao.XemDanhMuc(id);
            return View(result);
        }
        //[HttpPost, ActionName("Delete")]
        //public ActionResult DelSeteAction(int id)
        //{
        //    var result = dao.XoaDanhMuc(id);
        //    return RedirectToAction("Index");
        //}


             public void loadDrop()
             {
                 var dr = new List<SelectListItem>();
                 dr.Add(new SelectListItem { Value = null, Text = " " });

                 foreach (var pro in db.DanhMuc)
                 {
                     dr.Add(new SelectListItem { Value = pro.MaDM.ToString(), Text = pro.TenDM });
                 }
                 ViewData["MaCha"] = dr;
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
