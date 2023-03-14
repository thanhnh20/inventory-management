using Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
namespace Library.DataAccess
{
    public class ProductDAO
    {
        private InventoryManagementContext inventoryManagementContext;
        
        public ProductDAO() {
        inventoryManagementContext= new InventoryManagementContext();
        }
        public IEnumerable<Product> GetAll()
        {
            return inventoryManagementContext.Products.Include(p => p.Category);
        }
        public void Add(Product product)
        {
            inventoryManagementContext.Add(product);
            inventoryManagementContext.SaveChanges();
        }

    }
}
