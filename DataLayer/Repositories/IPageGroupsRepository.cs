using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IPageGroupsRepository
    {
        IEnumerable<Page_Groups> GetAllGroups();
        Page_Groups GetGroupById(int groupid);
        bool InsertGroup(Page_Groups group);
        bool UpdateGroup(Page_Groups group);
        bool DeleteGroup(Page_Groups group);
        bool DeleteGroup(int groupid);
        void Save();
    }
}
