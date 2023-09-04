using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlogPlatformWebApp.Data;
using BlogPlatformWebApp.Models;

namespace BlogPlatformWebApp.Pages.Posts
{
    public class DetailsModel : PageModel
    {
        private readonly BlogPlatformWebAppContext _context;

        public DetailsModel(BlogPlatformWebAppContext context)
        {
            _context = context;
        }

        public Post Post { get; set; } = default!;

        public string? Username { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            else 
            {
                Post = post;
            }

            Username = HttpContext.Session.GetString("username");

            return Page();
        }
    }
}
