using Azure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DkGLobalPortfolio.WebApi.Models.Blog
{
    public class UpdatePostDto
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string? Title { get; set; }

        [MaxLength(255)]
        public string? Slug { get; set; }
        public string? Content { get; set; }
        public string? Excerpt { get; set; }
        public IFormFile? FeaturedImage { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }

        public string? Status { get; set; } 

        public int ReadingTime { get; set; } 

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public List<int>? TagIds { get; set; }

    }
}
