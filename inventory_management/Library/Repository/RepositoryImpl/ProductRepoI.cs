using Library.DataAccess;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.RepositoryImpl
{
    public class ProductRepoI : ProductRepo
    {
        public ProductDAO productDAO;
        public ProductRepoI() {
            productDAO = new ProductDAO();
        }

        public void Add(Product product)
        {
            productDAO.Add(product);
        }

        public IEnumerable<Product> GetAll()
        {
            return productDAO.GetAll();
        }
        
    }
}
