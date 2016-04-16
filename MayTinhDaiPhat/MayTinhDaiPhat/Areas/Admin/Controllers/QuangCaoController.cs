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
    public class QuangCaoController : Controller
    {
        private readonly MayTinhDaiPhatEntities db = new MayTinhDaiPhatEntities();
        private readonly QuangCaoDAO dao = new QuangCaoDAO();

        // GET: /Admin/QuangCao/
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var list = dao.DanhSachQuangCao(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(list);
        }
        //public ActionResult Index()
        //{
        //    var list = dao.DanhSach();
        //    return View(list);
        //}
        // GET: /Admin/QuangCao/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuangCao quangcao = dao.XemQuangCao(id);
            if (quangcao == null)
            {
                return HttpNotFound();
            }
            return View(quangcao);
        }

        // GET: /Admin/QuangCao/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuangCao quangcao)
        {
            if (ModelState.IsValid)
            {
                var result = dao.ThemQuangCao(quangcao);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi! vui lòng kiểm tra lại");
                }
            }

            return View(quangcao);
        }

        // GET: /Admin/QuangCao/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuangCao quangcao = dao.XemQuangCao(id);
            if (quangcao == null)
            {
                return HttpNotFound();
            }
            return View(quangcao);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( QuangCao quangcao)
        {
            if (ModelState.IsValid)
            {
                var result = dao.SuaQuangCao(quangcao);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View(quangcao);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = dao.XoaQuangCao(id);
            return View(result);
        }

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DelSeteAction(int id)
        //{
        //    var result = dao.XoaQuangCao(id);
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
