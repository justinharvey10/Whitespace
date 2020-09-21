using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderRepository _orderRepository;
        ILogger<OrderController> _logger;

        public OrderController(IOrderRepository orderRepository, ILogger<OrderController> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        [HttpGet("GetOrders")]
        public ActionResult<List<OrderSummary>> GetOrders()
        {
            try
            {
                return Ok(_orderRepository.GetOrders().Select(o=>new OrderSummary(o)).ToList());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Exception caught in GetOrders");
                throw;
            }           
        }

        [HttpGet("GetOrderItems/{orderRef}")]
        public ActionResult<List<OrderItemDetail>> GetOrderItems(string orderRef)
        {
            try
            {
                if (string.IsNullOrEmpty(orderRef))
                    return BadRequest("Invalid value passed for orderRef");

                return Ok(_orderRepository.GetOrderItems(orderRef).Select(oi=>new OrderItemDetail(oi)).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception caught in GetOrderItems");
                throw;
            }
        }
    }
}
