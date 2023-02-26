using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Repository;
using Library.Repository.RepositoryImpl;

namespace WebApplication.Pages.AdminPages
{
    public class EditModel : PageModel
    {
        private IUserRepository userRepository;

        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGet(int id)
        {

            userRepository = new UserRepository();

            if (id == null)
            {
                return NotFound();
            }

            User = userRepository.GetUserByID(id);

            if (User == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(userRepository.GetUserList(), "RoleId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            userRepository = new UserRepository();

            if (!ModelState.IsValid)
            {
                return Page();
            }
           
            userRepository.UpdateUser(User);
             
            return RedirectToPage("./MainPage");
        }

        /*private bool UserExists(int id)
        {
            return userRepository.Users.Any(e => e.UserId == id);
        }*/
    }
}
