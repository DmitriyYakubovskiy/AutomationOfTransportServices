using AutomationOfTransportServices.DataAccess.Entities;
using AutomationOfTransportServices.DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AutomationOfTransportServices.DataAccess.Contexts
{
    public class TransportServicesDbContext : DbContext
    {
        public virtual DbSet<ClientEntity> Clients { get; set; }
        public virtual DbSet<DriverEntity> Drivers { get; set; }
        public virtual DbSet<StringOfServiceEntity> Strings { get; set; }
        public virtual DbSet<TypeOfServiceEntity> TypesOfServices { get; set; }
        public virtual DbSet<VehicleEntity> Vehicles { get; set; }

        public TransportServicesDbContext(DbContextOptions<TransportServicesDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientEntity>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("clients_pkey");
                entity.ToTable("clients");
                entity.Property(e => e.Id).HasColumnName("id").IsRequired();
                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(EntityRestrictions.clientNameLength).IsRequired();
                entity.Property(e => e.NumberOfTelephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(EntityRestrictions.clientTelephoneLength).IsRequired();
            });

            modelBuilder.Entity<DriverEntity>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("drivers_pkey");
                entity.ToTable("drivers");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(EntityRestrictions.driverNameLength).IsRequired();
            });

            modelBuilder.Entity<StringOfServiceEntity>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("strings_of_services_pkey");
                entity.ToTable("strings_of_services");
                entity.Property(e => e.Id).HasColumnName("id").IsRequired();

                entity.Property(e => e.Number).HasColumnName("n").IsRequired();
                entity.Property(e => e.Date).HasColumnName("date").IsRequired();
                entity.Property(e => e.Distance).HasColumnName("distance").IsRequired();
                entity.Property(e => e.ClientId).HasColumnName("client_id");
                entity.Property(e => e.DriverId).HasColumnName("driver_id");
                entity.Property(e => e.TypeOfServiceId).HasColumnName("type_of_service_id");
                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.HasOne(d => d.Client).WithMany(p => p.Strings).HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("strings_of_services_client_id_fkey");
                entity.HasOne(d => d.Driver).WithMany(p => p.Strings).HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("strings_of_services_driver_id_fkey");
                entity.HasOne(d => d.TypeOfService).WithMany(p => p.Strings).HasForeignKey(d => d.TypeOfServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("strings_of_services_type_of_service_id_fkey");
                entity.HasOne(d => d.Vehicle).WithMany(p => p.Strings).HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("strings_of_services_vehicle_id_fkey");

                entity.HasIndex(e => new { e.ClientId, e.Number })
                  .IsUnique()
                  .HasDatabaseName("unique_client_service_number");
            });

            modelBuilder.Entity<TypeOfServiceEntity>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("types_of_services_pkey");
                entity.ToTable("types_of_services");
                entity.Property(e => e.Id).HasColumnName("id").IsRequired();
                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(EntityRestrictions.typeOfServiceNameLength).IsRequired();
                entity.HasIndex(e => e.Name)
                  .IsUnique()
                  .HasDatabaseName("unique_type_of_service_name");
            });

            modelBuilder.Entity<VehicleEntity>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("vehicle_pkey");
                entity.ToTable("vehicles");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(EntityRestrictions.vehicleNameLength);
                entity.Property(e => e.PriceOfKm)
                    .HasColumnName("price_of_km");
                entity.HasIndex(e => e.Name)
                  .IsUnique()
                  .HasDatabaseName("unique_vehicle_name");

                entity.HasCheckConstraint("ck_price_of_km", "price_of_km > 0");
            });

        }
    }
}
