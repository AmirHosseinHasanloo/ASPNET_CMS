using DataLayer;
using DataLayer.Model;
using My_DeepLearn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace My_CMS.Controllers
{
    public class AccountController : Controller
    {
        EF_MyCMS_DBEntities db = new EF_MyCMS_DBEntities();
        IUsersRepository usersRepository;

        public AccountController()
        {
            usersRepository = new UsersRepository(db);
        }
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }
        [Route("Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var IsExists = usersRepository.IsExistsUserByEmail(register.Email.ToString());
                if (IsExists != null)
                {
                    Users user = new Users()
                    {
                        UserName = register.UserName,
                        Email = register.Email.Trim().ToLower(),
                        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                        ActiveCode = Guid.NewGuid().ToString(),
                        IsActive = false,
                        RoleID = 1,
                        RegisterDate = DateTime.Now,
                    };

                    usersRepository.InsertUser(user);
                    usersRepository.Save();

                    // Send Activation Email To User
                    string Body = PartialToStringClass.RenderPartialView("ManageEmails", "ActivationGmail", user);
                    SendEmail.Send(user.Email, "ایمیل فعالسازی حساب", Body);
                    //End Send Activation Email To User

                    return View("Success", user);
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربر گرامی حسابی با این ایمیل ثبت نام کرده است");
                }
            }
            return View(register);
        }
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login, string ReturnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                string HashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                var IsExists = usersRepository.IsExistsUserLogin(login.Email, HashPassword);
                if (IsExists != null)
                {
                    if (IsExists.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(IsExists.UserName, login.RememberMe);
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "کاربر گرامی این حساب فعال نمی باشد");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربر عزیز حسابی با این مشخصات موجود نمی باشد ابتدا ثبت نام کنید");
                }
            }
            return View(login);
        }

        [Route("LogOut")]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        public ActionResult ActiveUser(string id)
        {
            var isExists = usersRepository.IsExistsUserByActiveCode(id);
            if (isExists == null)
            {
                return HttpNotFound();
            }
            isExists.IsActive = true;
            isExists.ActiveCode = Guid.NewGuid().ToString();
            ViewBag.Name = isExists.UserName;
            usersRepository.Save();
            return View();
        }

        [Route("ForGotPass")]
        public ActionResult ForGotPassword()
        {
            return View();
        }
        [Route("ForGotPass")]
        [HttpPost]
        public ActionResult ForGotPassword(ForGotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var IsExists = usersRepository.IsExistsUserByEmail(model.Email);
                if (IsExists != null)
                {
                    if (IsExists.IsActive)
                    {
                        // Send Recovery Password Email
                        string Body = PartialToStringClass.RenderPartialView("ManageEmails", "ResetPassword",IsExists);
                        SendEmail.Send(model.Email, "ایمیل بازیابی رمز عبور", Body);
                        // End Send Recovery Password Email
                        return View("SuccesRestPassword", IsExists);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "کاربر گرامی این حساب فعال نمی باشد");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربر گرامی حسابی با این ایمیل ثبت نام نکرده است");
                }

            }
            return View(model);
        }

        public ActionResult RecoveryPassword(string id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult RecoveryPassword(string id, RecoveryPasswordViewModel recovery)
        {
            if (ModelState.IsValid)
            {
                var IsExists = usersRepository.IsExistsUserByActiveCode(id);
                if (IsExists == null) 
                {
                    return HttpNotFound();
                }
                IsExists.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(recovery.Password, "MD5");
                IsExists.ActiveCode = Guid.NewGuid().ToString();
                usersRepository.Save();
                return Redirect("/Login?Recovery=true");
            }
            return View(recovery);
        }

    }
}