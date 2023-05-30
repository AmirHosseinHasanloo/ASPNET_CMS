using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataLayer
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "کاربر گرامی {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "لطفا یک ایمیل معتبر وارد کنید")]
        public string Email { get; set; }
        [Display(Name = "رمز عبور فعلی")]
        [Required(ErrorMessage = "کاربر گرامی {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = " رمز عبور شما نمی تواند بیش از 150 کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "کاربر گرامی {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = " رمز عبور شما نمی تواند بیش از 150 کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار رمز عبور جدید")]
        [Required(ErrorMessage = "کاربر گرامی {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = " رمز عبور شما نمی تواند بیش از 150 کاراکتر باشد")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="رمز عبور جدید شما با تکرار شده ی آن مطابقت ندارد")]
        public string ReNewPassword { get; set; }
    }
}
