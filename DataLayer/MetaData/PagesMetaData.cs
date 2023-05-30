using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLayer
{
    public class PagesMetaData
    {
        [Key]
        public int PageID { get; set; }
        [Display(Name = "گروه خبری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]      
        public int GroupID { get; set; }
        [Display(Name = "عنوان خبر")]
        public string PageTitle { get; set; }
        [Display(Name = "توضیح مختصر")]
        [MaxLength(500,ErrorMessage ="توضیح مختصر نباید بیش از 500 کراکتر شود")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }
        [Display(Name = "متن خبر")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Text { get; set; }
        [Display(Name = "تصویر خبر")]
        public string ImageName { get; set; }
        [Display(Name = "بازدید خبر")]
        public int Visit { get; set; }
        [Display(Name = "نمایش در اسلایدر")]
        public bool ShowInSlider { get; set; }
        [Display(Name = "تاریخ انتشار خبر")]
        public System.DateTime CreateDate { get; set; }
    }
}
