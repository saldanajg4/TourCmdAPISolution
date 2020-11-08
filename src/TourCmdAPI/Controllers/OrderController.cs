using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TourCmdAPI.Dtos;
using TourCmdAPI.Helpers;
using TourCmdAPI.IRepos;

namespace TourCmdAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository repo;
        private readonly IMapper mapper;
        public OrderController(IOrderRepository repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> getOrders(){
            IEnumerable<Entities.Order> ordersFromRepo = (IEnumerable<Entities.Order>)await this.repo.GetOrders();
            var orders = this.mapper.Map<IEnumerable<Entities.Order>>(ordersFromRepo);
            return Ok(orders);
        }

        [HttpGet("{orderId}", Name = "getOrderById")]
        [RequestHeaderMatchesMediaType("Accept",
            new[] {"application/vnd.jose.order+json"})]
        public async Task<IActionResult> getOrderById(int orderId){
            return await getSpecificOrderById<Order>(orderId, false);
        }

        [HttpGet("{orderId}")]
        [RequestHeaderMatchesMediaType("Accept",
            new[] {"Application/vnd.jose.orderwithitems+json"})]
        public async Task<IActionResult> getOrderWithItemsById(int orderId){
            return await getSpecificOrderById<OrderWithItems>(orderId, true);
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type",
            new[] {"application/vnd.jose.orderforcreation+json"})]
        public async Task<IActionResult> AddOrder([FromBody] OrderForCreation order){
            if(order == null)
                return BadRequest();
            var orderEntity = mapper.Map<Entities.Order>(order);
            await repo.AddOrder(orderEntity);
           if (! await repo.SaveAsync())
            throw new Exception("Adding order failed.");
           var orderDto = mapper.Map<Order>(orderEntity); 
           
           return CreatedAtRoute("getOrderById",
                new { orderId = orderDto.OrderId }, orderDto);
        }


        private async Task<IActionResult> getSpecificOrderById<T>(int orderId, bool includeItems)
            where T : class{
                var orderFromRepo = await this.repo.GetOrderById(orderId, includeItems);
                var order = mapper.Map<T>(orderFromRepo);
                if(order == null){
                    return NotFound();
                }
                else{
                    return Ok(order);
                }
        }

         [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type",
            new[] {"application/json",
            "application/vnd.jose.orderitemcollectionforcreation+json"})]
         public async Task<IActionResult> CreateOrderItemCollection(
                [FromBody] IEnumerable<OrderItemForCreation> orderItemCollection)
        {
            if (orderItemCollection == null)
                return BadRequest();
            
            var orderItemEntities = mapper.Map<IEnumerable<Entities.OrderItem>>(orderItemCollection);
            foreach(var item in orderItemEntities){
                await repo.AddOrderItem(item);
            }
            if(!await repo.SaveAsync()){
                throw new Exception("Adding a collection of items failed on save.");
            }
            return Ok();
        }
    }
}