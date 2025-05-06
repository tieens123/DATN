using K21CNT2_2110900086_DATN.Extension;
using K21CNT2_2110900086_DATN.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace K21CNT2_2110900055_DATN.Controllers.Components
{
    public class HeaderCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            return View(cart);
        }
    }
}
