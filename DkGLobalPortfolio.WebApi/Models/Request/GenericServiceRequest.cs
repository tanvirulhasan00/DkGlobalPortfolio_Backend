using DkGLobalPortfolio.WebApi.Utilities;
using System.Linq.Expressions;

namespace DkGLobalPortfolio.WebApi.Models.Request
{
    public class GenericServiceRequest<T>
    {
        public Expression<Func<T, bool>>? Expression { get; set; } = null;
        public string? IncludeProperties { get; set; } = null;
        public OrderTypeClass.OrderType? OrderType { get; set; } = null;
        public Expression<Func<T, object>>? OrderExpression { get; set; } = null;
        public bool NoTracking { get; set; } = false;
        public CancellationToken CancellationToken { get; set; } = CancellationToken.None;
    }
}
