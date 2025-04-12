using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ProductFragranceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Create(int productId)
        {
            ViewBag.ProductId = productId;
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductFragrance model)
        {
            if (ModelState.IsValid)
            {
                db.ProductFragrances.Add(model);
                db.SaveChanges();
                return RedirectToAction("Edit", "Products", new { id = model.ProductId });
            }
            ViewBag.ProductId = model.ProductId;
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var item = db.ProductFragrances.Find(id);
            if (item != null)
            {
                db.ProductFragrances.Remove(item);
                db.SaveChanges();
                return RedirectToAction("Edit", "Products", new { id = item.ProductId });
            }
            return HttpNotFound();
        }
    }
}