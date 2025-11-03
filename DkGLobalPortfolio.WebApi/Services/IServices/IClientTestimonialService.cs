using DkGLobalPortfolio.WebApi.Models.ClientTestimonial;
using DkGLobalPortfolio.WebApi.Models.Leader;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IClientTestimonialService : IService<ClientTestimonial>
    {
        void Update(ClientTestimonial clientTestimonial);
    }
}
