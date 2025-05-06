using AspNetCoreHero.ToastNotification.Abstractions;
using K21CNT2_2110900086_DATN.Extension;
using K21CNT2_2110900086_DATN.Models;
using K21CNT2_2110900086_DATN.ViewModel;
using K21CNT2_2110900086_DATN.Extension;
using K21CNT2_2110900086_DATN.Models;
using K21CNT2_2110900086_DATN.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace K21CNT2_2110900055_DATN.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly K21cnt2NguyenVietTien2110900086DatnContext _context;
        public INotyfService _notifyService { get; }
        public ShoppingCartController(K21cnt2NguyenVietTien2110900086DatnContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }
        public List<CartItem> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (gh == default(List<CartItem>))
                {
                    gh = new List<CartItem>();
                }
                return gh;
            }
        }
        [HttpPost]
        [Route("api/cart/add")]
        public IActionResult AddToCart(int productID, int? amount)
        {
            try
            {
                List<CartItem> gioHang = GioHang;

                // Tìm sản phẩm trong giỏ hàng
                CartItem item = gioHang.SingleOrDefault(p => p.product.ProductId == productID);

                if (item != null) // Nếu sản phẩm đã có trong giỏ hàng
                {
                    if (amount.HasValue)
                    {
                        item.amount += amount.Value; // CỘNG THÊM SỐ LƯỢNG
                    }
                    else
                    {
                        item.amount++; //  Mặc định tăng thêm 1 nếu không có amount
                    }
                }
                else // Nếu sản phẩm chưa có, thêm mới vào giỏ hàng
                {
                    Product hh = _context.Products.SingleOrDefault(p => p.ProductId == productID);
                    item = new CartItem
                    {
                        amount = amount ?? 1, // Nếu amount có giá trị thì dùng, nếu không thì mặc định là 1
                        product = hh
                    };
                    gioHang.Add(item);
                }

                // Lưu lại session
                HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
                _notifyService.Success("Thêm sản phẩm thành công");
                // Trả về số lượng mới trong giỏ hàng
                int totalItems = gioHang.Sum(i => i.amount);

                return Json(new { success = true, cartItemCount = totalItems });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [Route("api/cart/update")]
        public IActionResult UpdateCart(int productID, int? amount)
        {
            // lấy giỏ hàng ra để xử lý
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");

            try
            {
                if (cart != null)
                {
                    CartItem item = cart.SingleOrDefault(p => p.product.ProductId == productID);
                    if (item != null && amount.HasValue) //đã có -- > cập nhật số lượng
                    {
                        item.amount = amount.Value;
                    }
                    //lưu lại session
                    HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
                }
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [Route("api/cart/remove")]
        public ActionResult Remove(int productID)
        {
            try
            {
                List<CartItem> gioHang = GioHang;
                CartItem item = gioHang.SingleOrDefault(p => p.product.ProductId == productID);
                if (item != null)
                {
                    gioHang.Remove(item);
                }

                // Lưu lại session
                HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        [Route("cart.html", Name = "Cart")]
        public IActionResult Index()
        {
            return View(GioHang);
        }

    }
}
