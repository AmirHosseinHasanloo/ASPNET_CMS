using DataLayer;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My_CMS.Controllers
{
    public class SearchController : Controller
    {
        EF_MyCMS_DBEntities db = new EF_MyCMS_DBEntities();
        IPagesRepository pagesRepository;

        public SearchController()
        {
            pagesRepository = new PagesRepository(db);
        }

        public ActionResult Index(string q)
        {
            ViewBag.T = q;
            return View(pagesRepository.SearchPages(q));
        }
    }
}