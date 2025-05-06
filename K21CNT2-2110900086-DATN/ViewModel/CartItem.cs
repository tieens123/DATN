using K21CNT2_2110900086_DATN.Models;

namespace K21CNT2_2110900086_DATN.ViewModel
{
    public class CartItem
    {
        public Product product { get; set; }
        public int amount { get; set; }
        public double TotalMoney => amount * product.Price;
    }
}
