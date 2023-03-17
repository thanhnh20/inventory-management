using Library.DataAccess;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.RepositoryImpl
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(InventoryManagementContext context) : base(context)
        {
        }

        public List<Customer> GetAll() => CustomerDAO.Instance.GetALl();
    }
}
