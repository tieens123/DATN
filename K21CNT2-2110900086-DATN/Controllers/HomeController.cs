using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using K21CNT2_2110900086_DATN.Models;
using Microsoft.EntityFrameworkCore;

namespace K21CNT2_2110900086_DATN.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly K21cnt2NguyenVietTien2110900086DatnContext _context;

    public HomeController(ILogger<HomeController> logger, K21cnt2NguyenVietTien2110900086DatnContext context)
    {
        _logger = logger;
        _context = context;
    }


    public IActionResult Index()
    {
        var products = _context.Products
            .AsNoTracking()
            .Take(8) // L?y 8 s?n ph?m trong danh sách
            .ToList();

        // L?y 3 bài blog m?i nh?t
        var blogs = _context.Blogs
            .AsNoTracking()
            .OrderByDescending(x => x.BlogId)
            .Take(2)
            .ToList();

        // G?i danh sách s?n ph?m ra View
        ViewBag.Products = products;
        // ??y d? li?u blog ra View
        ViewBag.Blogs = blogs;
        return View();
    }


    public IActionResult About()
    {
        return View();
    }
    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
