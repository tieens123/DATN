using System.ComponentModel.DataAnnotations;

namespace K21CNT2_2110900086_DATN.ViewModel
{
    public class AccountViewModel
    {
        public class RegisterViewModel
        {
            [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
            public string FullName { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập email")]
            [EmailAddress(ErrorMessage = "Email không hợp lệ")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
            [StringLength(11, ErrorMessage = "Số điện thoại không hợp lệ")]
            public string Phone { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
            [MinLength(6, ErrorMessage = "Mật khẩu phải ít nhất 6 ký tự")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
            [Compare("Password", ErrorMessage = "Mật khẩu không trùng khớp")]
            public string ConfirmPassword { get; set; }
        }

        public class LoginViewModel
        {
            [Required(ErrorMessage = "Vui lòng nhập email")]
            [EmailAddress(ErrorMessage = "Email không hợp lệ")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
            public string Password { get; set; }
        }
    }
}

