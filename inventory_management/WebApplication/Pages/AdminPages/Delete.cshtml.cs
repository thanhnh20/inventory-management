using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Repository;
using Library.Repository.RepositoryImpl;

namespace WebApplication.Pages.AdminPages
{
    public class DeleteModel : PageModel
    {
        private IUserRepository userRepository;

        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGet(int? id)
        {
            userRepository = new UserRepository();

            if (id == null)
            {
                return NotFound();
            }

            User = userRepository.GetUserByID((int)id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {

            userRepository = new UserRepository();

            if (id == null)
            {
                return NotFound();
            }

            User = userRepository.GetUserByID((int)id);
            
            if (User != null)
            {
                userRepository.DeleteUser(User);
            }

            return RedirectToPage("./MainPage");
        }
    }
}
