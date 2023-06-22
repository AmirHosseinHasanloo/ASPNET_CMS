using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IPageCommentRepository
    {
        IEnumerable<Comments> GetAllComments();
        IEnumerable<Comments> GetCommentById(int pageid);
        bool InsertComment(Comments comment);
        bool UpdateComment(Comments comment);
        bool DeleteComment(Comments comment);
        bool DeleteComment(int commentid);
        void Save();
    }
}
