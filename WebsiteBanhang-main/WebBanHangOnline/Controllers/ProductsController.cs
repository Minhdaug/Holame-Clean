using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index(decimal? minPrice, decimal? maxPrice, string sortOrder, string searchString)
        {
            var items = db.Products.Where(x => x.IsActive);
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                items = items.Where(x =>
                    x.Title.ToLower().Contains(searchString) ||
                    x.ProductCode.ToLower().Contains(searchString) ||
                    x.Alias.ToLower().Contains(searchString));
            }

            var productList = items.ToList();
            // Lọc theo giá
            if (minPrice.HasValue)
            {
                items = items.Where(x => x.PriceSale >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                items = items.Where(x => x.PriceSale <= maxPrice.Value);
            }

            // Sắp xếp theo giá
            switch (sortOrder)
            {
                case "price_asc":
                    items = items.OrderBy(x => x.PriceSale);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(x => x.PriceSale);
                    break;
                default:
                    items = items.OrderByDescending(x => x.Id); // Mặc định theo mới nhất
                    break;
            }

            return View(items.ToList());
        }


        public ActionResult Detail(string alias,int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                db.Entry(item).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }
            var countReview = db.Reviews.Where(x => x.ProductId == id).Count();
            ViewBag.CountReview = countReview;
            return View(item);
        }
        public ActionResult ProductCategory(string alias, int id, decimal? minPrice, decimal? maxPrice, string sortOrder, string searchString)
        {
            var items = db.Products.Where(x => x.ProductCategoryId == id && x.IsActive);
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                items = items.Where(x =>
                    x.Title.ToLower().Contains(searchString) ||
                    x.ProductCode.ToLower().Contains(searchString) ||
                    x.Alias.ToLower().Contains(searchString));
            }

            var productList = items.ToList();
            if (minPrice.HasValue)
            {
                items = items.Where(x => x.PriceSale >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                items = items.Where(x => x.PriceSale <= maxPrice.Value);
            }

            switch (sortOrder)
            {
                case "price_asc":
                    items = items.OrderBy(x => x.PriceSale);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(x => x.PriceSale);
                    break;
                default:
                    items = items.OrderByDescending(x => x.Id);
                    break;
            }

            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
                ViewBag.SeoKeywords = cate.SeoKeywords;
                ViewBag.SeoDescription = cate.SeoDescription;
            }

            ViewBag.CateId = id;
            return View(items.ToList());
        }


        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.Products.Where(x => x.IsHome && x.IsActive).Take(8).ToList();
            return PartialView(items);
        }

        public ActionResult Partial_ProductSales()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).ToList();
            return PartialView(items);
        }
        public ActionResult ProductCategoryHome()
        {
            // tự lấy danh sách category hoặc mặc định
            var categories = db.ProductCategories.ToList();
            return PartialView(categories);
        }
        public ActionResult Partial_ItemsHome()
        {
            var items = db.Products.Where(x => x.IsHome && x.IsActive).Take(4).ToList();
            return PartialView(items);
        }
    }
}