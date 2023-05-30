using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace DataLayer
{
    public class UsersRepository : IUsersRepository
    {
        private EF_MyCMS_DBEntities db;

        public UsersRepository(EF_MyCMS_DBEntities context)
        {
            this.db = context;
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return db.Users.ToList();
        }

        public Users GetUsersById(int userId)
        {
            return db.Users.Find(userId);
        }

        public bool InsertUser(Users user)
        {
            try
            {
                db.Users.Add(user);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateUser(Users user)
        {
            try
            {
                db.Entry(user).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteUser(Users user)
        {
            try
            {
                db.Entry(user).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                var Delete = db.Users.Find(userId);
                DeleteUser(Delete);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Users IsExistsUserByEmail(string email)
        {
            return db.Users.SingleOrDefault(u => u.Email == email.Trim().ToLower());
        }

        public Users IsExistsUserByActiveCode(string activecode)
        {
            return db.Users.SingleOrDefault(u => u.ActiveCode == activecode);
        }

        public Users IsExistsUserLogin(string Email, string password)
        {
            return db.Users.SingleOrDefault(u => u.Email == Email.Trim().ToLower() && u.Password == password);
        }

        public IEnumerable<Users> SearchUser(string q)
        {
            return db.Users.Where(U => U.UserName.Contains(q) || U.Email.Contains(q)).ToList();
        }

        public IEnumerable<Users> ReturnAdmins()
        {
            return db.Users.Where(U => U.Roles.RoleID == 2).ToList();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
