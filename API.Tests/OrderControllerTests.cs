using Api.Models;
using API.Controllers;
using Castle.Core.Logging;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Repository;
using System;
using System.Collections.Generic;

namespace API.Tests
{
    public class OrderControllerTests
    {
        Mock<IOrderRepository> _mockRepo;
        Mock<ILogger<OrderController>> _mockLogger;


        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IOrderRepository>();
            _mockLogger = new Mock<ILogger<OrderController>>();
        }

        [Test]
        public void GivenOrdersExist_WhenGetOrdersCalled_OrdersAreReturned()
        {
            var orders = GetOrders();
            _mockRepo.Setup(mr => mr.GetOrders()).Returns(orders);

            OrderController orderController = new OrderController(_mockRepo.Object, _mockLogger.Object);

            var result = orderController.GetOrders();

            Assert.IsInstanceOf<OkObjectResult>(result.Result);

            var summaryList = ((result.Result as OkObjectResult).Value as List<OrderSummary>);

            Assert.AreEqual(summaryList.Count, orders.Count);
            Assert.AreEqual(summaryList[0].OrderRef, orders[0].Ref);

            _mockRepo.Verify(mr => mr.GetOrders(), Times.Once);
        }

        [Test]
        public void GivenServiceThrows_WhenGetOrdersCalled_ExceptionReThrown()
        {
            var orders = GetOrders();
            var ex = new Exception();
            _mockRepo.Setup(mr => mr.GetOrders()).Throws(ex);

            OrderController orderController = new OrderController(_mockRepo.Object, _mockLogger.Object);

            var thrown = Assert.Throws<Exception>(() =>
            {
                var result = orderController.GetOrders();
            });

            Assert.AreEqual(ex, thrown);
            _mockRepo.Verify(mr => mr.GetOrders(), Times.Once);
        }

        [Test]
        public void GivenOrdersExist_WhenGetOrderItemsCalled_OrderItemsAreReturned()
        {
            var orders = GetOrders();
            var order = orders[0];
            _mockRepo.Setup(mr => mr.GetOrderItems(order.Ref)).Returns((List<OrderItem>)order.Items);

            OrderController orderController = new OrderController(_mockRepo.Object, _mockLogger.Object);

            var result = orderController.GetOrderItems(order.Ref);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);

            var summaryList = ((result.Result as OkObjectResult).Value as List<OrderItemDetail>);
            Assert.AreEqual(summaryList.Count, order.Items.Count);
            Assert.AreEqual(summaryList[0].ProductCode, order.Items[0].Code);

            _mockRepo.Verify(mr => mr.GetOrderItems(order.Ref), Times.Once);
        }

        [Test]
        public void GivenInvalidRefPassed_WhenGetOrderItemsCalled_BadRequestReturned()
        {

            OrderController orderController = new OrderController(_mockRepo.Object, _mockLogger.Object);

            var result = orderController.GetOrderItems(null);

            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);

            _mockRepo.Verify(mr => mr.GetOrderItems(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void GivenServiceThrows_WhenGetOrderItemsCalled_ExceptionReThrown()
        {
            var orders = GetOrders();
            var order = orders[0];
            var ex = new Exception();
            _mockRepo.Setup(mr => mr.GetOrderItems(order.Ref)).Throws(ex);

            OrderController orderController = new OrderController(_mockRepo.Object, _mockLogger.Object);

            var thrown = Assert.Throws<Exception>(() =>
            {
                var result = orderController.GetOrderItems(orders[0].Ref);
            });

            Assert.AreEqual(ex, thrown);

            _mockRepo.Verify(mr => mr.GetOrderItems(order.Ref), Times.Once);
        }

        private List<Order> GetOrders() => new List<Order>
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
}