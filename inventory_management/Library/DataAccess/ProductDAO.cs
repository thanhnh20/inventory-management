using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Product GetProductByID(int productID) => db.Products.Find(productID);

        public List<Product> GetProduct() => db.Products.ToList();
    }
}
