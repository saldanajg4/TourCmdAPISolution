using Microsoft.EntityFrameworkCore;
using TourCmdAPI.Entities;

namespace TourCmdAPI.DbContexts{
    public class TourContext : DbContext{
        public TourContext(DbContextOptions<TourContext> options) : base(options)
        {
           
        }
         public DbSet<Tour> TourItems { get; set; }
    }
}