using BlogPlatformWebApp.Data;
using BlogPlatformWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BlogPlatformWebApp.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly BlogPlatformWebAppContext _context;

        public IndexModel(BlogPlatformWebAppContext context)
        {
            _context = context;
        }

        public User User { get; set; } = default!;

        public IList<Post> Post { get; set; } = default!;

        public IEnumerable<Post> sortedPostList;

        public string? LoggedInUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string? username)
        {
            LoggedInUser = HttpContext.Session.GetString("username");
            if (username.IsNullOrEmpty() && LoggedInUser.IsNullOrEmpty())
            {
                return RedirectToPage("/Login/Index");
            }
            
            if (!username.IsNullOrEmpty() && username!.Equals(LoggedInUser))
            {
                username = "";
                return RedirectToPage("/Profile/Index");
            }

            if (username.IsNullOrEmpty())
            {
                username = LoggedInUser;
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.Username == username);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }

            if (_context.Post != null)
            {
                var postList = from p in _context.Post
                             select p;

                postList = postList.Where(post => post.Username == username);

                Post = await postList.ToListAsync();

                sortedPostList = Post.OrderBy(p => p.DateCreated).Reverse();
                Post = sortedPostList.ToList();

                foreach (var post in Post)
                {
                    if (post.Content!.Length >= 400)
                    {
                        post.Content = post.Content![..400] + "...";
                    }
                }
            }

            return Page();
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
