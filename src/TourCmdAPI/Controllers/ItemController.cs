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
    public class ItemController : ControllerBase
    {
        private readonly IOrderRepository repo;
        private readonly IMapper mapper;
        public ItemController(IOrderRepository repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> getItems()
        {
            var itemsFromEntities = await this.repo.GetItems();
            var mappedItems = mapper.Map<IEnumerable<Item>>(itemsFromEntities);
            if(mappedItems == null)
                return NotFound();
            return Ok(mappedItems);
        }

       [HttpGet("{itemId}", Name = "GetItemById") ]
        [RequestHeaderMatchesMediaType("Accept",
            new[] {"application/vnd.jose.item+json"})]
        public async Task<IActionResult> GetItemById(int itemId) {
            return await GetSpecificItem<Item>(itemId);
        }
        
        [HttpGet("{itemId}")]
        [RequestHeaderMatchesMediaType("Accept",
        new [] {"application/vnd.jose.itemwithestimatedcost+json"})]
        public async Task<IActionResult> GetItemWithEstimatedCost(int itemId){
            return await GetSpecificItem<ItemWithEstimatedCost>(itemId);
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type",
            new[] {"application/json",
                   "application/vnd.jose.itemforcreation+json"})]
        public async Task<IActionResult> AddItem([FromBody] ItemForCreation item){
            if(item == null)
                return BadRequest();
            //validation of DTO happnes here
            return await AddSpecificItem<ItemForCreation>(item);
        }
        


        public async Task<IActionResult> AddSpecificItem<T>(T item) where T : class{
            var itemEntity = mapper.Map<Entities.Item>(item);
            // if(tourEntity.ManagerId == Guid.Empty){
            //     tourEntity.ManagerId = new Guid("fec0a4d6-5830-4eb8-8024-272bd5d6d2bb");
            // }
            await repo.AddItem(itemEntity);
            if(!await repo.SaveAsync()){
                throw new Exception("Adding item failed on save.");
            }
            //now map the entity item into dto object for the client
            var itemToReturn = mapper.Map<Item>(itemEntity);

            //mathcing the uri for /api/item/itemId
            return CreatedAtRoute("GetItemById", 
                new {itemId = itemToReturn.ItemId}, itemToReturn);
        }

        private async Task<IActionResult> GetSpecificItem<T>(int itemId) where T : class{
            var itemFromRepo = await this.repo.GetItemById(itemId);
            var itemToReturn = mapper.Map<T>(itemFromRepo);

            if(itemToReturn == null)
                return NotFound();
            return Ok(itemToReturn);
        }
    }
}