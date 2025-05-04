using AspNetCoreHero.ToastNotification.Abstractions;
using K21CNT2_2110900086_DATN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace K21CNT2_2110900086_DATN.Controllers
{
    public class BlogController : Controller
    {
        private readonly K21cnt2NguyenVietTien2110900086DatnContext _context;
        public BlogController(K21cnt2NguyenVietTien2110900086DatnContext context)
        {
            _context = context;
        }
        // GET: Blogs/Index
        [Route("blogs.html", Name = "Blog")]
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var IsBlogs = _context.Blogs.AsNoTracking().OrderBy(x => x.BlogId);
            PagedList<Blog> models = new PagedList<Blog>(IsBlogs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }
        // GET: Blogs/Details/5
        [Route("/Blog/{id}.html", Name = "BlogDetails")]
        public IActionResult Details(int id)
        {
            var blog = _context.Blogs.AsNoTracking().SingleOrDefault(x => x.BlogId == id);
            if (blog == null)
            {
                return RedirectToAction("Index");
            }
            var lsBaivietlienquan = _context.Blogs
            .AsNoTracking()
            .Where(x => x.BlogId != id) // Lọc bài khác với bài hiện tại
            .Take(3) // Lấy 3 bài
            .ToList();
            ViewBag.Baivietlienquan = lsBaivietlienquan;
            return View(blog);
        }

    }
}
