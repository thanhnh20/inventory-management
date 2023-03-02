using Library.Model;
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

        public void OnGet()
        {
            userRepository = new UserRepository();
            User = userRepository.GetUserList().ToList();
        }

        public IActionResult OnGetLogOut()
        {
            HttpContext.Session.Remove("ADMIN");
            return Redirect("~/HomePages/Home");
        }

    }
}
