using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using TourCmdAPI.DbContexts;
using TourCmdAPI.Entities;
using TourCmdAPI.IRepos;
using TourCmdAPI.Repos;

namespace TourCmdAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
 
            var builder = new NpgsqlConnectionStringBuilder();
            builder.ConnectionString = Configuration.GetConnectionString("TourConnection");
            builder.Username = Configuration["UserID"];
            builder.Password = Configuration["Password"];

            services.AddDbContext<TourContext>(opt => opt.UseNpgsql
            (builder.ConnectionString)
            );
            
            services.AddScoped<ITourRepository, TourRepository>();
             // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping());
            });
            

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            TourContext context)
        {
            context.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // var config = new MapperConfiguration(cfg =>
            // {
            //     cfg.CreateMap<Entities.Tour, Dtos.Tour>();
            // });

             var config = new MapperConfiguration(config =>
            {
                config.CreateMap<Entities.Tour, Dtos.Tour>()
                    .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));

                // config.CreateMap<Entities.Tour, Dtos.TourWithEstimatedProfits>()
                //    .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));

                config.CreateMap<Entities.Band, Dtos.Band>();
                config.CreateMap<Entities.Manager, Dtos.Manager>();
                config.CreateMap<Entities.Show, Dtos.Show>();

                // config.CreateMap<Dtos.TourForCreation, Entities.Tour>();
                // config.CreateMap<Dtos.TourWithManagerForCreation, Entities.Tour>();

                // config.CreateMap<Entities.Tour, Dtos.TourWithShows>()
                //    .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));

                // config.CreateMap<Entities.Tour, Dtos.TourWithEstimatedProfitsAndShows>()
                //     .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));

                // config.CreateMap<Dtos.TourWithShowsForCreation, Entities.Tour>();
                // config.CreateMap<Dtos.TourWithManagerAndShowsForCreation, Entities.Tour>();
                // config.CreateMap<Dtos.ShowForCreation, Entities.Show>();

                // config.CreateMap<Entities.Tour, Dtos.TourForUpdate>().ReverseMap();

            });


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //needs to be removed
                // endpoints.MapGet("/", async context =>
                // {
                //     await context.Response.WriteAsync("Hello World!");
                // });
            });
        }
    }
}
