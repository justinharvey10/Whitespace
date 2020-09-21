using Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Web.Services
{
    public class OrderApiClient : IOrderApiClient
    {
        static HttpClient _client = new HttpClient()
        {
            BaseAddress = new Uri(ConfigurationManager.AppSettings["APIUrl"]),
            Timeout = new TimeSpan(0, 0, 30)
        };

        public OrderApiClient()
        {
            _client.DefaultRequestHeaders.Clear();
        }

        public async Task<List<OrderSummary>> Order()
        {
            using (var response = await _client.GetAsync($"/api/Order/GetOrders"))
            {
                response.EnsureSuccessStatusCode();

                var orders = await response.Content.ReadAsStringAsync();

                var orderList = JsonConvert.DeserializeObject<List<OrderSummary>>(orders);

                return orderList;
            }
        }

        public async Task<List<OrderItemDetail>> OrderDetails(string orderRef)
        {
            using (var response = await _client.GetAsync($"api/Order/GetOrderItems/{orderRef}"))
            {
                response.EnsureSuccessStatusCode();

                var orders = await response.Content.ReadAsStringAsync();
                var orderItems = JsonConvert.DeserializeObject<List<OrderItemDetail>>(orders);

                return orderItems;
            }
        }
    }
}