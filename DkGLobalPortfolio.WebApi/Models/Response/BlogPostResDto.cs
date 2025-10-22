namespace DkGLobalPortfolio.WebApi.Models.Response
{
    public class BlogPostResDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Content { get; set; }
        public string? Excerpt { get; set; }
        public string? FeaturedImage { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorAvatar { get; set; }
        public string? AuthorBio { get; set; }
        public string? CategoryName { get; set; }
        public List<string>? Tags { get; set; }
        public string? Status { get; set; }
        public bool IsActive { get; set; }
        public int ReadingTime { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
