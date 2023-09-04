using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogPlatformWebApp.Models;

namespace BlogPlatformWebApp.Data
{
    public class BlogPlatformWebAppContext : DbContext
    {
        public BlogPlatformWebAppContext (DbContextOptions<BlogPlatformWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<BlogPlatformWebApp.Models.Post> Post { get; set; } = default!;

        public DbSet<BlogPlatformWebApp.Models.User> User { get; set; } = default!;
    }
}
