using Microsoft.EntityFrameworkCore;
using Job_Portal.Models;

namespace Job_Portal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }

        public DbSet<Comment> Comments { get; set; }

    }
}
