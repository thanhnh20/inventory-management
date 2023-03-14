using Library.DataAccess;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.RepositoryImpl
{
    public class CategoryRepoI : CategoryRepo
    {
        public CategoryDAO categoryDAO;
        public CategoryRepoI()
        {
            categoryDAO = new CategoryDAO();
        }
        public IEnumerable<Category> GetAll()
        {
            return categoryDAO.GetAll();
        }
    }
}
