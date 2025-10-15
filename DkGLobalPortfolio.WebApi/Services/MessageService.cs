using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Leader;
using DkGLobalPortfolio.WebApi.Models.Message;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class MessageService : Service<Message>, IMessageService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public MessageService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Message message)
        {
            _db.Update(message);
        }
    }
}
