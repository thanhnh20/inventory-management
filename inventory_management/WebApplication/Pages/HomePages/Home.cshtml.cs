using Library.Models;
using Library.Repository;
using Library.Repository.RepositoryImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace WebApplication.Pages.HomePages
{
    public class HomeModel : PageModel
    {
        private IUserRepository userRepository;
        public HomeModel()
        {
            userRepository = new UserRepository();
        }

        [BindProperty]
        public User User { get; set; }

        public string errorMSg { get; set; }

        private static string url { get; set; }


        public IActionResult OnPost()
        {
            if(User.Username != null && User.Password != null)
            {
                var account = userRepository.checkLogin(User.Username, User.Password);
                if(account != null)
                {
                    if(account.RoleId == 0)
                    {
                        // set admin session
                        HttpContext.Session.SetString("ADMIN", JsonConvert.SerializeObject(account));
                        // redirect to admin page
                        url = "~/AdminPages/MainPage";
                        return Redirect(url);

                    }
                    else if(account.RoleId == 1)
                    {
                        // set staff to session
                        HttpContext.Session.SetString("STAFF", JsonConvert.SerializeObject(account));
                        // redirect to staff page
                        url = "~/StaffPages/MainPage";
                        return Redirect(url);
                    }
                }
                else
                {
                    errorMSg = "Username or Password is invalid";
                }
            }
            return Page();
        }
    }
}
