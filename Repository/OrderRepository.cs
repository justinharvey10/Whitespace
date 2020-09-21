namespace Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain;

    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders;

        public OrderRepository()
        {

            _orders = new List<Order>
            {
                new Order
                {
                    Ref = "ABC123",
                    Items = new List<OrderItem>
                    {
                        new OrderItem {Code = "HAT", Description = "Wool Hat with bobble on top", Cost = (decimal) 5.00, Quantity = 1},
                        new OrderItem {Code = "JUMPER", Description = "Knitted style jumper with large buttons", Cost = (decimal) 12.65, Quantity = 2},
                        new OrderItem {Code = "TROUSERS", Description = "Long Slim Fit Denim Jeans", Cost = (decimal) 35.25, Quantity = 3}
                    }
                },
                new Order
                {
                    Ref = "DEF456",
                    Items = new List<OrderItem>
                    {
                        new OrderItem {Code = "HAT", Description = "Wool Hat with bobble on top", Cost = (decimal) 5.00, Quantity = 1},
                        new OrderItem {Code = "JUMPER", Description = "Knitted style jumper with large buttons", Cost = (decimal) 12.65, Quantity = 2}
                    }
                },
                new Order
                {
                    Ref = "GHI456"
                },
            };
        }


        public List<Order> GetOrders()
        {
            return _orders;
        }

        public List<OrderItem> GetOrderItems(string orderRef)
        {
            return (from o in _orders where o.Ref == orderRef select o.Items as List<OrderItem>).FirstOrDefault();
        }
    }
}
