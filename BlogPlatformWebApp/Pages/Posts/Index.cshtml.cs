using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlogPlatformWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlogPlatformWebApp.Pages.Posts
{
    public class IndexModel : PageModel
    {
        private readonly Data.BlogPlatformWebAppContext _context;

        public IndexModel(Data.BlogPlatformWebAppContext context)
        {
            _context = context;
        }

        public string? Username { get; set; }

        public bool IsLoggedIn { get; set; }

        public IList<Post> Post { get;set; } = default!;

        public IEnumerable<Post> sortedPostList;

        // TODO: Filter and search functionalities
        // TODO: Place <div> tag inside <a> tag for posts

        public async Task OnGetAsync()
        {
            Username = HttpContext.Session.GetString("username")!;
            if (Username.IsNullOrEmpty())
            {
                IsLoggedIn = false;
            }
            else
            {
                IsLoggedIn = true;
            }

            if (_context.Post != null)
            {
                Post = await _context.Post.ToListAsync();

                sortedPostList = Post.OrderBy(p => p.DateCreated).Reverse();
                Post = sortedPostList.ToList();

                foreach (var post in Post)
                {
                    if (post.Content!.Length >= 940)
                    {
                        post.Content = post.Content![..940] + "...";
                    }
                }
            }
        }

        public IActionResult OnGetLogout()
        {
            // Clear only a specific key:
            //HttpContext.Session.Remove("username");

            // Clear all keys:
            HttpContext.Session.Clear();

            return RedirectToPage("/Login/Index");
        }
    }
}
