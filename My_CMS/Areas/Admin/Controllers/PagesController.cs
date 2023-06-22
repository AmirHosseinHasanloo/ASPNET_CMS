using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.Model;
using KooyWebApp_MVC.Classes;
using My_DeepLearn.MyUtitlties;

namespace My_CMS.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        private EF_MyCMS_DBEntities db = new EF_MyCMS_DBEntities();
        IPagesRepository pagesRepository;
        IPageGroupsRepository pageGroupsRepository;
        public PagesController()
        {
            pagesRepository = new PagesRepository(db);
            pageGroupsRepository = new PageGroupsRepository(db);
        }

        // GET: Admin/Pages
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return View(pagesRepository.GetAllPages());
            }
            else
            {
                return View(pagesRepository.GetPagesByGroupId(id.Value));
            }
        }

        // GET: Admin/Pages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pages pages = pagesRepository.GetPagesById(id.Value);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View(pages);
        }

        // GET: Admin/Pages/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(pageGroupsRepository.GetAllGroups(), "GroupID", "GroupTitle");
            return View();
        }

        // POST: Admin/Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PageID,GroupID,PageTitle,ShortDescription,Text,ImageName,Visit,MyTags,ShowInSlider,CreateDate")] Pages pages, HttpPostedFileBase ImageUP, string MyTags)
        {
            if (ModelState.IsValid)
            {
                if (ImageUP.FileName != null && ImageUP.IsImage())
                {
                    pages.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUP.FileName);
                    ImageUP.SaveAs(Server.MapPath("/Images/NewsImages/" + pages.ImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Images/NewsImages/" + pages.ImageName), Server.MapPath("/Images/Thumb/" + pages.ImageName));
                }
                if (!string.IsNullOrEmpty(MyTags))
                {
                    pagesRepository.InsertTags(pages.PageID, MyTags);
                }
                pages.CreateDate = DateTime.Now;
                pages.Visit = 1;
                pagesRepository.InsertPage(pages);
                pagesRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(pageGroupsRepository.GetAllGroups(), "GroupID", "GroupTitle", pages.GroupID);
            return View(pages);
        }

        // GET: Admin/Pages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pages pages = pagesRepository.GetPagesById(id.Value);
            if (pages == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShowTags = string.Join(",", pagesRepository.GetTagsByPageId(pages.PageID).Select(t=>t.Tag));
            ViewBag.GroupID = new SelectList(pageGroupsRepository.GetAllGroups(), "GroupID", "GroupTitle", pages.GroupID);
            return View(pages);
        }

        // POST: Admin/Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PageID,GroupID,PageTitle,ShortDescription,Text,ImageName,Visit,ShowInSlider,CreateDate,MyTags")] Pages pages, HttpPostedFileBase ImageUP, string MyTags)
        {
            if (ModelState.IsValid)
            {
                if (ImageUP != null)
                {
                    if (ImageUP.FileName != null)
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/NewsImages/" + pages.ImageName));
                        System.IO.File.Delete(Server.MapPath("/Images/Thumb/" + pages.ImageName));
                    }
                    if (ImageUP.IsImage())
                    {
                        pages.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUP.FileName);
                        ImageUP.SaveAs(Server.MapPath("/Images/NewsImages/" + pages.ImageName));
                        ImageResizer img = new ImageResizer();
                        img.Resize(Server.MapPath("/Images/NewsImages/" + pages.ImageName), Server.MapPath("/Images/Thumb/" + pages.ImageName));
                    }
                }
                pagesRepository.DeleteTagsByPageId(pages.PageID);
                if (!string.IsNullOrEmpty(MyTags))
                {
                    pagesRepository.InsertTags(pages.PageID,MyTags);
                }
                pagesRepository.UpdatePage(pages);
                pagesRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(pageGroupsRepository.GetAllGroups(), "GroupID", "GroupTitle", pages.GroupID);
            return View(pages);
        }

        // GET: Admin/Pages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pages pages = pagesRepository.GetPagesById(id.Value);

            // Delete Picture Of News
            System.IO.File.Delete(Server.MapPath("/Images/NewsImages/" + pages.ImageName));
            System.IO.File.Delete(Server.MapPath("/Images/Thumb/" + pages.ImageName));

            //Delete Tags
            pagesRepository.DeleteTagsByPageId(pages.PageID);

            if (pages == null)
            {
                return HttpNotFound();
            }
            return View(pages);
        }

        // POST: Admin/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pages pages = pagesRepository.GetPagesById(id);
            pagesRepository.DeletePage(pages);
            pagesRepository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult SelectNewsGroup()
        {
            return PartialView(pageGroupsRepository.GetAllGroups());
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
