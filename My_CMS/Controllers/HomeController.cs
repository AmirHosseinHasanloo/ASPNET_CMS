using DataLayer;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Web.UI.WebControls.WebParts;

namespace My_CMS.Controllers
{
    public class HomeController : Controller
    {
        private EF_MyCMS_DBEntities db = new EF_MyCMS_DBEntities();
        IPagesRepository pagesRepository;
        IPageGroupsRepository pageGroupsRepository;
        IPageCommentRepository pageCommentRepository;

        public HomeController()
        {
            pageGroupsRepository = new PageGroupsRepository(db);
            pagesRepository = new PagesRepository(db);
            pageCommentRepository = new PageCommentRepository(db);
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Slider()
        {
            return PartialView(pagesRepository.ShowPagesOnSlider());
        }
        public ActionResult ShowLastNews()
        {
            return PartialView(pagesRepository.LastReleasedNews());
        }
        public ActionResult ShowNewsGroupsOnMenu()
        {
            return PartialView(pageGroupsRepository.GetAllGroups());
        }
        public ActionResult ShowGroupsOnList()
        {
            return PartialView(pageGroupsRepository.GetAllGroups());
        }
        [Route("ShowPages/{id}")]
        public ActionResult ShowPagesByGroupId(int? id)
        {
            ViewBag.NewsGroupTitle = pageGroupsRepository.GetGroupById(id.Value).GroupTitle;
            return View(pagesRepository.ShowPagesByGroupId(id.Value));
        }
        [Route("Pages/{id}")]
        public ActionResult ShowPagesByPageId(int? id)
        {
            var Page = pagesRepository.GetPagesById(id.Value);
            Page.Visit++;
            pagesRepository.Save();
            return View(Page);
        }
        public ActionResult MostVisitedNews()
        {
            return PartialView(pagesRepository.MostVisitedPage());
        }

        // Archive Without The Pagging System...    
        public ActionResult ArchiveNews(int? PageNumber)
        {
            var Temp = pagesRepository.GetAllPages().ToPagedList(PageNumber ?? 1, 3);
            return View(Temp);
        }

        public ActionResult AddComment(int id, string Name, string Email, string Comment)
        {
            Comments comment = new Comments()
            {
                PageID = id,
                UserName = Name,
                Email = Email,
                Comment = Comment,
                CreateDate = DateTime.Now,
            };
            pageCommentRepository.InsertComment(comment);
            pageCommentRepository.Save();
            return PartialView("ShowCommentsByPageId", pageCommentRepository.GetCommentById(id));
        }
        public ActionResult ShowCommentsByPageId(int id)
        {
            return PartialView(pageCommentRepository.GetCommentById(id));
        }
        public ActionResult AdminIdentity()
        {
            return PartialView();
        }
    }
}