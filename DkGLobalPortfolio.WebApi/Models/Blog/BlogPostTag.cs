namespace DkGLobalPortfolio.WebApi.Models.Blog
{
    public class BlogPostTag
    {
        public int Id { get; set; } // Surrogate PK

        public int PostId { get; set; }
        public int TagId { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        public int? AddedBy { get; set; }

        public Post Post { get; set; } = null!;
        public Tag Tag { get; set; } = null!;
    }
}
