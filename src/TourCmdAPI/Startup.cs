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
using TourCmdAPI.TokenAuthentication;

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
                    //tour output formatters from the web api into client
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.tour+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.tourwithestimatedprofits+json");
                     jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.tourwithshows+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.tourwithestimatedprofitsandshows+json");

                    //order formatters from the web api into client
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.order+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.orderwithitems+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.item+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.itemwithestimatedcost+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.employee+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.allcustomers+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.customer+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.ingredientcategory+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.allingredientcategories+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.ingredient+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.allingredients+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.orderitemcollectionforcreation+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.paymentdetails+json");
                    
                }

                var jsonInputFormatter = setupAction.OutputFormatters
                    .OfType<SystemTextJsonInputFormatter>().FirstOrDefault();
                //tour input formatters from client into web api like in httppost
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

                    //order input formatters from client into web api like in httppost
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.itemforcreation+json");
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.itemcollectionforcreation+json");
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.employeeforcreation+json");
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.customerforcreation+json");
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.ingredientcategoryforcreation+json");
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.ingredientforcreation+json");
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.orderforcreation+json");
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.orderitemcollectionforcreation+json");
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.jose.paymentdetailsforcreation+json");
                }

            });
 
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllTourOriginsHeadersAndMethods",
                    // builder => builder.WithOrigins("https://demoapiapp.azurewebsites.net",
                    builder => builder.WithOrigins("https://localhost:4200",
                                                    "https://paymentappapi.azurewebsites.net")
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod());
            });

            var builder = new NpgsqlConnectionStringBuilder();
            builder.ConnectionString = Configuration.GetConnectionString("TourConnection");
            builder.Username = Configuration["UserID"];
            builder.Password = Configuration["Password"];

            services.AddDbContext<TourContext>(opt => opt.UseLazyLoadingProxies()
            .UseNpgsql(builder.ConnectionString));
            // services.AddDbContext<TourContext>(opt => opt.UseNpgsql(builder.ConnectionString));

            var orderBuilder = new NpgsqlConnectionStringBuilder();
            orderBuilder.ConnectionString = Configuration.GetConnectionString("OrderConnection");
            orderBuilder.Username = Configuration["UserID"];
            orderBuilder.Password = Configuration["Password"];

            services.AddDbContext<OrderContext>(opt => opt.UseNpgsql
            (orderBuilder.ConnectionString)
            );
            
            services.AddScoped<ITourRepository, TourRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<IPaymentDetailsRepository, PaymentDetailsRepository>();
             // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping());
            });
            

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriServices>(o =>
            {//getting the base URL of the application
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
            // services.AddMemoryCache();
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
