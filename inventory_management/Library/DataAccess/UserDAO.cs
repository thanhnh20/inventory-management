using Library.Model;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<User> GetUserList()
        {
            var list = new List<User>();
            using(var db = new InventoryManagementContext()) 
            {
                list = db.Users.Where(c => c.RoleId == 1).ToList();
            }

            return list;
        }

        public User GetUserByID(int userID)
        {
            User _user = null;
            try
            {
                _user = db.Users.FirstOrDefault(s => s.UserId == userID);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _user;
        }
        public void UpdateStatusUser(User user)
        {
            User check = GetUserByID(user.UserId);
            if (check != null)
            {
                using (var db = new InventoryManagementContext())
                {
                    check = db.Users.Where(m => m.UserId == user.UserId).First();
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

        public void DeleteUser(User user)
        {
            try
            {
                User _user = GetUserByID(user.UserId);
                if(_user != null)
                {
                    db.Users.Remove(_user);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("The user does not already exist");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertUser(User user)
        {
            User check = GetUserByID(user.UserId);
            if (check == null)
            {
                using (var db = new InventoryManagementContext())
                {
                    check = db.Users.Where(m => m.UserId == user.UserId).FirstOrDefault();
                    db.Users.Add(new User
                    {
                        Username = user.Username,
                        FullName = user.FullName,
                        Password = user.Password,
                        RoleId = user.RoleId,
                        Gender = user.Gender,
                        BirthDay = user.BirthDay,
                        PhoneNumber = user.PhoneNumber,
                        Address = user.Address,
                        Status = user.Status,
                    });
                    db.SaveChanges();
                    Console.WriteLine("Save successfully");
                }
            }
            else
            {
                throw new Exception("User exists already!");
            }
        }

        public void UpdateUser(User user)
        {
            User check = GetUserByID(user.UserId);
            if (check != null)
            {
                using (var db = new InventoryManagementContext())
                {

                    check = db.Users.Where(m => m.UserId == user.UserId).First();
                    check.Username = user.Username;
                    check.FullName = user.FullName;
                    check.Password = user.Password;
                    check.RoleId = user.RoleId;
                    check.Gender = user.Gender;
                    check.BirthDay = user.BirthDay;
                    check.PhoneNumber = user.PhoneNumber;
                    check.Address = user.Address;
                    check.Status = user.Status;
                    db.SaveChanges();
                }
            }
            else
            {
                throw new Exception("Member does not exist!!!");
            }
        }

        public IEnumerable<User> SearchByNameAndId(string searchValue)
        {
            var task = db.Users.Where(c => c.FullName.Contains(searchValue) || c.Username.Contains(searchValue)).ToList();
            return task;
        }

        public int getTotalUserPage()
        {
            return db.Users.Count();
        }

        public List<User> getUserPage(int pageSize, int pageIndex)
        {
            return db.Users.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
