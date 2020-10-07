using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourCmdAPI.Controllers;
using TourCmdAPI.Entities;
using TourCmdAPI.IRepos;
using TourCmdAPI.Repos;
using TourCmdAPI.Services;
using Xunit;

namespace TourCmdAPI.Tests
{
    public class OrderCmdAPITests
    {
        public DbContextOptionsBuilder<OrderContext> orderDbBuilder { get; set; }
        public OrderContext orderContext { get; set; }
        public IOrderRepository orderRepo { get; set; }
        public OrderController orderController { get; set; }
        public OrderCmdAPITests()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            orderDbBuilder = new DbContextOptionsBuilder<OrderContext>();
            orderDbBuilder.UseInMemoryDatabase("OrderApiDb");
            orderContext = new OrderContext(orderDbBuilder.Options);
            orderRepo = new OrderRepository(orderContext);
            orderController = new OrderController(orderRepo,mapper);
            orderContext.EnsureSeedDataForOrderContext(); 
            Console.WriteLine("calling constructor");
        }
        
        [Fact]
        public void GetOrderCountShouldNotReturnNull(){
            //arrange
            //act
            var orders = orderController.getOrders();
            //assert
            Assert.NotNull(orders.Result);
        }

        [Fact]
        public void GetOrderCountShouldReturnOkResults(){
            var orders = orderController.getOrders();
            // var okOrdersResult = orders.Result as OkObjectResult;
            // Console.WriteLine(((List<Order>)okOrdersResult.Value).Count);
             Assert.IsType<OkObjectResult>(orders.Result);
        }

        [Fact]
        public void TestGetOrderByIdShouldReturnOK(){
            var order = orderController.getOrderById(1);
            Assert.IsType<OkObjectResult>(order.Result);
        }
        [Fact]
        public void TestGetOrderByIdShouldNotReturnOK(){
            var order = orderController.getOrderById(1000);
            Assert.IsType<NotFoundResult>(order.Result);
        }
    }
}