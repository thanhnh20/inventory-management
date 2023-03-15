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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(InventoryManagementContext context) : base(context)
        {
        }

        public void Add(Product product) => ProductDAO.Instance.Add(product);


        public IEnumerable<Product> GetAll() => ProductDAO.Instance.GetAll();

        public Product GetProductById(int id) => ProductDAO.Instance.GetProductByID(id);
        public List<Product> GetProducts() => ProductDAO.Instance.GetProduct();

    }
}
