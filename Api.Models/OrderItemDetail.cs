using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Models
{
    public class OrderItemDetail
    {
        public OrderItemDetail() { }
        public OrderItemDetail(OrderItem orderItem)
        {
            ProductCode = orderItem.Code;
            Description = orderItem.Description;
            Cost = orderItem.Cost;
            Quantity = orderItem.Quantity;
            LineCost = orderItem.Cost/orderItem.Quantity;
        }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public decimal LineCost { get; set; }
    }
}
