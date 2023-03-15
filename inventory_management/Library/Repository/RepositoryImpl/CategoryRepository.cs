using Library.DataAccess;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.RepositoryImpl
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryDAO categoryDAO;

        public CategoryRepository(InventoryManagementContext context) : base(context)
        {
            categoryDAO = new CategoryDAO();
        }

        public IEnumerable<Category> GetAll()
        {
            return categoryDAO.GetAll();
        }
    }
}
