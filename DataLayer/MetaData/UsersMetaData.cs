using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UsersMetaData
    {
        [Key]
        public int UserID { get; set; }
        public int RoleID { get; set; }
        [Display(Name ="نام کاربری")]
        [Required(ErrorMessage ="کاربر گرامی {0} را وارد کنید")]
        [MaxLength(250,ErrorMessage ="نام کاربری شما نمی تواند بیش از 250 کاراکتر باشد")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "کاربر گرامی {0} را وارد کنید")]
        [EmailAddress(ErrorMessage ="لطفا یک ایمیل معتبر وارد کنید")]
        public string Email { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "کاربر گرامی {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = " رمز عبور شما نمی تواند بیش از 150 کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "کد فعالسازی")]
        public string ActiveCode { get; set; }
        [Display(Name = "وضعیت حساب")]
        public bool IsActive { get; set; }
        [Display(Name = "تاریخ ثبت نام")]
        public System.DateTime RegisterDate { get; set; }
    }
}
