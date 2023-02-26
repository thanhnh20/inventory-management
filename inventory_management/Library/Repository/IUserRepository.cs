using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public interface IUserRepository
    {
        public User checkLogin(string username, string password);
        IEnumerable<User> GetUserList();
        User GetUserByID(int userID);
        void UpdateUser(User user);
        void InsertUser(User user);
        void DeleteUser(User user);
    }
}
