using K21CNT2_2110900086_DATN.Models;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Linq;
using static K21CNT2_2110900086_DATN.ViewModel.AccountViewModel;

namespace K21CNT2_2110900055_DATN.Controllers
{
    public class AccountController : Controller
    {
        private readonly K21cnt2NguyenVietTien2110900086DatnContext _context;
        public INotyfService _notifyService { get; }

        public AccountController(K21cnt2NguyenVietTien2110900086DatnContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        public IActionResult Register() => View();
        public IActionResult MyAccount() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var existingUser = _context.Accounts.FirstOrDefault(a => a.Email == model.Email);
            if (existingUser != null)
            {
                ViewBag.Error = "Email đã được sử dụng!";
                return View(model);
            }

            var newUser = new Account
            {
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone,
                Password = model.Password, // Lưu mật khẩu dưới dạng plaintext
                RoleId = 2, // Mặc định là User
                CreateDate = DateTime.Now
            };

            _context.Accounts.Add(newUser);
            _context.SaveChanges();
            _notifyService.Success("Đăng kí tài khoản thành công");
            return RedirectToAction("Login");
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var account = _context.Accounts.FirstOrDefault(a => a.Email == model.Email);
            if (account == null || account.Password != model.Password)
            {
                ViewBag.Error = "Email hoặc mật khẩu không đúng!";
                return View(model);
            }

            // Lưu thông tin vào Session
            HttpContext.Session.SetInt32("UserId", account.AccountId);
            HttpContext.Session.SetInt32("RoleId", (int)account.RoleId);
            account.LastLogin = DateTime.Now;
            _context.SaveChanges();


            // Điều hướng Admin về trang Admin Index
            if (account.RoleId == 1)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            }

            // Điều hướng User về trang chính
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            _notifyService.Information("Bạn đã đăng xuất.");
            return RedirectToAction("Login");
        }
    }
}
