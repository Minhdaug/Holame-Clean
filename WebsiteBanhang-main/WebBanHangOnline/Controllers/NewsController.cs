using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: News
        public ActionResult Index(string Searchtext, int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }

            // Lọc tin tức được hiển thị (IsActive == 1)
            IEnumerable<News> items = db.News
                .Where(x => x.IsActive == true)
                .OrderByDescending(x => x.CreatedDate);

            // Tìm kiếm nếu có từ khoá
            if (!string.IsNullOrEmpty(Searchtext))
            {
                var searchLower = Searchtext.ToLower();
                items = items.Where(x => x.Title.ToLower().Contains(searchLower));
            }

            var pageIndex = page ?? 1;
            var pagedItems = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            return View(pagedItems);
        }
        public ActionResult Category(int id, int? page)
        {
            var pageSize = 10;
            var pageNumber = page ?? 1;

            var category = db.NewsCategories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            var items = db.News
                          .Where(x => x.NewsCategoryId == id && x.IsActive)
                          .OrderByDescending(x => x.Id)
                          .ToPagedList(pageNumber, pageSize);

            ViewBag.CategoryName = category.Title;
            ViewBag.CategoryId = id;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;

            return View("Index", items);
        }


        public ActionResult Detail(int id)
        {
            var item = db.News.FirstOrDefault(x => x.Id == id && x.IsActive == true);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public ActionResult Partial_News_Home()
        {
            var items = db.News
                .Where(x => x.IsActive == true)
                .OrderByDescending(x => x.CreatedDate)
                .Take(3)
                .ToList();

            return PartialView(items);
        }

        public ActionResult PartialRecent()
        {
            var items = db.News
                .Where(x => x.IsActive == true)
                .OrderByDescending(x => x.CreatedDate)
                .Take(3)
                .ToList();

            return PartialView(items);
        }
        public ActionResult PartialRecentHome()
        {
            var items = db.News
                .Where(x => x.IsActive == true)
                .OrderByDescending(x => x.CreatedDate)
                .Take(3)
                .ToList();

            return PartialView(items);
        }
        public ActionResult PartialNewsCategory()
        {
            var categories = db.NewsCategories
                .Select(c => new WebBanHangOnline.Models.ViewModels.NewsCategoryViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    NewsCount = c.News.Count()
                }).ToList();

            return PartialView(categories);
        }
    }
}
