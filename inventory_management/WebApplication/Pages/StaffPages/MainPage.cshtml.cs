using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication.Pages.StaffPages
{
    public class MainPageModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToPage("../Products/Statistic");
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("STAFF");
            return Redirect("~/HomePages/Home");
        }

        public IActionResult OnGetLogin()
        {
            return Redirect("~/HomePages/Home");
        }
    }
}
