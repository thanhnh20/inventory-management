using Library.DataAccess;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.RepositoryImpl
{
    public class UserRepository : IUserRepository
    {
        public User checkLogin(string username, string password) => UserDAO.Instance.checkLogin(username, password);
    }
}
