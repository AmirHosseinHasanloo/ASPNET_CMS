using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Model;
namespace My_CMS.Controllers
{
    public class ManageEmailsController : Controller
    {
        // GET: ManageEmails
        public ActionResult ActivationGmail()
        {
            return PartialView();
        }
        public ActionResult ResetPassword()
        {
            return PartialView();
        }
    }
}