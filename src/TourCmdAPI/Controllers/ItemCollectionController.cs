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
    
    [Route("api/itemcollection")]
    [ApiController]
    public class ItemCollectionController : ControllerBase
    {
        private readonly IOrderRepository repo;
        private readonly IMapper mapper;
        public ItemCollectionController(IOrderRepository repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;
        }
        //no automapper needed since receiving ItemForCreation enumerable of
        //showcollectionforcreation type    [Route("api/tour/{tourId}/showcollection")]
        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type",
            new[] {"application/json",
            "application/vnd.jose.itemcollectionforcreation+json"})]
         public async Task<IActionResult> CreateItemCollection([FromBody] IEnumerable<ItemForCreation> itemCollection)
        {
            if (itemCollection == null)
                return BadRequest();
            
            //if Ok request, automap collections into ienumerable show entities, 
            //check if exists in automapper
            var itemEntities = mapper.Map<IEnumerable<Entities.Item>>(itemCollection);
            foreach(var item in itemEntities){
                await repo.AddItem(item);
            }
            if(!await repo.SaveAsync()){
                throw new Exception("Adding a collection of items failed on save.");
            }

            return Ok();
        }
    }
}