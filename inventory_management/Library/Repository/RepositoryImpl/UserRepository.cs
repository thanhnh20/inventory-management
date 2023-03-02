using Library.DataAccess;
using Library.Model;
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

        public void DeleteUser(User user) => UserDAO.Instance.DeleteUser(user);

        public User GetUserByID(int userID) => UserDAO.Instance.GetUserByID(userID);

        public IEnumerable<User> GetUserList() => UserDAO.Instance.GetUserList();

        public void InsertUser(User user) => UserDAO.Instance.InsertUser(user);

        public void UpdateUser(User user) => UserDAO.Instance.UpdateUser(user);

    }
}
