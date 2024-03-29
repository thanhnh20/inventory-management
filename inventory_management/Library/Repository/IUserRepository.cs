﻿using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User checkLogin(string username, string password);
        IEnumerable<User> GetUserList();
        User GetUserByID(int userID);
        void UpdateUser(User user);
        void InsertUser(User user);
        void DeleteUser(User user);
        IEnumerable<User> SearchByNameAndId(string searchValue);
        void UpdateStatusUser(User userD);

        public List<User> getUserPage(int pageSize, int pageIndex);

        public int getTotalUserPage();
    }
}
