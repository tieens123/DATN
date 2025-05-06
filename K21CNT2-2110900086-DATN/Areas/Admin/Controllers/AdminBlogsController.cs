using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using K21CNT2_2110900086_DATN.Models;
using PagedList.Core;
using AspNetCoreHero.ToastNotification.Abstractions;
using K21CNT2_2110900086_DATN.Helper;
using static System.Net.Mime.MediaTypeNames;

namespace K21CNT2_2110900086_DATN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminBlogsController : Controller
    {
        private readonly K21cnt2NguyenVietTien2110900086DatnContext _context;

        public INotyfService _notifyService { get; }
        public AdminBlogsController(K21cnt2NguyenVietTien2110900086DatnContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/AdminBlogs
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var IsBlogs = _context.Blogs.AsNoTracking().OrderBy(x => x.BlogId);
            PagedList<Blog> models = new PagedList<Blog>(IsBlogs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }
        // GET: Admin/AdminBlogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Admin/AdminBlogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminBlogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,BlogName,Title,Description,Image,Author")] Blog blog, Microsoft.AspNetCore.Http.IFormFile fImage)
        {
            if (ModelState.IsValid)
            {
                if (fImage != null && fImage.Length > 0)
                {
                    string extension = Path.GetExtension(fImage.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(fImage.FileName)
                                     .Replace(" ", "-") + "-" + Guid.NewGuid().ToString().Substring(0, 8) + extension;

                    blog.Image = await Utilities.UploadFile(fImage, "blogs", fileName.ToLower());
                }
                else
                {
                    blog.Image = "default.jpg"; // Nếu không có ảnh, đặt ảnh mặc định
                }
                _context.Add(blog);
                _notifyService.Success("Tạo mới thành công");
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Admin/AdminBlogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Admin/AdminBlogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,BlogName,Title,Description,Image,Author")] Blog blog, Microsoft.AspNetCore.Http.IFormFile fImage)
        {
            if (id != blog.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingBlog = await _context.Blogs.FindAsync(id);
                    if (existingBlog == null)
                    {
                        return NotFound();
                    }

                    // Nếu có file ảnh mới thì cập nhật, nếu không giữ nguyên ảnh cũ
                    if (fImage != null && fImage.Length > 0)
                    {
                        string extension = Path.GetExtension(fImage.FileName);
                        string fileName = blog.BlogName.Replace(" ", "-") + "-" + Guid.NewGuid().ToString().Substring(0, 8) + extension;
                        existingBlog.Image = await Utilities.UploadFile(fImage, "blogs", fileName.ToLower());
                    }

                    // Cập nhật thông tin khác của sản phẩm
                    existingBlog.BlogName = blog.BlogName;
                    existingBlog.Title = blog.Title;
                    existingBlog.Description = blog.Description;
                    existingBlog.Author = blog.Author;


                    _context.Update(existingBlog);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Cập nhật thành công");
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.BlogId))
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
            return View(blog);
        }

        // GET: Admin/AdminBlogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Admin/AdminBlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
            }

            await _context.SaveChangesAsync();
            _notifyService.Success("Xóa sản phẩm thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
        }
    }
}
