using Library.Models;
using Library.Repository;
using Library.Repository.RepositoryImpl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Pages.AdminPages
{
    public class MainPageModel : PageModel
    {   
        
        private IUserRepository userRepository;

        public IList<User> User { get; set; }

        public string SearchString { get; set; }

        public void OnGet(string searchString)
        {
            userRepository = new UserRepository();
            if (searchString != null)
            {
                SearchString= searchString;
                var task = userRepository.SearchByNameAndId(searchString);
                if(task == null) 
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
            
            /*userRepository = new UserRepository();
            User = userRepository.GetUserList().ToList();*/
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("ADMIN");
            return RedirectToPage("/Login");
        }

    }
}
