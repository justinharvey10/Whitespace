using Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Services
{
    public interface IOrderApiClient
    {
        Task<List<OrderSummary>> Order();
        Task<List<OrderItemDetail>> OrderDetails(string orderRef);
    }
}