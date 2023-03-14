using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public interface ProductRepo
    {
        IEnumerable<Product> GetAll();
        void Add(Product product);
    }

}
