using System.Text.RegularExpressions;

namespace DkGLobalPortfolio.WebApi.Utilities
{
    public class Slug
    {
        public static string Generate(string phrase)
        {
            string str = phrase.ToLowerInvariant();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // remove invalid chars
            str = Regex.Replace(str, @"\s+", "-").Trim('-'); // convert spaces to hyphens
            str = Regex.Replace(str, @"-+", "-"); // collapse multiple hyphens
            return str;
        }
    }
}
