using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TourCmdAPI.Services;

namespace TourCmdAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
             var host = CreateHostBuilder(args).Build();

            // migrate & seed the database.  Best practice = in Main, using service scope
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    // var context = scope.ServiceProvider.GetService<TourContext>();
                    // context.Database.Migrate();
                    // context.EnsureSeedDataForContext();

                    // var orderContext = scope.ServiceProvider.GetService<OrderContext>();
                    // orderContext.Database.Migrate();
                    // orderContext.EnsureSeedDataForOrderContext();
                }
                catch (System.Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred with migrating or seeding the DB.");
                }     
                // using (var appContext = scope.ServiceProvider.GetRequiredService<TourContext>())
                // {
                //     try
                //     {
                //         appContext.Database.Migrate();
                //         appContext.EnsureSeedDataForContext();
                //     }
                //     catch (Exception ex)
                //     {
                //         //Log errors or do anything you think it's needed
                //         throw;
                //     }
                // }
            }

            CreateHostBuilder(args).Build().Run();
            //  var host = BuildWebHost(args);

            // // migrate & seed the database.  Best practice = in Main, using service scope
            // using (var scope = host.Services.CreateScope())
            // {
            //     try
            //     {
            //         var context = scope.ServiceProvider.GetService<TourContext>();
            //         context.Database.Migrate();
            //         context.EnsureSeedDataForContext();
            //     }
            //     catch (System.Exception ex)
            //     {
            //         var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            //         logger.LogError(ex, "An error occurred with migrating or seeding the DB.");
            //     }               
            // }

            // // run the web app
            // host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //  public static IWebHost BuildWebHost(string[] args) =>
        //     WebHost.CreateDefaultBuilder(args)
        //         .UseStartup<Startup>()
        //         .Build();
    }
}
