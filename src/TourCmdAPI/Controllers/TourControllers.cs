using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TourCmdAPI.Dtos;
using TourCmdAPI.Helpers;
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

        //Return diff representations of same resource.  Tour and TourWithEstimatedProfits

        // [HttpGet("{id}", Name = "GetTour")]
        // public async Task<IActionResult> GetTourById(Guid id)
        // {
        //     IEnumerable<Entities.Tour> tourResult = await tourRepository.GetTourById(id);
        //     var tour = _mapper.Map<IEnumerable<Entities.Tour>>(tourResult);
        //     if(tour == null || ((List<Entities.Tour>)tourResult).Count == 0){
        //         return NotFound();
        //     }
        //     return Ok(tour);
        // }

        [HttpGet("{tourId}")]
        [RequestHeaderMatchesMediaType("Accept",
            new[] {"application/vnd.jose.tour+json"})]
        public async Task<IActionResult> GetTourById(Guid tourId) {
            return await GetSpecificTour<Tour>(tourId);
        }

          [HttpGet("{tourId}")]
        [RequestHeaderMatchesMediaType("Accept",
            new[] {"application/vnd.jose.tourwithestimatedprofits+json"})]
        public async Task<IActionResult> GetTourWithEstimatedProfits(Guid tourId) {
            return await GetSpecificTour<TourWithEstimatedProfits>(tourId);
        }

        private async Task<IActionResult> GetSpecificTour<T>(Guid tourId) where T : class{
            var tourFromRepo = await tourRepository.GetTourById(tourId);
            var tour = _mapper.Map<IEnumerable<T>>(tourFromRepo);
            if(tour == null || ((List<T>)tour).Count == 0){
                // return BadRequest();
                return NotFound();
            }
            // return Ok(_mapper.Map<T>(tourFromRepo));
            return Ok(tour);
        }

        // [HttpGet]
        // public ActionResult<IEnumerable<string>> Get(){
        //     return new string[] {"this","is", "hard", "codadfed"};
        // }
    }
}