using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer;
using DataLayer.Model;

namespace My_CMS.Areas.ManageUsers.Controllers
{
    [Authorize(Roles="Admin")]
    public class UsersController : Controller
    {
        private EF_MyCMS_DBEntities db = new EF_MyCMS_DBEntities();
        IUsersRepository usersRepository;
        IRolesRepository rolesRepository;

        public UsersController()
        {
            usersRepository = new UsersRepository(db);
            rolesRepository = new RolesRepository(db);
        }

        // GET: ManageUsers/Users
        public ActionResult Index()
        {
            return View(usersRepository.GetAllUsers());
        }

        // GET: ManageUsers/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = usersRepository.GetUsersById(id.Value);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: ManageUsers/Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(rolesRepository.GetAllRoles(), "RoleID", "RoleTitle");
            return View();
        }

        // POST: ManageUsers/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,RoleID,UserName,Email,Password,ActiveCode,IsActive,RegisterDate")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.RegisterDate = DateTime.Now;
                users.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(users.Password, "MD5");
                users.ActiveCode = Guid.NewGuid().ToString();
                usersRepository.InsertUser(users);
                usersRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(rolesRepository.GetAllRoles(), "RoleID", "RoleTitle", users.RoleID);
            return View(users);
        }

        // GET: ManageUsers/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = usersRepository.GetUsersById(id.Value);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(rolesRepository.GetAllRoles(), "RoleID", "RoleTitle", users.RoleID);
            return View(users);
        }

        // POST: ManageUsers/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,RoleID,UserName,Email,Password,ActiveCode,IsActive,RegisterDate")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(users.Password, "MD5");
                usersRepository.UpdateUser(users);
                usersRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(rolesRepository.GetAllRoles(), "RoleID", "RoleTitle", users.RoleID);
            return View(users);
        }

        // GET: ManageUsers/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = usersRepository.GetUsersById(id.Value);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: ManageUsers/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = usersRepository.GetUsersById(id);
            usersRepository.DeleteUser(users);
            usersRepository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult SearchForUsers(string id)
        {
            return View(usersRepository.SearchUser(id));
        }

        public ActionResult Admins()
        {
            return View(usersRepository.ReturnAdmins());
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
