using System.ComponentModel.DataAnnotations;

namespace DkGLobalPortfolio.WebApi.Models.Blog.Dto
{
    public class CreateBlogCategoryDto
    {
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Slug { get; set; } = string.Empty;

        public string? Description { get; set; }


    }
}
