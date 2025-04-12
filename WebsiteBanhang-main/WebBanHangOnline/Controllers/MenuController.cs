using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuTop()
        {
            var items = db.Categories.OrderBy(x=>x.Position).ToList();
            return PartialView("_MenuTop", items);
        }

        public ActionResult MenuProductCategory()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", items);
        }
        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.CateId = id;
            }
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuLeft", items);
        }

        public ActionResult MenuArrivals()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuArrivals", items);
        }
        public ActionResult Menu4()
        {
            // return PartialView hoặc View phù hợp
            return PartialView("_Menu4");
        }
        public ActionResult MenuFeedback()
        {
            // return PartialView hoặc View phù hợp
            return PartialView("_MenuFeedback");
        }
        public ActionResult MenuHolame()
        {
            // return PartialView hoặc View phù hợp
            return PartialView("_MenuHolame");
        }
        public ActionResult MenuNhanxet()
        {
            // return PartialView hoặc View phù hợp
            return PartialView("_MenuNhanxet");
        }
        public ActionResult MenuSlide()
        {
            // return PartialView hoặc View phù hợp
            return PartialView("_MenuSlide");
        }
        public ActionResult MenuCategory()
        {
            var items = db.ProductCategories.Take(4).ToList();
            return PartialView("_MenuCategory", items);
        }
        public ActionResult MenuTop10()
        {
            return PartialView("_MenuTop10");
        }


    }
}