namespace Domain
{
    using System;
    using Newtonsoft.Json;

    [JsonObject]
    [Serializable]
    public class OrderItem : Product
    {
        public int Quantity { get; set; }
    }
}
