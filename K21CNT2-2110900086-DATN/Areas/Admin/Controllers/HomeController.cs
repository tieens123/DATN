using Microsoft.AspNetCore.Mvc;

namespace K21CNT2_2110900086_DATN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
