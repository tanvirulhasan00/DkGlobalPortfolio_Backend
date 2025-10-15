using Azure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DkGLobalPortfolio.WebApi.Models.Blog
{
    public class CreatePostDto
    {
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(255)]
        public string Slug { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Excerpt { get; set; }
        public IFormFile? FeaturedImage { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string Status { get; set; } = "draft";
        public bool IsActive { get; set; } = true; 
        public int ReadingTime { get; set; } = 5; // in minutes
        public DateTime? PublishedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // 👇 Add this
        public List<int>? TagIds { get; set; }

    }
}
