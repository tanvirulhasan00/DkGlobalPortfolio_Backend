using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DkGLobalPortfolio.WebApi.Database
{
    public class DkGlobalPortfolioDbContext : IdentityDbContext<ApplicationUser>
    {
        public DkGlobalPortfolioDbContext(DbContextOptions<DkGlobalPortfolioDbContext> options): base(options)
        {
            
        }

        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Author> Author => Set<Author>();
        public DbSet<BlogPostTag> BlogPostTags => Set<BlogPostTag>();



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Configure many-to-many relationship
            builder.Entity<BlogPostTag>()
                .HasKey(bt=>bt.Id);

            builder.Entity<BlogPostTag>()
                .HasOne(bt => bt.Post)
                .WithMany(b => b.BlogPostTags)
                .HasForeignKey(bt => bt.PostId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<BlogPostTag>()
                .HasOne(bt => bt.Tag)
                .WithMany(t => t.BlogPostTags)
                .HasForeignKey(bt => bt.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BlogPostTag>()
                .HasIndex(bt => new { bt.PostId, bt.TagId })
                .IsUnique(); // prevent duplicates

            builder.Entity<Post>()
                .HasIndex(b => new { b.Status, b.PublishedAt });
            builder.Entity<Post>()
                .HasIndex(p => p.Slug)
                .IsUnique();

            builder.Entity<Category>()
                .HasIndex(c => c.Slug)
                .IsUnique();

            builder.Entity<Tag>()
                .HasIndex(t => t.Slug)
                .IsUnique();

            builder.Entity<Author>()
                .HasIndex(a => a.Username)
                .IsUnique();

            builder.Entity<Author>()
                .HasIndex(a => a.Email)
                .IsUnique();


        }
    }
}
