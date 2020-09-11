using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TourCmdAPI.Controllers;
using TourCmdAPI.Services;
using TourCmdAPI.Entities;
using TourCmdAPI.IRepos;
using TourCmdAPI.Repos;
using Xunit;

namespace TourCmdAPI.Tests
{
    public class TourCmdAPITests
    {
        DbContextOptionsBuilder<TourContext> tourBuilder;
        public TourController tourController { get; set; }
        public TourContext tourContext { get; set; }
        public ITourRepository _repo { get; set; }

        public TourCmdAPITests()
        {
             var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            

            tourBuilder = new DbContextOptionsBuilder<TourContext>();
            tourBuilder.UseInMemoryDatabase("TourApiDb");
            tourContext = new TourContext(tourBuilder.Options);
            _repo = new TourRepository(tourContext);
            tourController = new TourController(_repo,mapper);
        }

        [Fact]
        public void Test1()
        {
            //Arrange
            
            //Act
            var tours = tourController.getAllTours();
            //Assert
            Assert.NotNull(tours);
        }
    }
}
