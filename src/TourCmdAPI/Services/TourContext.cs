using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourCmdAPI.Entities;
using TourCmdAPI.Services;

namespace TourCmdAPI.Services{
    public class TourContext : DbContext{
        private readonly IUserInfoService _userInfoService;
        public TourContext(DbContextOptions<TourContext> options,
            IUserInfoService userInfoService) : base(options)
        {
           // userInfoService is a required argument
            _userInfoService = userInfoService ?? throw new ArgumentNullException(nameof(userInfoService));
        }
         public TourContext(DbContextOptions<TourContext> options) : base(options)
        {
           
        }
         public DbSet<Tour> Tours { get; set; }
          public DbSet<Show> Shows { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Manager> Managers { get; set; }

        


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken 
            = default(CancellationToken))
        {
            // get added or updated entries
            var addedOrUpdatedEntries = ChangeTracker.Entries()
                    .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

            // fill out the audit fields
            foreach (var entry in addedOrUpdatedEntries)
            {
                var entity = entry.Entity as AuditableEntity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedBy = _userInfoService.UserId;
                    entity.CreatedOn = DateTime.UtcNow;
                }

                entity.UpdatedBy = _userInfoService.UserId;
                entity.UpdatedOn = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}