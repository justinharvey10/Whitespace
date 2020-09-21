namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [JsonObject]
    [Serializable]
    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public string Ref { get; set; }
        public IList<OrderItem> Items { get; set; }
    }
}
