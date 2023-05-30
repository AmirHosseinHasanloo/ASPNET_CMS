using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer
{
    public class PageGroupsRepository : IPageGroupsRepository
    {
        private EF_MyCMS_DBEntities db;
        public PageGroupsRepository(EF_MyCMS_DBEntities context)
        {
            this.db = context;
        }
        public IEnumerable<Page_Groups> GetAllGroups()
        {
            return db.Page_Groups.ToList();
        }

        public Page_Groups GetGroupById(int groupid)
        {
            return db.Page_Groups.Find(groupid);
        }

        public bool InsertGroup(Page_Groups group)
        {
            try
            {
                db.Page_Groups.Add(group);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateGroup(Page_Groups group)
        {
            try
            {
                db.Entry(group).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteGroup(Page_Groups group)
        {
            try
            {
                db.Entry(group).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteGroup(int groupid)
        {
            try
            {
                var Delete = db.Page_Groups.Find(groupid);
                db.Entry(Delete).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
