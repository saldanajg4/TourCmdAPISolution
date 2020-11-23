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
    public class IngredientCategoryController : ControllerBase
    {
        private readonly IOrderRepository repo;
        private readonly IMapper mapper;
        public IngredientCategoryController(IOrderRepository repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;
        }
        [HttpGet("alling")]
        [RequestHeaderMatchesMediaType("Accept",
            new[] {"application/json", "application/vnd.jose.allingredients+json"})]
        public async Task<IActionResult> GetAllIngredients(){
            var ingEntities = await this.repo.GetAllIngredients();
            if(ingEntities == null)
                return BadRequest();
            var ingDtos = mapper.Map<IEnumerable<Ingredient>>(ingEntities);
            return Ok(ingDtos);
        }
        [HttpGet("{ingredientId}", Name = "GetIngredientById")]
        [RequestHeaderMatchesMediaType("Accept",
            new[] {"application/json", "application/vnd.jose.ingredient+json"})]
        public async Task<IActionResult> GetIngredientById(int ingredientId){
            var ingredientEntity = await this.repo.GetIngredientById(ingredientId);
            if(ingredientEntity == null)
                return NotFound();
            var ingredientDto = mapper.Map<Ingredient>(ingredientEntity);
            return Ok(ingredientDto);
        }
        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type",
            new[] {"applicaiton/json", "application/vnd.jose.ingredientforcreation+json"})]
        public async Task<IActionResult> AddIngredient([FromBody] IngredientForCreation ingredient){
            var ingredientEntity = mapper.Map<Entities.Ingredient>(ingredient);
            await this.repo.AddIngredient(ingredientEntity);
            
            if(!await this.repo.SaveAsync())
                throw new Exception("Error adding ingredient");
            var ingredientDto = mapper.Map<Ingredient>(ingredientEntity);
            return CreatedAtRoute("GetIngredientById",
                new {ingredientId = ingredientEntity.IngredientId}, ingredientDto);
        }

        [HttpGet("allingcat")]
        [RequestHeaderMatchesMediaType("Accept",
            new[] {"application/vnd.jose.allingredientcategories+json"})]
        public async Task<IActionResult> GetAllIngredientCategories(){
            var ingCatEntities = await this.repo.GetAllIngredientCategories();
            if(ingCatEntities == null)
                return NotFound();
            var ingCatDtos = mapper.Map<IEnumerable<Dtos.IngredientCategory>>(ingCatEntities);
            return Ok(ingCatDtos);
        }

        [HttpGet("{ingredientCategoryId}", Name = "GetIngredienCategoryById")]
        [RequestHeaderMatchesMediaType("Accept",
            new[] {"application/json", "application/vnd.jose.ingredientcategory+json"})]
        public async Task<IActionResult> GetIngredientCategoryById(int ingredientCategoryId){
            var ingCatEntity = await this.repo.GetIngredientCategoryById(ingredientCategoryId);
            if(ingCatEntity == null)
                return NotFound();
            var ingCatDto = mapper.Map<IngredientCategory>(ingCatEntity);
            return Ok(ingCatDto);
        }

        [HttpPost("ingcat")]
        [RequestHeaderMatchesMediaType("Content-Type",
            new[] {"application/json", "application/vnd.jose.ingredientcategoryforcreation+json"})]
        public async Task<IActionResult> AddIngredientCategory([FromBody]IngredientCategoryForCreation ingCat){
            var ingCatEntity = mapper.Map<Entities.IngredientCategory>(ingCat);
            await this.repo.AddIngredientCategory(ingCatEntity);
            if(!await this.repo.SaveAsync()){
                throw new Exception("Error adding ingredient category");
            }
            var ingCatDto = mapper.Map<Dtos.IngredientCategory>(ingCatEntity);
            return CreatedAtRoute("GetIngredienCategoryById",
                new {ingredientCategoryId = ingCatDto.IngredientCategoryId}, ingCatDto);
            // return Ok();
        }

    }
}