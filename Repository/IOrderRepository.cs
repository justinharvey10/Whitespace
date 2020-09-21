using System.Collections.Generic;
using Domain;

namespace Repository
{
    public interface IOrderRepository
    {
        List<Order> GetOrders();
        List<OrderItem> GetOrderItems(string orderRef);
    }
}
