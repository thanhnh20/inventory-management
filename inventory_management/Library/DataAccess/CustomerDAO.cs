using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess
{
    public class CustomerDAO
    {

            private static CustomerDAO instance = null;
            private static readonly object instancelock = new object();
            private CustomerDAO() { }
            public static CustomerDAO Instance
            {
                get
                {
                    lock (instancelock)
                    {
                        if (instance == null)
                        {
                            instance = new CustomerDAO();
                        }
                        return instance;
                    }
                }
            }

            public List<Customer> GetALl()
        {
            using (var db = new InventoryManagementContext())
            {
                return db.Customers.ToList();
            }
        }
    }
}
