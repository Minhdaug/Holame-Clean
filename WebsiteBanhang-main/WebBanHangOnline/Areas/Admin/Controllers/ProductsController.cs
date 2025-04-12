using PagedList;
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
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Products
        public ActionResult Index(string Searchtext, int? page)
        {
            IEnumerable<Product> items = db.Products.OrderByDescending(x => x.Id);
            var pageSize = 10;

            // Xử lý trang
            if (page == null)
            {
                page = 1;
            }

            // Kiểm tra và lọc theo từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(Searchtext))
            {
                var searchLower = Searchtext.ToLower();

                items = items.Where(x =>
                                    (!string.IsNullOrEmpty(x.Alias) && x.Alias.ToLower().Contains(searchLower)) ||
                                    (!string.IsNullOrEmpty(x.ProductCode) && x.ProductCode.ToLower().Contains(searchLower)) ||
                                    (!string.IsNullOrEmpty(x.Title) && x.Title.ToLower().Contains(searchLower))
                                    ).ToList();

            }

            // Phân trang
            var pageIndex = page.Value;
            items = items.ToPagedList(pageIndex, pageSize);

            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Add()
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Images, List<int> rDefault, List<string> Fragrances)
        {
            if (ModelState.IsValid)
            {
                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        if (i + 1 == rDefault[0])
                        {
                            model.Image = Images[i];
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = true
                            });
                        }
                        else
                        {
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false
                            });
                        }
                    }
                }
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                if (string.IsNullOrEmpty(model.SeoTitle))
                {
                    model.SeoTitle = model.Title;
                }
                if (string.IsNullOrEmpty(model.Alias))
                    model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                db.Products.Add(model);
                db.SaveChanges();

                // Lưu mùi hương mới nếu có
                if (Fragrances != null && Fragrances.Any())
                {
                    foreach (var fragrance in Fragrances)
                    {
                        if (!string.IsNullOrWhiteSpace(fragrance))
                        {
                            db.ProductFragrances.Add(new ProductFragrance
                            {
                                ProductId = model.Id,
                                Fragrance = fragrance.Trim()
                            });
                        }
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            var item = db.Products.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model, List<string> Images, List<int> rDefault, List<string> Fragrances)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                db.Products.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                // Xoá các mùi cũ trước
                var oldFragrances = db.ProductFragrances.Where(f => f.ProductId == model.Id).ToList();
                db.ProductFragrances.RemoveRange(oldFragrances);

                // Thêm mùi mới
                if (Fragrances != null && Fragrances.Any())
                {
                    foreach (var frag in Fragrances)
                    {
                        if (!string.IsNullOrWhiteSpace(frag))
                        {
                            db.ProductFragrances.Add(new ProductFragrance
                            {
                                ProductId = model.Id,
                                Fragrance = frag.Trim()
                            });
                        }
                    }
                }
                db.SaveChanges();

                // Xóa ảnh cũ
                var oldImages = db.ProductImages.Where(x => x.ProductId == model.Id).ToList();
                db.ProductImages.RemoveRange(oldImages);

                // Thêm lại ảnh
                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        db.ProductImages.Add(new ProductImage
                        {
                            ProductId = model.Id,
                            Image = Images[i],
                            IsDefault = (i + 1 == rDefault[0])
                        });
                    }
                }
                db.SaveChanges();


                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                //var checkImg = item.ProductImage.Where(x => x.ProductId == item.Id);
                //if (checkImg != null)
                //{
                //    foreach(var img in checkImg)
                //    {
                //        db.ProductImages.Remove(img);
                //        db.SaveChanges();
                //    }
                //}
                db.Products.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isAcive = item.IsActive });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsHome = item.IsHome });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsSale = item.IsSale });
            }

            return Json(new { success = false });
        }
    }
}