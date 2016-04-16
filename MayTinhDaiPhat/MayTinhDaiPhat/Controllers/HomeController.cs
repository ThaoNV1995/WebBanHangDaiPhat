using NhapXuat.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MayTinhDaiPhat.Controllers
{
    public class HomeController : Controller
    {
        private readonly SanPhamDAO dao = new SanPhamDAO();
        public ActionResult Index()
        {
            var list = dao.DanhSach();
            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}