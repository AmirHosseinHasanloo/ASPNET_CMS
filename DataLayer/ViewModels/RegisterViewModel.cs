using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataLayer
{
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "کاربر گرامی {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "نام کاربری شما نمی تواند بیش از 250 کاراکتر باشد")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "کاربر گرامی {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "لطفا یک ایمیل معتبر وارد کنید")]
        public string Email { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "کاربر گرامی {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = " رمز عبور شما نمی تواند بیش از 150 کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "کاربر گرامی {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = " تکرار رمز عبور شما نمی تواند بیش از 150 کاراکتر باشد")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="کاربر گرامی تکرار رمز عبور شما با رمز عبور شما مطابقت ندارد")]
        public string RePassword { get; set; }
    }
}
