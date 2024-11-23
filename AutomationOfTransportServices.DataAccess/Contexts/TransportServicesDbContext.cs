using AutomationOfTransportServices.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomationOfTransportServices.DataAccess.Contexts
{
    public class TransportServicesDbContext:DbContext
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
                    entity.Property(e => e.Id).HasColumnName("id");
                    entity.Property(e => e.Name)
                        .HasColumnName("name")
                        .HasMaxLength(20);
                    entity.Property(e => e.NumberOfTelephone)
                        .HasColumnName("telephone")
                        .HasMaxLength(11);
                });

                modelBuilder.Entity<DriverEntity>(entity =>
                {
                    entity.HasKey(e => e.Id).HasName("drivers_pkey");
                    entity.ToTable("drivers");
                    entity.Property(e => e.Id).HasColumnName("id");
                    entity.Property(e => e.Name)
                        .HasColumnName("name")
                        .HasMaxLength(20);
                });

                modelBuilder.Entity<StringOfServiceEntity>(entity =>
                {
                    entity.HasKey(e => e.Id).HasName("strings_of_services_pkey");
                    entity.ToTable("strings_of_services");
                    entity.Property(e => e.Id).HasColumnName("id");

                    entity.Property(e=>e.Number).HasColumnName("n");
                    entity.Property(e => e.Date).HasColumnName("date");
                    entity.Property(e => e.Distance).HasColumnName("distance");
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
                    entity.HasOne(d => d.Vehicle).WithMany(p => p.Strings).HasForeignKey(d => d.ClientId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("strings_of_services_vehicle_id_fkey");
                });

                modelBuilder.Entity<TypeOfServiceEntity>(entity =>
                {
                    entity.HasKey(e => e.Id).HasName("types_of_services_pkey");
                    entity.ToTable("types_of_services");
                    entity.Property(e => e.Id).HasColumnName("id");
                    entity.Property(e => e.Name)
                        .HasColumnName("name")
                        .HasMaxLength(30);
                });

                modelBuilder.Entity<VehicleEntity>(entity =>
                {
                    entity.HasKey(e => e.Id).HasName("vehicle_pkey");
                    entity.ToTable("vehicles");
                    entity.Property(e => e.Id).HasColumnName("id");
                    entity.Property(e => e.Name)
                        .HasColumnName("name")
                        .HasMaxLength(6);
                    entity.Property(e => e.PriceOfKm)
                        .HasColumnName("price_of_km");
                });

            }
    }
}
