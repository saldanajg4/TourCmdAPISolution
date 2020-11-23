using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TourCmdAPI.Dtos;
using TourCmdAPI.IRepos;

namespace TourCmdAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly ITourRepository repository;
        private readonly IMapper mapper;

        public ManagerController(ITourRepository repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetManagers(){
            var managersFromRepo = await this.repository.GetManagers();
            var managers = mapper.Map<IEnumerable<Manager>>(managersFromRepo);
            if(managers == null)
                return NotFound();
            return Ok(managers)  ;
        }
    }
}