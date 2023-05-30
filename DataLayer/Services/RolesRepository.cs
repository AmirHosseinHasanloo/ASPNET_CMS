using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class RolesRepository : IRolesRepository
    {
        private EF_MyCMS_DBEntities db;

        public RolesRepository(EF_MyCMS_DBEntities context)
        {
            this.db = context;
        }

        public IEnumerable<Roles> GetAllRoles()
        {
            return db.Roles.ToList();
        }
    }
}
