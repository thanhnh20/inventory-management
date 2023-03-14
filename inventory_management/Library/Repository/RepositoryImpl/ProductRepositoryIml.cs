using Library.DataAccess;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.RepositoryImpl
{
    public class ProductRepositoryIml : IProductRepository
    {
        public Product GetProductById(int id) => ProductDAO.Instance.GetProductByID(id);
        public List<Product> GetProducts() => ProductDAO.Instance.GetProduct();
    }
}
