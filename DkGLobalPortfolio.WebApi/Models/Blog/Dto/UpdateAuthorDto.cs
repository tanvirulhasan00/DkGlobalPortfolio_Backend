using System.ComponentModel.DataAnnotations;

namespace DkGLobalPortfolio.WebApi.Models.Blog.Dto
{
    public class UpdateAuthorDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public IFormFile? Avatar { get; set; }

    }
}
