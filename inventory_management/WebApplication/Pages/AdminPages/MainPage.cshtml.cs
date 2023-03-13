using DataAccess.Repository;
using DataAccess.Repository.RepositoryImpl;
using Library.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Pages.AdminPages
{
    public class MainPageModel : PageModel
    {

        private IUserRepository userRepository;

        private readonly ILogger _logger;

        public string Error { get; set; }

        private static readonly string _urlHomePage = "~/HomePages/Home";
        public MainPageModel(ILogger<MainPageModel> logger)
        {
            userRepository = new UserRepository();
            _logger = logger;   
        }

        public IList<User> User { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("ADMIN"));
                User = userRepository.GetUserList().ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message + " at MainPageModel");    
                Error = ex.Message;
            }
            return Page();
        }

        public IActionResult OnGetLogOut()
        {
            HttpContext.Session.Remove("ADMIN");
            return Redirect(_urlHomePage);
        }

        public IActionResult OnGetLogin()
        {
            return Redirect(_urlHomePage);
        }

    }
}
