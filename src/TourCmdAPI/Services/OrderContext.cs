using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourCmdAPI.Entities;

namespace TourCmdAPI.Services
{
    public class OrderContext : DbContext
    {
         private readonly IUserInfoService _userInfoService;
        public OrderContext(DbContextOptions<TourContext> options,
            IUserInfoService userInfoService) : base(options)
        {
           // userInfoService is a required argument
            _userInfoService = userInfoService ?? throw new ArgumentNullException(nameof(userInfoService));
        }

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
            
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientCategory> IngredientCategories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi. ItemId});
            // builder.Entity<OrderItem>()
            //     .HasOne(oi => oi.Order)
            //     .WithMany(o => o.OrderItems)
            //     .HasForeignKey(oi => oi.OrderId);
            
            // builder.Entity<OrderItem>()
            //     .HasOne(oi => oi.Item)
            //     .WithMany(i => i.OrderItems)
            //     .HasForeignKey(oi => oi.ItemId);

            // builder.Entity<IngredientCategory>()
            //     .HasIndex(i => i.IngredientCategoryName)
            //     .IsUnique();
            // builder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId});
        }

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

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    // entity.CreatedBy = _userInfoService.UserId;
                    entity.CreatedBy = "jose";
                    entity.CreatedOn = DateTime.UtcNow;
                }

                // entity.UpdatedBy = _userInfoService.UserId;
                entity.UpdatedBy = "jose";
                entity.UpdatedOn = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}