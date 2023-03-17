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
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;


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
        public void UpdateProduct(Product product)
        {
            var flag = GetProductByID(product.ProductId);
            if (product.Image !=null) {
               
                if (flag != null)
                {
                    flag.ProductName = product.ProductName;
                    flag.Description = product.Description;
                    flag.CategoryId = product.CategoryId;
                    flag.Image = product.Image;
                    flag.Unit = product.Unit;
                    flag.ImportPrice = product.ImportPrice;
                    flag.SellingPrice = product.SellingPrice;
                    flag.TotalQuantity = product.TotalQuantity;
                    flag.Status = product.Status;
                    db.SaveChanges();
                }
            }
            else
            {

                if (flag != null)
                {

                    flag.ProductName = product.ProductName;
                    flag.Description = product.Description;
                    flag.CategoryId = product.CategoryId;

                    
                    flag.Unit = product.Unit;
                    flag.ImportPrice = product.ImportPrice;
                    flag.SellingPrice = product.SellingPrice;
                    flag.TotalQuantity = product.TotalQuantity;
                    flag.Status = product.Status;
                    db.SaveChanges();
                }
            }
        }
        public void DeleteProductByID(int productId)
        {
            
                Product check = GetProductByID(productId);
                if (check != null)
                {
                    using (var db = new InventoryManagementContext())
                    {
                        check = db.Products.Where(m => m.ProductId == productId).First();
                        check.Status = 0;
                        db.SaveChanges();
                        Console.WriteLine("Save successfully");
                    }
                }
                else
                {
                    throw new Exception("User exists already!");
                }


         }
        public IEnumerable<Product> GetAllAndDescending(string name)
        {
            return db.Products.Where(p => p.ProductName == name).OrderByDescending(p => p.Status).ToList();
        }
    }
}

