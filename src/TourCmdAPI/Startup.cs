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
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using TourCmdAPI.Services;
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
            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;

                var jsonOutputFormatter = setupAction.OutputFormatters
                    .OfType<SystemTextJsonOutputFormatter>().FirstOrDefault();

                if(jsonOutputFormatter != null){
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.tour+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.tourwithestimatedprofits+json");
                     jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.tourwithshows+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.tourwithestimatedprofitsandshows+json");

                    //order formatters
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.order+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.orderwithitems+json");
                }

                var jsonInputFormatter = setupAction.OutputFormatters
                    .OfType<SystemTextJsonInputFormatter>().FirstOrDefault();

                if(jsonInputFormatter != null){
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.tourforcreation+json");
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.tourwithmanagerforcreation+json");
                     jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.tourwithshowsforcreation+json");
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.tourwithmanagerandshowsforcreation+json");
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.showcollectionforcreation+json");
                }

            });
 
 // Configure CORS so the API allows requests from JavaScript.  
            // For demo purposes, all origins/headers/methods are allowed.  
            // services.AddCors(options =>
            // {
            //     options.AddPolicy("AllowAllOriginsHeadersAndMethods",
            //         builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            // });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllTourOriginsHeadersAndMethods",
                    builder => builder.WithOrigins("https://localhost:4200"));
            });

            var builder = new NpgsqlConnectionStringBuilder();
            builder.ConnectionString = Configuration.GetConnectionString("TourConnection");
            builder.Username = Configuration["UserID"];
            builder.Password = Configuration["Password"];

            services.AddDbContext<TourContext>(opt => opt.UseNpgsql
            (builder.ConnectionString)
            );

            var orderBuilder = new NpgsqlConnectionStringBuilder();
            orderBuilder.ConnectionString = Configuration.GetConnectionString("OrderConnection");
            orderBuilder.Username = Configuration["UserID"];
            orderBuilder.Password = Configuration["Password"];

            services.AddDbContext<OrderContext>(opt => opt.UseNpgsql
            (orderBuilder.ConnectionString)
            );
            
            services.AddScoped<ITourRepository, TourRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
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
            TourContext context, OrderContext orderContext)
        {
            context.Database.Migrate();
            orderContext.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //  var config = new MapperConfiguration(config =>
            // {
            //     config.CreateMap<Entities.Tour, Dtos.Tour>()
            //         .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));

                // config.CreateMap<Entities.Tour, Dtos.TourWithEstimatedProfits>()
                //    .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));

                // config.CreateMap<Entities.Band, Dtos.Band>();
                // config.CreateMap<Entities.Manager, Dtos.Manager>();
                // config.CreateMap<Entities.Show, Dtos.Show>();

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

            // });

            app.UseHttpsRedirection();
            // Enable CORS
            // app.UseCors("AllowAllOriginsHeadersAndMethods");
            app.UseCors("AllowAllTourOriginsHeadersAndMethods");
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }
    }
}
