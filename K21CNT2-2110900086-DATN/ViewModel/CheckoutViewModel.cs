using System.ComponentModel.DataAnnotations;

namespace K21CNT2_2110900086_DATN.ViewModel
{
    public class CheckoutViewModel
    {
        //public int CustomerId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress]
        public string Email { get; set; }

        public string? Note { get; set; }
    }
}
