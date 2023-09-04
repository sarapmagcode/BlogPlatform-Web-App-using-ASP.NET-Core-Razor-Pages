using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlogPlatformWebApp.Models;

namespace BlogPlatformWebApp.Pages.Posts
{
    public class EditModel : PageModel
    {
        private readonly Data.BlogPlatformWebAppContext _context;

        public EditModel(Data.BlogPlatformWebAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Post Post { get; set; } = default!;
        
        public static Post? PostCopy { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post =  await _context.Post.FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound();
            }
            Post = post;
            PostCopy = new Post
            {
                Id = Post.Id,
                Username = Post.Username,
                DateCreated = Post.DateCreated,
                DateEdited = Post.DateEdited,
                Topic = Post.Topic,
                ReadTimeInMinutes = Post.ReadTimeInMinutes
            };
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Post.Id = PostCopy!.Id;
            Post.Username = PostCopy.Username;
            Post.DateCreated = PostCopy.DateCreated;
            Post.DateEdited = PostCopy.DateEdited;
            Post.Topic = PostCopy.Topic;
            Post.ReadTimeInMinutes = PostCopy.ReadTimeInMinutes;

            if (Post.Title == null || Post.Content == null)
            {
                return Page();
            }

            Post.DateEdited = DateTime.Now;
            Post.ReadTimeInMinutes = CalculateReadTimeInMins(Post.Content!);

            _context.Attach(Post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(Post.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PostExists(int id)
        {
          return (_context.Post?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private int CalculateReadTimeInMins(string content)
        {
            char[] delimiters = new char[] { ' ', '\r', '\n' };
            int wordCount = content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
            return wordCount / 200;
        }
    }
}
