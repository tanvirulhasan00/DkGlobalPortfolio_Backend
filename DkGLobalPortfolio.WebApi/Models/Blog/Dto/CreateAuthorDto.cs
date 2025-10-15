using System.ComponentModel.DataAnnotations;

namespace DkGLobalPortfolio.WebApi.Models.Blog.Dto
{
    public class CreateAuthorDto
    {
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public IFormFile? Avatar { get; set; }

    }
}
