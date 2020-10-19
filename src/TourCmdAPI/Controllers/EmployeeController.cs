using System;
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
    public class EmployeeController : ControllerBase
    {
        private readonly IOrderRepository orderRepo;
        private readonly IMapper mapper;
        public EmployeeController(IOrderRepository orderRepo, IMapper mapper)
        {
            this.mapper = mapper;
            this.orderRepo = orderRepo;
        }

        [HttpGet("{employeeId}", Name = "GetEmployeeById")]
        [RequestHeaderMatchesMediaType("Accept",
            new [] {"application/vnd.jose.employee+json"})]
        public async Task<IActionResult> GetEmployeeById(int employeeId){
            var employeeFromEntity = await this.orderRepo.GetEmployeeById(employeeId);
            if(employeeFromEntity == null)
                return NotFound();
            var dtoEmployee = mapper.Map<Employee>(employeeFromEntity);
            return Ok(dtoEmployee);
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type",
            new[] {"application/json",
                "application/vnd.jose.employeeforcreation+json"})]
        public async Task<IActionResult> AddEmployee(EmployeeForCreation emp){
            return await AddSpecificEmployee<EmployeeForCreation>(emp);
        }

        private async Task<IActionResult> AddSpecificEmployee<T>(T employee) where T : class{
            if(employee == null)
                return BadRequest();
            var empEntity = mapper.Map<Entities.Employee>(employee);
            await this.orderRepo.AddEmployee(empEntity);//here is been added to the ORM of context 
                                        //but not save in db yet unitl SaveAsync() is called
            if(! await this.orderRepo.SaveAsync()){
                throw new Exception("Error inserting employee");
            }

            return CreatedAtAction("GetEmployeeById",
                                    new {EmployeeId = empEntity.EmployeeId}, empEntity);
        }
    }
}