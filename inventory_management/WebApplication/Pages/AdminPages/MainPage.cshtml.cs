using Library.Model;
using Library.Repository;
using Library.Repository.RepositoryImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Pages.AdminPages
{
    public class MainPageModel : PageModel
    {   
        
        private IUserRepository userRepository;

        public IList<User> User { get; set; }

        public string SearchString { get; set; }

        public User CurUser { get; set; }

        public MainPageModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult OnGet(string searchString)
        {
            
            CurUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("ADMIN"));
            if(CurUser == null)
            {
                CurUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("STAFF"));
                if(CurUser == null)
                {
                   return RedirectToPage("/Login");
                }
            }
            if (searchString != null)
            {
                SearchString= searchString;
                var task = userRepository.SearchByNameAndId(searchString);
                if (task == null) 
                {
                    User = userRepository.GetUserList().ToList();
                }
                else
                {
                    User = (IList<User>)task;
                }
            }
            else
            {
                User = (IList<User>) userRepository.GetUserList();
            }
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("ADMIN");
            return Redirect("~/HomePages/Home");
        }

    }
}
