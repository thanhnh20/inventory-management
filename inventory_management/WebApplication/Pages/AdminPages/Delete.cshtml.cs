using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using Library.Repository;
using Library.Repository.RepositoryImpl;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebApplication.Pages.AdminPages
{
    public class DeleteModel : PageModel
    {
        private IUserRepository userRepository;
        private readonly ILogger _logger;
        public string Error { get; set; }
        public DeleteModel(ILogger<MainPageModel> logger, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            _logger = logger;
        }
        [BindProperty]
        public User User { get; set; }
        private static readonly string _urlHomePage = "~/HomePages/Home";


        public IActionResult OnGet(int? id)
        {
            try
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("ADMIN"));
                if (id == null)
                {
                    return NotFound();
                }

                User = userRepository.GetUserByID((int)id);

                if (User == null)
                {
                    return NotFound();
                }
            
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message + " at MainPageModel");
                Error = ex.Message;
            }
            return Page();

        }

        public IActionResult OnPost(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            User = userRepository.GetUserByID((int)id);
            
            if (User != null)
            {
                userRepository.UpdateStatusUser(User);
            }

            return RedirectToPage("./MainPage");
        }
        public IActionResult OnGetLogOut()
        {
            HttpContext.Session.Remove("ADMIN");
            return Redirect(_urlHomePage);
        }
    }
}
