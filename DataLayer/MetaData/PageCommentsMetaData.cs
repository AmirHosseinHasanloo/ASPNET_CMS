using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PageCommentsMetaData
    {
        [Key]
        public int CommentID { get; set; }
        [Display(Name = "صفحه ی خبر")]
        public int PageID { get; set; }
        [Display(Name = "نام کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = " نام کاربری شما نمی تواند بیش از 350 کاراکتر باشد")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage ="لطفا یک ایمیل معتبر وارد کنید")]
        public string Email { get; set; }
        [Display(Name = "نظر ارزشمند شما")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [MaxLength(1200, ErrorMessage = " نظر شما نمی تواند بیش از 1200 کاراکتر باشد")]
        public string Comment { get; set; }
        [Display(Name = "تاریخ ثبت نظر")]
        public System.DateTime CreateDate { get; set; }
    }
}
