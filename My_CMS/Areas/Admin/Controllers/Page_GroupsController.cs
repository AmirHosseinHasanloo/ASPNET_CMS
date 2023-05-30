using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.Model;

namespace My_CMS.Areas.Admin.Controllers
{
    [Authorize(Roles="Admin")]
    public class Page_GroupsController : Controller
    {
        private EF_MyCMS_DBEntities db = new EF_MyCMS_DBEntities();
        IPageGroupsRepository pageGroupsRepository;

        public Page_GroupsController()
        {
            pageGroupsRepository = new PageGroupsRepository(db);
        }

        // GET: Admin/Page_Groups
        public ActionResult Index()
        {
            return View(pageGroupsRepository.GetAllGroups());
        }

        // GET: Admin/Page_Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page_Groups page_Groups = pageGroupsRepository.GetGroupById(id.Value);
            if (page_Groups == null)
            {
                return HttpNotFound();
            }
            return View(page_Groups);
        }

        // GET: Admin/Page_Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Page_Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,GroupTitle")] Page_Groups page_Groups)
        {
            if (ModelState.IsValid)
            {
                pageGroupsRepository.InsertGroup(page_Groups);
                pageGroupsRepository.Save();
                return RedirectToAction("Index");
            }

            return View(page_Groups);
        }

        // GET: Admin/Page_Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page_Groups page_Groups = pageGroupsRepository.GetGroupById(id.Value);
            if (page_Groups == null)
            {
                return HttpNotFound();
            }
            return View(page_Groups);
        }

        // POST: Admin/Page_Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,GroupTitle")] Page_Groups page_Groups)
        {
            if (ModelState.IsValid)
            {
                pageGroupsRepository.UpdateGroup(page_Groups);
                pageGroupsRepository.Save();
                return RedirectToAction("Index");
            }
            return View(page_Groups);
        }

        // GET: Admin/Page_Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page_Groups page_Groups = pageGroupsRepository.GetGroupById(id.Value);
            if (page_Groups == null)
            {
                return HttpNotFound();
            }
            return View(page_Groups);
        }

        // POST: Admin/Page_Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Page_Groups page_Groups = pageGroupsRepository.GetGroupById(id);
            pageGroupsRepository.DeleteGroup(page_Groups);
            pageGroupsRepository.Save();
            return RedirectToAction("Index");
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
