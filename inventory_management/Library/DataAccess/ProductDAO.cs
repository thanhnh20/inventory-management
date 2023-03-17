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
        InventoryManagementContext db = new InventoryManagementContext();

        private static ProductDAO instance = null;
        private static readonly object instancelock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Product> GetAll()
        {
            using (var db = new InventoryManagementContext())
            {
                return db.Products.Include(p => p.Category).ToList();
            }
            
        }
        public void Add(Product product)
        {
            db.Add(product);
            db.SaveChanges();
        }


        public List<Product> GetProduct() {
            using (var db = new InventoryManagementContext())
            {
                return db.Products.Include(c => c.Category).ToList();
            }
        }

        public Product GetProductByID(int productID)
        {
            using (var db = new InventoryManagementContext())
            {
                return db.Products.Where(p => p.ProductId == productID).Include(c => c.Category).FirstOrDefault();
            }
        }

    }
}
