using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TourCmdAPI.Controllers;
using TourCmdAPI.Services;
using TourCmdAPI.Entities;
using TourCmdAPI.IRepos;
using TourCmdAPI.Repos;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using TourCmdAPI.Filter;
using TourCmdAPI.Wrappers;

namespace TourCmdAPI.Tests
{
    public class TourCmdAPITests
    {
        DbContextOptionsBuilder<TourContext> tourBuilder;
        public TourController tourController { get; set; }
        public TourContext tourContext { get; set; }
        public ITourRepository _repo { get; set; }
        public PaginationFilter filter { get; set; }

        public TourCmdAPITests()
        {
            filter = new PaginationFilter();
             filter.PageNumber = 1;
            filter.PageSize = 2;  
             var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            

            tourBuilder = new DbContextOptionsBuilder<TourContext>();
            tourBuilder.UseInMemoryDatabase("TourApiDb");
            tourContext = new TourContext(tourBuilder.Options);
            _repo = new TourRepository(tourContext);
            tourController = new TourController(_repo,mapper, new UriService());
            tourContext.EnsureSeedDataForContext(); 
            
        }

        [Fact]
        public void GetTourObject_ShouldNotBeNull()
        {
            //Arrange
           
            //Act
            var tours = tourController.getAllTours(filter);
            //Assert
            Assert.NotNull(tours);
        }

        // [Fact]
        [TokenAuthenticationFilter]
        public void GetToursReturnCorrectType(){
            //Arrange
           

            //Act
            var tours = tourController.getAllTours(filter);

            //Assert
            Assert.IsType<OkObjectResult>(tours.Result);
        }

        // [Fact]
        public void GetAllTours_ShouldReturnGreaterThanZeroElements(){
            //Arrange
            //Act
            var tours = tourController.getAllTours(filter);
            var okTourResult = tours.Result as OkObjectResult;
  
            //Assert
            Assert.NotNull(okTourResult);
            Assert.Equal(200, okTourResult.StatusCode);//
            // Assert.True(((List<Tour>)okTourResult.Value).Count > 0);
        }
        [Fact]
        public void GetTourReturnsNullWhenInvalid(){
            //act
            var actualTour = tourController.GetTourById(new Guid("b7d637df-0566-47aa-a74c-dad95c00c3f0"));
            var actualTourResult = actualTour.Result as NotFoundObjectResult;

            //assert is empty list, tour not found
            Assert.Null(actualTourResult);
        }

         [Fact]
        public void GetTourReturns404NotFoundTypeWhenInvalid(){
            //act
            var actualTour = tourController.GetTourById(new Guid("b7d637df-0566-47aa-a74c-dad95c00c3f0"));
            // var actualTourResult = actualTour.Result as OkObjectResult;

            //assert is empty list, tour not found
            Assert.IsType<NotFoundResult>(actualTour.Result);
        }

           [Fact]
        public void GetTourReturnsOkFoundTypeWhenValid(){
            //act
            var actualTour = tourController.GetTourById(new Guid("c7ba6add-09c4-45f8-8dd0-eaca221e5d93"));
            // var actualTourResult = actualTour.Result as OkObjectResult;

            //assert is empty list, tour not found
            Assert.IsType<OkObjectResult>(actualTour.Result);
        }
    }
}
