using DataLayer;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace My_CMS.Areas.UsersPanel.Controllers
{
    public class UserPanelController : Controller
    {
        private EF_MyCMS_DBEntities db = new EF_MyCMS_DBEntities();
        IUsersRepository usersRepository;
        public UserPanelController()
        {
            usersRepository = new UsersRepository(db);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var IsExists = usersRepository.IsExistsUserByEmail(model.Email.Trim().ToLower());
                if (IsExists!=null)
                {
                    string HashOldPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password, "MD5");
                    if (IsExists.Password == HashOldPassword)
                    {
                        string HashNewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(model.NewPassword, "MD5");
                        IsExists.Password = HashNewPassword;
                        usersRepository.Save();
                        ViewBag.Success = true;
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "کاربر گرامی لطفا رمز عبور فعلی خودتان را به درستی وارد کنید");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربر گرامی ایمیلی با این مشخصات ثبت نام نکرده است");
                }
            }
            return View(model);
        }
    }
}