using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webAPISecSess.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<GuidedTour> GuidedTourSet { get; set; }
        public DbSet<Category> CategorySet { get; set; }
        public DbSet<PlaceWithOrder> PlaceWithOrderSet { get; set; }
        public DbSet<TouristPlace> TouristPlaceSet { get; set; }
        public DbSet<PlaceToEat> PlacesToEatSet { get; set; }
        public DbSet<Transport> TransportSet { get; set; }
        public object DeleteBehavior { get; private set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GuidedTour>()
            .HasOptional(p => p.PlaceToEat)
            .WithMany(p => p.GuidedTour)
            .HasForeignKey(c => c.Id_PlaceToEat)
            .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
