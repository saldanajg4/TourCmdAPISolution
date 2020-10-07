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
        // [HttpGet("{orderId}")]
        // public async Task<IActionResult> getOrderById(int orderId){
        //     Entities.Order orderFromRepo = (Entities.Order)await this.repo.GetOrderById(orderId);
        //     if(orderFromRepo == null)
        //         return BadRequest();

        //     return Ok(this.mapper.Map<Entities.Order>(orderFromRepo));
        // }

        [HttpGet("{orderId}")]
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

    }
}