using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DkGLobalPortfolio.WebApi.Models.Blog.Dto
{
    public class UpdateBlogCategoryDto
    {   
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

    }
}
