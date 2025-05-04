using K21CNT2_2110900086_DATN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace K21CNT2_2110900086_DATN.Controllers
{
    public class ProductController : Controller
    {
        private readonly K21cnt2NguyenVietTien2110900086DatnContext _context;

        public ProductController(K21cnt2NguyenVietTien2110900086DatnContext context)
        {
            _context = context;
        }
        [Route("shop.html", Name = "ShopProduct")]
        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 15;
                var IsProducts = _context.Products
                    .AsNoTracking()
                    .OrderBy(x => x.ProductId);
                PagedList<Product> models = new PagedList<Product>(IsProducts, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;

                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [Route("/{id}.html", Name = "ListProduct")]
        public IActionResult List(int id, int page = 1)
        {
            try
            {
                var pageSize = 15;
                var danhmuc = _context.Categories.Find(id);
                var IsProducts = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CategoryId == id)
                    .OrderBy(x => x.ProductId);
                PagedList<Product> models = new PagedList<Product>(IsProducts, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCate = danhmuc;
                return View(models);

            }
            catch
            {
                return RedirectToAction("Index", "Home");

            }

        }
        [Route("/Product/{id}.html", Name = "ProductDetails")]
        public IActionResult Details(int id)
        {
            try
            {
                var product = _context.Products.Include(x => x.Category).FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                var lsProduct = _context.Products.AsNoTracking()
                .Where(x => x.CategoryId == product.CategoryId && x.ProductId != id)
                .Take(4)
                .ToList();
                ViewBag.Sanpham = lsProduct;

                return View(product);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
