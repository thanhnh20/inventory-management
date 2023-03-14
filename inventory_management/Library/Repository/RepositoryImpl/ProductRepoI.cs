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
        public void Add(Product product) => ProductDAO.Instance.Add(product);


        public IEnumerable<Product> GetAll() => ProductDAO.Instance.GetAll();
        
    }
}
