namespace Domain
{
    using System;
    using Newtonsoft.Json;

    [JsonObject]
    [Serializable]
    public class Product
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}
