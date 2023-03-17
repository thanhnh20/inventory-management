using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Product GetProductById(int id);

        public List<Product> GetProducts();

        IEnumerable<Product> GetAll();
        void Add(Product product);
        void UpdateProduct(Product product);
        void DeleteProductByID(Product product);
        IEnumerable<Product> GetAllAndDescending(string name);
    }
}
