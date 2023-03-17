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

        public void DeleteProductByID(Product product)
        {
            ProductDAO.Instance.DeleteProductByID(product);
        }

        public IEnumerable<Product> GetAll() => ProductDAO.Instance.GetAll();

        public IEnumerable<Product> GetAllAndDescending(string name) => ProductDAO.Instance.GetAllAndDescending(name);

        public Product GetProductById(int id) => ProductDAO.Instance.GetProductByID(id);
        public List<Product> GetProducts() => ProductDAO.Instance.GetProduct();
        public void UpdateProduct(Product product) => ProductDAO.Instance.UpdateProduct(product);

    }
}
