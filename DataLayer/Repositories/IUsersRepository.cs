using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IUsersRepository
    {
        IEnumerable<Users> GetAllUsers();
        Users GetUsersById(int userId);
        bool InsertUser(Users user);
        bool UpdateUser(Users user);
        bool DeleteUser(Users user);
        bool DeleteUser(int userId);
        Users IsExistsUserByEmail(string email);
        Users IsExistsUserByActiveCode(string activecode);
        Users IsExistsUserLogin(string Email, string password);
        IEnumerable<Users> SearchUser(string q);
        IEnumerable<Users> ReturnAdmins();
        void Save();
    }
}
