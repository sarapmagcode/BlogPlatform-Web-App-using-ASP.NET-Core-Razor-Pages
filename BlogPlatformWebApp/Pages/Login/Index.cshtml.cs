using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatformWebApp.Pages.Login
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public required string Username { get; set; }

        [BindProperty]
        public required string Password { get; set; }

        public string? Message { get; set; }

        private readonly BlogPlatformWebApp.Data.BlogPlatformWebAppContext _context;

        public IndexModel(BlogPlatformWebApp.Data.BlogPlatformWebAppContext context)
        {
            _context = context;
        }

        /* Handler Methods */

        public IActionResult OnGet()
        {
            string currentUser = HttpContext.Session.GetString("username")!;
            if (currentUser != null)
            {
                return RedirectToPage("/Posts/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (Username == null || Password == null)
            {
                Message = "Please fill in all the fields.";
                return Page();
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.Username == Username);
            if (user == null || !Password.Equals(user.Password))
            {
                Message = "Incorrect username/password. Please try again.";
                return Page();
            }

            HttpContext.Session.SetString("username", Username);
            return RedirectToPage("/Posts/Index");
        }
    }
}
