using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using MayTinhDaiPhat.Models;
using NhapXuat.DAO;
using MayTinhDaiPhat.Model;

namespace MayTinhDaiPhat.Areas.Admin.Controllers
{
    public class SlideController : Controller
    {
        //private DataContext db = new DataContext();
        private readonly MayTinhDaiPhatEntities db = new MayTinhDaiPhatEntities();
        private readonly SlideDAO dao = new SlideDAO();

        // GET: /Admin/Slide/
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var list = dao.DanhSachSlide(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(list);
        }
        //public ActionResult Index()
        //{
        //    var list = dao.DanhSach();
        //    return View(list);
        //}
        // GET: /Admin/Slide/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = dao.XemSlide(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // GET: /Admin/Slide/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var result = dao.ThemSlide(slide);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi! vui lòng kiểm tra lại");
                }
            }

            return View(slide);
        }

        // GET: /Admin/Slide/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = dao.XemSlide(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var result = dao.SuaSlide(slide);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View(slide);
        }

             [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = dao.XemSlide(id);
            return View(result);
        }
        //[HttpPost, ActionName("Delete")]
        //public ActionResult DelSeteAction(int id)
        //{
        //    var result = dao.XoaSlide(id);
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
