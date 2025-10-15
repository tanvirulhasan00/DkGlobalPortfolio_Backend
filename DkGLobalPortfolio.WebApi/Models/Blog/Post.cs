using Azure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DkGLobalPortfolio.WebApi.Models.Blog
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Slug { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public string? Excerpt { get; set; }

        public string? FeaturedImage { get; set; }

        [Required]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Required]
        public string Status { get; set; } = "draft"; // draft, published, archived
        public bool IsActive { get; set; } = true; 

        public int ReadingTime { get; set; } = 5; // in minutes

        public DateTime? PublishedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Soft delete fields
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }

        // Navigation properties
        public virtual Author Author { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public ICollection<BlogPostTag> BlogPostTags { get; set; } = new List<BlogPostTag>();


    }
}
