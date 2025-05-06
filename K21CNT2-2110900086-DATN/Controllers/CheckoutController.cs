using AspNetCoreHero.ToastNotification.Abstractions;
using K21CNT2_2110900086_DATN.Extension;
using K21CNT2_2110900086_DATN.Models;
using K21CNT2_2110900086_DATN.ViewModel;
using K21CNT2_2110900086_DATN.Models;
using K21CNT2_2110900086_DATN.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K21CNT2_2110900055_DATN.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly K21cnt2NguyenVietTien2110900086DatnContext _context;
        private readonly INotyfService _notifyService;

        public CheckoutController(K21cnt2NguyenVietTien2110900086DatnContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        [Route("checkout.html", Name = "Checkout")]
        [HttpGet]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            if (cart == null || !cart.Any())
            {
                _notifyService.Warning("Giỏ hàng trống.");
                return RedirectToAction("Index", "Cart");
            }
            ViewBag.GioHang = cart;
            return View(new CheckoutViewModel());
        }

        [Route("checkout.html", Name = "Checkout")]
        [HttpPost]
        public IActionResult Index(CheckoutViewModel muahang)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            if (cart == null || !cart.Any() || !ModelState.IsValid)
            {
                _notifyService.Warning("Giỏ hàng trống hoặc thông tin chưa đầy đủ.");
                ViewBag.GioHang = cart;
                return View(muahang);
            }

            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var customer = _context.Customers.FirstOrDefault(c => c.Email == muahang.Email) ?? new Customer
                    {
                        FullName = muahang.FullName,
                        Phone = muahang.Phone,
                        Email = muahang.Email,
                        Address = muahang.Address
                    };

                    if (customer.CustomerId == 0) _context.Customers.Add(customer);
                    _context.SaveChanges();

                    var order = new Order
                    {
                        CustomerId = customer.CustomerId,
                        OrderDate = DateTime.Now,
                        StatusId = 1,
                        Deleted = false,
                        Paid = false,
                        Note = muahang.Note
                    };
                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    _context.OrderDetails.AddRange(cart.Select(item => new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = item.product.ProductId,
                        Quantity = item.amount,
                        Total = (int)item.TotalMoney
                    }));
                    _context.SaveChanges();
                    transaction.Commit();

                    TempData["OrderId"] = order.OrderId;
                    TempData["CustomerName"] = customer.FullName;
                    TempData["TotalAmount"] = cart.Sum(c => c.TotalMoney).ToString("#,##0") + " VNĐ";
                    HttpContext.Session.Remove("GioHang");

                    _notifyService.Success("Đặt hàng thành công!");
                    return RedirectToAction("Success");
                }
            }
            catch
            {
                _notifyService.Error("Có lỗi xảy ra khi đặt hàng.");
                ViewBag.GioHang = cart;
                return View(muahang);
            }
        }

        [Route("checkout-success.html", Name = "CheckoutSuccess")]
        public IActionResult Success()
        {

            ViewBag.OrderId = TempData["OrderId"];
            ViewBag.CustomerName = TempData["CustomerName"];
            ViewBag.TotalAmount = TempData["TotalAmount"];

            return View();
        }
    }
}
