namespace DkGLobalPortfolio.WebApi.Models.Blog.Dto
{
    public class DeletePostDto
    {
        public List<int> Ids { get; set; } = new List<int>();

        // Who deleted it (optional, if tracking users)
        public int? DeletedBy { get; set; }
    }
}
