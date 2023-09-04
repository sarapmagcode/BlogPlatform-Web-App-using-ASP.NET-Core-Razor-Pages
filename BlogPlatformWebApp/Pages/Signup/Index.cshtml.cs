using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlogPlatformWebApp.Models;

namespace BlogPlatformWebApp.Pages.Signup
{
    public class IndexModel : PageModel
    {
        private readonly BlogPlatformWebApp.Data.BlogPlatformWebAppContext _context;

        public IndexModel(BlogPlatformWebApp.Data.BlogPlatformWebAppContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            //For debugging using console:
            //Console.WriteLine("Hello, world!");

            if (_context.User != null)
            {
                User = await _context.User.ToListAsync();
            }
        }
    }
}
