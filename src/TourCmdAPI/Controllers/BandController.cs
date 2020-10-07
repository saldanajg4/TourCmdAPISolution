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
    public class BandController : ControllerBase
    {
        private readonly ITourRepository repo;
        private readonly IMapper mapper;
        public BandController(ITourRepository repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetBands(){
            var bandsFromRepo = await this.repo.GetBands();
            var bands = mapper.Map<IEnumerable<Band>>(bandsFromRepo);

            if(bands == null)
                return NotFound();
            return Ok(bands);
        }
    }
}