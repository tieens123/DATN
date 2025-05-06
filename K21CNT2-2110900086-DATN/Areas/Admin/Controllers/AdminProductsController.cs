using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using K21CNT2_2110900086_DATN.Models;
using PagedList.Core;
using K21CNT2_2110900086_DATN.Helper;
using static System.Net.Mime.MediaTypeNames;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace K21CNT2_2110900086_DATN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly K21cnt2NguyenVietTien2110900086DatnContext _context;
        public INotyfService _notifyService { get; }

        public AdminProductsController(K21cnt2NguyenVietTien2110900086DatnContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService; 
        }

        // GET: Admin/AdminProducts
        public IActionResult Index(int page = 1, int CatID = 0)
        {
            var pageNumber = page;
            var pageSize = 20;

            IQueryable<Product> query = _context.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .OrderBy(x => x.ProductId);

            // Lọc theo danh mục nếu CatID > 0
            if (CatID > 0)
            {
                query = query.Where(x => x.CategoryId == CatID);
            }

            PagedList<Product> models = new PagedList<Product>(query, pageNumber, pageSize);

            ViewBag.CurrentCatID = CatID;
            ViewBag.CurrentPage = pageNumber;
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", CatID);

            return View(models);
        }

        public IActionResult Filtter(int CatID = 0)
        {
            string url = "/Admin/AdminProducts";

            if (CatID > 0)
            {
                url = $"/Admin/AdminProducts?CatID={CatID}";
            }

            return Json(new { status = "success", redirectUrl = url });
        }

        // GET: Admin/AdminProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/AdminProducts/Create
        public IActionResult Create()
        {
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }


        // POST: Admin/AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,CategoryId,Price,UnitslnStock,Image")] Product product, Microsoft.AspNetCore.Http.IFormFile fImage)
        {
            if (ModelState.IsValid)
            {
                if (fImage != null && fImage.Length > 0)
                {
                    string extension = Path.GetExtension(fImage.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(fImage.FileName)
                                     .Replace(" ", "-") + "-" + Guid.NewGuid().ToString().Substring(0, 8) + extension;

                    product.Image = await Utilities.UploadFile(fImage, "products", fileName.ToLower());
                }
                else
                {
                    product.Image = "default.jpg"; // Nếu không có ảnh, đặt ảnh mặc định
                }

                _context.Add(product);
                _notifyService.Success("Tạo mới thành công");
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }



        // GET: Admin/AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Admin/AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Description,CategoryId,Price,Image,UnitslnStock")] Product product, Microsoft.AspNetCore.Http.IFormFile fImage)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingProduct = await _context.Products.FindAsync(id);
                    if (existingProduct == null)
                    {
                        return NotFound();
                    }

                    // Nếu có file ảnh mới thì cập nhật, nếu không giữ nguyên ảnh cũ
                    if (fImage != null && fImage.Length > 0)
                    {
                        string extension = Path.GetExtension(fImage.FileName);
                        string fileName = product.ProductName.Replace(" ", "-") + "-" + Guid.NewGuid().ToString().Substring(0, 8) + extension;
                        existingProduct.Image = await Utilities.UploadFile(fImage, "products", fileName.ToLower());
                    }

                    // Cập nhật thông tin khác của sản phẩm
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.Description = product.Description;
                    existingProduct.CategoryId = product.CategoryId;
                    existingProduct.Price = product.Price;
                    existingProduct.UnitsInStock = product.UnitsInStock;

                    _context.Update(existingProduct);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Cập nhật thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        _notifyService.Error("Có lỗi xảy ra");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }


        // GET: Admin/AdminProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            _notifyService.Success("Xóa sản phẩm thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
