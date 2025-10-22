using System.ComponentModel.DataAnnotations;

namespace DkGLobalPortfolio.WebApi.Models.Blog.Dto
{
    public class UpdateTagDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Slug { get; set; } = string.Empty;
    }
}
