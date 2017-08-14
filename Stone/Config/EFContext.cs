using Stone.Domain.Entities;
using System.Data.Entity;

namespace Stone.Config
{
    public class EFContext : DbContext
    {
        public EFContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .ToTable("City")
                .HasKey(c => c.ID)
                .HasMany(c => c.Temperatures)
                .WithRequired(c => c.City)
                .HasForeignKey(c => c.CityID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Temperature>()
                .ToTable("Temperature")
                .HasKey(c => c.ID)
                .Property(c => c.CityID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
