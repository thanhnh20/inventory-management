using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess
{
    public class UserDAO
    {
        InventoryManagementContext db = new InventoryManagementContext();

        private static UserDAO instance = null;
        private static readonly object instancelock = new object();
        private UserDAO() { }
        public static UserDAO Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }

        public User checkLogin(string username, string password)
            => db.Users.Where(m => m.Username.Equals(username) && m.Password.Equals(password)).FirstOrDefault();
    }
}
