using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TourCmdAPI.IRepos;

namespace TourCmdAPI.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase{
        private readonly ITourRepository tourRepository;
        private readonly IMapper _mapper;
        public TourController(ITourRepository repo, IMapper mapper)
        {
            tourRepository = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> getAllTours(){
            IEnumerable<Entities.Tour> toursFromRepo = await tourRepository.GetTours();
        
             var tours = _mapper.Map<IEnumerable<Entities.Tour>>(toursFromRepo);
            return Ok(tours);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTourById(Guid id)
        {
            IEnumerable<Entities.Tour> tourResult = await tourRepository.GetTourById(id);
            var tour = _mapper.Map<IEnumerable<Entities.Tour>>(tourResult);
            if(tour == null || ((List<Entities.Tour>)tourResult).Count == 0){
                return NotFound();
            }
            return Ok(tour);
        }
        // [HttpGet]
        // public ActionResult<IEnumerable<string>> Get(){
        //     return new string[] {"this","is", "hard", "codadfed"};
        // }
    }
}