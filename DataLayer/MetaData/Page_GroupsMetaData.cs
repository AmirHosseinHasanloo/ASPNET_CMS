using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Page_GroupsMetaData
    {
        [Key]
        public int GroupID { get; set; }
        [Display(Name ="عنوان گروه خبری")]
        [Required(ErrorMessage ="عنوان گروه خبری را وارد کنید")]
        [MaxLength(350,ErrorMessage ="عنوان گروه خبری شما نمی تواند بیش از 350 کرکتر باشد")]
        public string GroupTitle { get; set; }
    }
}
