using System.Linq;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ProductImage
        public ActionResult Index(int productId)
        {
            var images = db.ProductImages.Where(x => x.ProductId == productId).ToList();
            ViewBag.productId = productId;
            return View(images);
        }

        [HttpPost]
        public ActionResult AddImage(int productId, string url)
        {
            db.ProductImages.Add(new ProductImage
            {
                ProductId = productId,
                Image = url,
                IsDefault = false
            });
            db.SaveChanges();
            return Json(new { Success = true });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.ProductImages.Find(id);
            if (item == null)
            {
                return Json(new { success = false, message = "Không tìm thấy ảnh" });
            }

            db.ProductImages.Remove(item);
            db.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult SetDefault(int id)
        {
            var item = db.ProductImages.Find(id);
            if (item == null)
            {
                return Json(new { success = false });
            }

            // Reset tất cả ảnh của sản phẩm
            var allImages = db.ProductImages.Where(x => x.ProductId == item.ProductId).ToList();
            foreach (var img in allImages)
            {
                img.IsDefault = false;
            }

            item.IsDefault = true;
            db.SaveChanges();

            return Json(new { success = true });
        }
    }
}
