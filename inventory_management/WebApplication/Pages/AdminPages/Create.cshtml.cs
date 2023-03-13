using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Model;
using Library.Repository;
using Library.Repository.RepositoryImpl;

namespace WebApplication.Pages.AdminPages
{
    public class CreateModel : PageModel
    {
        private IUserRepository userRepository;

       

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            userRepository = new UserRepository();

            if (User.UserId == null)
            {
                return RedirectToPage("../Error");
            }
            else
            {
                userRepository.InsertUser(User);
            }




            return RedirectToPage("./MainPage");
        }
    }
}
