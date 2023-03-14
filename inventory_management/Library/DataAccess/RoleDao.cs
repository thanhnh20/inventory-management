using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess
{

    public class RoleDao
    {
        private InventoryManagementContext _context;
        public RoleDao()
        {
            _context = new InventoryManagementContext();
        }


    }
}
