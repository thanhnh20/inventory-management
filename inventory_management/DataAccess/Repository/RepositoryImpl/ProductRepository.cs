using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.RepositoryImpl
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(InventoryManagementContext context) : base(context)
        {
        }
    }
}
