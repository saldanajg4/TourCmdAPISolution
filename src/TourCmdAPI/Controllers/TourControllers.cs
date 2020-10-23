using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
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
        
             var tours = _mapper.Map<IEnumerable<Dtos.Tour>>(toursFromRepo);
            return Ok(tours);
        }

        //Return diff representations of same resource.  Tour and TourWithEstimatedProfits

        // [HttpGet("{id}", Name = "GetTour")]
        // public async Task<IActionResult> GetTourById(Guid id)
        // {
        //     var tourResult = await tourRepository.GetTourById(id);
        //     var tour = _mapper.Map<IEnumerable<Entities.Tour>>(tourResult);
        //     if(tour == null){
        //         return NotFound();
        //     }
        //     return Ok(tour);
        // }

        //this method takes default "application/json" requests
        [HttpGet("{tourId}")]
        public async Task<IActionResult> GetDefaultTourById(Guid tourId){
            if (Request.Headers.TryGetValue("Accept", out StringValues values))
            {
                Debug.WriteLine($"Accept header(s): {string.Join(",",values)}");
            }
            return await GetSpecificTour<Tour>(tourId);
        }

        [HttpGet("{tourId}", Name = "GetTourById") ]
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

        [HttpGet("{tourId}")]
        [RequestHeaderMatchesMediaType("Accept",
            new[] {"application/vnd.jose.tourwithshows+json"})]
        public async Task<IActionResult> GetTourWithShows(Guid tourId) {
            return await GetSpecificTour<TourWithShows>(tourId, true);
        }

        [HttpGet("{tourId}")]
        [RequestHeaderMatchesMediaType("Accept",
            new[] {"application/vnd.jose.tourwithestimatedprofitsandshows+json"})]
        public async Task<IActionResult> GetTourWithEstimatedProfitsAndShows(Guid tourId) {
            return await GetSpecificTour<TourWithEstimatedProfitsAndShows>(tourId, true);
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type",
            new[] {"application/json",
                   "application/vnd.jose.tourforcreation+json"})]
        public async Task<IActionResult> AddTour([FromBody] TourForCreation tour){
            if(tour == null)
                return BadRequest();
            //validation of DTO happnes here
            return await AddSpecificTour<TourForCreation>(tour);
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type",
            new[] {"application/vnd.jose.tourwithmanagerforcreation+json"})]
        public async Task<IActionResult> AddTourWithManager([FromBody] 
                                        TourWithManagerForCreation tour){
            Console.WriteLine("creating the tour");
            if(tour == null )
                return BadRequest();
            //validation of DTO happens here
            return await AddSpecificTour<TourWithManagerForCreation>(tour);
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type",
            new[] {"application/vnd.jose.tourwithshowsforcreation+json"})]
        public async Task<IActionResult> AddTourWithShows(
            [FromBody] TourWithShowsForCreation tour)
        {
            if(tour == null)
                return BadRequest();
            //validation of DTO happens here

            //the rest is the same as for other actions
            return await AddSpecificTour(tour);
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type",
            new[] {"application/vnd.jose.tourwithmanagerandshowsforcreation+json"})]
        public async Task<IActionResult> AddTourWithManagerAndShows(
            [FromBody] TourWithManagerAndShowsForCreation tour)
        {
            if(tour == null)
                return BadRequest();
            //validation of DTO happens here

            //the rest is the same as for other actions
            return await AddSpecificTour(tour);
        }
        

        public async Task<IActionResult> AddSpecificTour<T>(T tour) where T : class{
            var tourEntity = _mapper.Map<Entities.Tour>(tour);
            if(tourEntity.ManagerId == Guid.Empty){
                tourEntity.ManagerId = new Guid("fec0a4d6-5830-4eb8-8024-272bd5d6d2bb");
            }
            await tourRepository.AddTour(tourEntity);
            if(!await tourRepository.SaveAsync()){
                throw new Exception("Adding a tour failed on save.");
            }
            //now map the entity tour into dto object for the client
            var tourToReturn = _mapper.Map<Tour>(tourEntity);

            //mathcing the uri for /api/tour/tourId
            return CreatedAtRoute("GetTourById", 
                new {tourId = tourToReturn.TourId}, tourToReturn);
        }

        private async Task<IActionResult> GetSpecificTour<T>(Guid tourId,
            bool includeShows = false) where T : class{
            var tourFromRepo = await tourRepository.GetTourById(tourId, includeShows);
            // var tour = _mapper.Map<IEnumerable<T>>(tourFromRepo);
            var tour = _mapper.Map<T>(tourFromRepo);
            // if(tour == null || ((List<T>)tour).Count == 0){
                if(tour == null){
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