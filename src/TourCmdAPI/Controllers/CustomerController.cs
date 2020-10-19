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
    public class CustomerController : ControllerBase
    {
        private readonly IOrderRepository orderRepo;
        private readonly IMapper mapper;
        public CustomerController(IOrderRepository orderRepo, IMapper mapper)
        {
            this.mapper = mapper;
            this.orderRepo = orderRepo;
        }

        [HttpGet]
        [RequestHeaderMatchesMediaType("Accept",
            new [] {"application/vnd.jose.allcustomers+json"})]
        public async Task<IActionResult> GetAllCustomers(){
            var customerEntities = await this.orderRepo.GetAllCustomers(); 
            if(customerEntities == null)
                return NotFound();
            var customerDtos = mapper.Map<IEnumerable<Customer>>(customerEntities);
            return Ok(customerDtos);
        }

        [HttpGet("{customerId}", Name = "GetCustomerById")]
        [RequestHeaderMatchesMediaType("Accept",
            new[] {"application/json","application/vnd.jose.customer+json"})]
        public async Task<IActionResult> GetCustomerById(int customerId){
            return await GetSpecificCustomer<Customer>(customerId);
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type",
            new[] {"application/json", "application/vnd.jose.customerforcreation+json"})]
        public async Task<IActionResult> AddCustomer(CustomerForCreation customer){
            return await AddSpecificCustomer<CustomerForCreation>(customer);
        }

        private async Task<IActionResult> GetSpecificCustomer<T>(int customerId) where T : class{
            var customerEntity = await this.orderRepo.GetCustomerById(customerId);
            if(customerEntity == null)
                return NotFound();
            var customerDto = mapper.Map<T>(customerEntity);
            return Ok(customerDto);
        }

        private async Task<IActionResult> AddSpecificCustomer<T>(T customer) where T : class{
            var customerEntity = mapper.Map<Entities.Customer>(customer);
            if(customerEntity == null)
                return BadRequest();
            await this.orderRepo.AddCustomer(customerEntity);
            if(! await this.orderRepo.SaveAsync())
                throw new Exception("Cannot save customer into database");
            return CreatedAtAction("GetCustomerById",
                        new {CustomerId = customerEntity.CustomerId}, customerEntity);
        }

    }
}