using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IPagesRepository
    {
        IEnumerable<Pages> GetAllPages();
        Pages GetPagesById(int pageid);
        bool InsertPage(Pages page);
        bool UpdatePage(Pages page);
        bool DeletePage(Pages page);
        bool DeletePage(int pageid);
        void InsertTags(int id, string tags);
        IEnumerable<Pages> GetPagesByGroupId(int groupid);
        IEnumerable<Pages> ShowPagesOnSlider();
        void DeleteTagsByPageId(int pageid);
        IEnumerable<Tags> GetTagsByPageId(int pageid);
        IEnumerable<Pages> ShowPagesByGroupId(int groupid);
        IEnumerable<Pages> MostVisitedPage();
        IEnumerable<Pages> LastReleasedNews();
        IEnumerable<Pages> SearchPages(string q);
        void Save();
    }
}
