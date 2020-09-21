namespace Web.Controllers
{
    using Api.Models;
    using Domain;
    using Newtonsoft.Json;
    using Serilog;
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Web.Services;

    public class OrderController : Controller
    {
        private readonly IOrderApiClient _orderApiClient;
        private readonly ILogger _logger;

        public OrderController(IOrderApiClient orderApiClient, ILogger logger)
        {
            _orderApiClient = orderApiClient;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult> Order()
        {
            try
            {
                return View(await _orderApiClient.Order());
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Exception caught in Order()");
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> OrderDetails(string orderRef)
        {
            try
            {
                return View(await _orderApiClient.OrderDetails(orderRef));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception caught in OrderDetails(orderRef)");
                throw;
            }
        }
    }
}