using Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess
{
    public class CategoryDAO
    {
        private InventoryManagementContext _context;
        public CategoryDAO()
        {
            _context = new InventoryManagementContext();
        }
        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
    }
}
