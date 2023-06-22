using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PageCommentRepository : IPageCommentRepository
    {
        private EF_MyCMS_DBEntities db;
        public PageCommentRepository(EF_MyCMS_DBEntities context)
        {
            this.db= context;
        }
        public IEnumerable<Comments> GetAllComments()
        {
            return db.Comments.ToList();
        }

        public IEnumerable<Comments> GetCommentById(int pageid)
        {
            return db.Comments.Where(C => C.PageID == pageid).ToList();
        }

        public bool InsertComment(Comments comment)
        {
            try
            {
                db.Comments.Add(comment);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateComment(Comments comment)
        {
            try
            {
                db.Entry(comment).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteComment(Comments comment)
        {
            try
            {
                db.Entry(comment).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteComment(int commentid)
        {
            try
            {
                var delete = db.Comments.Find(commentid);
                DeleteComment(delete);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Save()
        {
           db.SaveChanges();
        }
    }
}
