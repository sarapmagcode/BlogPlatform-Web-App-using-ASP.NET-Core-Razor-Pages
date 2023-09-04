using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BlogPlatformWebApp.Models;

namespace BlogPlatformWebApp.Pages.Posts
{
    public class CreateModel : PageModel
    {
        private readonly Data.BlogPlatformWebAppContext _context;

        private string? currentUser;

        [BindProperty]
        public Post Post { get; set; } = default!;

        /* Constructor */

        public CreateModel(Data.BlogPlatformWebAppContext context)
        {
            _context = context;
        }

        /* Handler Methods */

        public IActionResult OnGet()
        {
            currentUser = HttpContext.Session.GetString("username")!;
            if (currentUser == null)
            {
                return RedirectToPage("/Login/Index");
            }

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // Get username of logged-in user
            currentUser = HttpContext.Session.GetString("username")!;

            Post.Username = currentUser;
            Post.DateCreated = DateTime.Now;
            Post.DateEdited = DateTime.Now;
            Post.ReadTimeInMinutes = CalculateReadTimeInMins(Post.Content!);

            if (!ModelState.IsValid || _context.Post == null || Post == null)
            {
                return Page();
            }

            _context.Post.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private int CalculateReadTimeInMins(string content)
        {
            char[] delimiters = new char[] { ' ', '\r', '\n' };
            int wordCount = content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
            return wordCount / 200;
        }
    }
}
