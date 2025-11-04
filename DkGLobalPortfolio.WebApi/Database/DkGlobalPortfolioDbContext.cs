using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.ClientTestimonial;
using DkGLobalPortfolio.WebApi.Models.Leader;
using DkGLobalPortfolio.WebApi.Models.Message;
using DkGLobalPortfolio.WebApi.Models.Newsletter;
using DkGLobalPortfolio.WebApi.Models.Partner;
using DkGLobalPortfolio.WebApi.Models.Product;
using DkGLobalPortfolio.WebApi.Models.Profile;
using DkGLobalPortfolio.WebApi.Models.Report;
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
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<BlogPostTag> BlogPostTags => Set<BlogPostTag>();
        public DbSet<LeaderShip> LeaderShips => Set<LeaderShip>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Newsletter> Newsletters => Set<Newsletter>();
        public DbSet<Partner> Partners => Set<Partner>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductCategory> PartnerCategories => Set<ProductCategory>();
        public DbSet<ProductImage> ProductImages => Set<ProductImage>();
        public DbSet<CompanyInfo> CompanyInfos => Set<CompanyInfo>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<BankInfo> BankInfos => Set<BankInfo>();
        public DbSet<ProfileImage> ProfileImages => Set<ProfileImage>();
        public DbSet<Report> Reports => Set<Report>();
        public DbSet<ReportCategory> ReportCategories => Set<ReportCategory>();
        public DbSet<ClientTestimonial> ClientTestimonials => Set<ClientTestimonial>();



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
                .HasIndex(a => a.FirstName)
                .IsUnique();

            builder.Entity<Author>()
                .HasIndex(a => a.Email)
                .IsUnique();


        }
    }
}
