using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class OrderSummary
    {
        public OrderSummary() { }
        public OrderSummary(Order order)
        {
            OrderRef = order.Ref;
            TotalValue = order?.Items?.Sum(i => i.Cost);
        }
        public string OrderRef { get; set; }
        public decimal? TotalValue { get; set; }
    }
}
