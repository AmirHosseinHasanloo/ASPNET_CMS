using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PagesRepository : IPagesRepository
    {
        private EF_MyCMS_DBEntities db;
        public PagesRepository(EF_MyCMS_DBEntities context)
        {
            this.db = context;
        }
        public IEnumerable<Pages> GetAllPages()
        {
            return db.Pages.ToList();
        }

        public Pages GetPagesById(int pageid)
        {
            return db.Pages.Find(pageid);
        }

        public bool InsertPage(Pages page)
        {
            try
            {
                db.Pages.Add(page);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdatePage(Pages page)
        {
            try
            {
                db.Entry(page).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeletePage(Pages page)
        {
            try
            {
                db.Entry(page).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeletePage(int pageid)
        {
            try
            {
                var delete = db.Pages.Find(pageid);
                DeletePage(delete);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void InsertTags(int id, string tags)
        {
            string[] NewTags = tags.Split(',');
            foreach (string T in NewTags)
            {
                db.Tags.Add(new Tags()
                {
                    PageID = id,
                    Tag = T.Trim(),
                });
            }
        }

        public IEnumerable<Pages> GetPagesByGroupId(int groupid)
        {
            return db.Pages.Where(P => P.GroupID == groupid).ToList();
        }

        public void DeleteTagsByPageId(int pageid)
        {
            db.Tags.Where(t => t.PageID == pageid).ToList().ForEach(t => db.Tags.Remove(t));
        }

        public IEnumerable<Tags> GetTagsByPageId(int pageid)
        {
            return db.Tags.Where(t => t.PageID == pageid).ToList();
        }

        public IEnumerable<Pages> ShowPagesOnSlider()
        {
            return db.Pages.Where(P => P.ShowInSlider == true).ToList();
        }

        public IEnumerable<Pages> ShowPagesByGroupId(int groupid)
        {
            return db.Pages.Where(p => p.GroupID == groupid);
        }

        public IEnumerable<Pages> MostVisitedPage()
        {
            return db.Pages.OrderByDescending(p => p.Visit);
        }

        public IEnumerable<Pages> LastReleasedNews()
        {
            return db.Pages.OrderByDescending(P => P.CreateDate).Take(6);
        }


        public IEnumerable<Pages> SearchPages(string q)
        {
            List<Pages> list = new List<Pages>();
            list.AddRange(db.Tags.Where(T => T.Tag == q).Select(T => T.Pages).ToList());
            list.AddRange(db.Pages.Where(P => P.PageTitle.Contains(q) || P.Text.Contains(q) ||
             P.ShortDescription.Contains(q) || P.Page_Groups.GroupTitle.Contains(q)).ToList());
            return list.Distinct();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
