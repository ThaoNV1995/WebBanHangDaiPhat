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
    public class NhaPhanPhoiController : Controller
    {
        private readonly MayTinhDaiPhatEntities db = new MayTinhDaiPhatEntities();
        private readonly NhaPhanPhoiDAO dao = new NhaPhanPhoiDAO();

        // GET: /Admin/NhaPhanPhoi/
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var list = dao.DanhSachNhaPhanPhoi(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(list);
        }
        //public ActionResult Index()
        //{
        //    var list = dao.DanhSach();
        //    return View(list);
        //}
        // GET: /Admin/NhaPhanPhoi/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaPhanPhoi nhaphanphoi = dao.XemNhaPhanPhoi(id);
            if (nhaphanphoi == null)
            {
                return HttpNotFound();
            }
            return View(nhaphanphoi);
        }

        // GET: /Admin/NhaPhanPhoi/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NhaPhanPhoi nhaphanphoi)
        {
            if (ModelState.IsValid)
            {
                var result = dao.ThemNhaPhanPhoi(nhaphanphoi);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi! vui lòng kiểm tra lại");
                }
            }

            return View(nhaphanphoi);
        }

        // GET: /Admin/NhaPhanPhoi/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaPhanPhoi nhaphanphoi = dao.XemNhaPhanPhoi(id);
            if (nhaphanphoi == null)
            {
                return HttpNotFound();
            }
            return View(nhaphanphoi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NhaPhanPhoi nhaphanphoi)
        {
            if (ModelState.IsValid)
            {
                var result = dao.SuaNhaPhanPhoi(nhaphanphoi);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View(nhaphanphoi);
        }

             [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = dao.XemNhaPhanPhoi(id);
            return View(result);
        }
        //[HttpPost, ActionName("Delete")]
        //public ActionResult DelSeteAction(int id)
        //{
        //    var result = dao.XoaNhaPhanPhoi(id);
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
