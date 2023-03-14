using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public interface IProductRepository
    {
        public Product GetProductById(int id);

        public List<Product> GetProducts();
    }
}
