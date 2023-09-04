using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BlogPlatformWebApp.Models;

namespace BlogPlatformWebApp.Pages.Signup
{
    public class CreateModel : PageModel
    {
        private readonly Data.BlogPlatformWebAppContext _context;

        [BindProperty]
        public User User { get; set; } = default!;

        [BindProperty]
        public string? ConfirmPassword { get; set; }

        public string? Message { get; set; }

        /* Constructor */

        public CreateModel(Data.BlogPlatformWebAppContext context)
        {
            _context = context;
        }

        /* Handler Methods */

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Get date today => DateTime.Now.ToString("d/M/yyyy")
            User.AccountCreated = DateTime.Now;

            if (!ModelState.IsValid || _context.User == null || User == null)
            {
                return Page();
            }

            if (ConfirmPassword == null)
            {
                Message = "The Confirm Password field is required.";
                return Page();
            }

            if (!User.Password!.Equals(ConfirmPassword))
            {
                Message = "Passwords do not match.";
                return Page();
            }

            _context.User.Add(User);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("username", User.Username!);

            return RedirectToPage("/Posts/Index");
        }
    }
}
