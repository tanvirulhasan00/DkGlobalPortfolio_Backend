using DkGLobalPortfolio.WebApi.Models.Leader;
using DkGLobalPortfolio.WebApi.Models.Message;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IMessageService : IService<Message>
    {
        void Update(Message message);
    }
}
