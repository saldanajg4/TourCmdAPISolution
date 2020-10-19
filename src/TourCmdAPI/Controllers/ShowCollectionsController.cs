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
    [Route("api/tour/{tourId}/showcollection")]
    [ApiController]
    public class ShowCollectionsController : ControllerBase
    {
        private readonly ITourRepository tourRepo;
        private readonly IMapper mapper;
        public ShowCollectionsController(ITourRepository tourRepo, IMapper mapper)
        {
            this.mapper = mapper;
            this.tourRepo = tourRepo;
        }
        //no automapper needed since receiving ShowForCreation enumerable of
        //showcollectionforcreation type
        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-type",
            new[] {"application/json",
            "application/vnd.jose.showcollectionforcreation+json"})]
        public async Task<IActionResult> CreateShowCollection(Guid tourId,
            [FromBody] IEnumerable<ShowForCreation> showCollection)
        {
            if (showCollection == null)
                return BadRequest();
            if (!await tourRepo.TourExists(tourId))
                return NotFound();
            //if Ok request, automap collections into ienumerable show entities, 
            //check if exists in automapper
            var showEntities = mapper.Map<IEnumerable<Entities.Show>>(showCollection);
            foreach(var show in showEntities){
                await tourRepo.AddShow(tourId, show);//adding a show into particular tour using tourId
            }
            if(!await tourRepo.SaveAsync()){
                throw new Exception("Adding a collection of shows failed on save.");
            }

            return Ok();
        }
    }
}