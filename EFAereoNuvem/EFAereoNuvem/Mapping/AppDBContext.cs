using EFAereoNuvem.Models;
using Microsoft.EntityFrameworkCore;

namespace EFAereoNuvem.Mapping
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Scale> Scales { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientStatus> ClientStatuses { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<Armchair> Armchairs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar relacionamento 1 para 1 entre Client e CurrentAdress
            modelBuilder.Entity<Client>()
                .HasOne(c => c.CurrentAdress)
                .WithOne()
                .HasForeignKey<Client>(c => c.CurrentAdressId)
                .OnDelete(DeleteBehavior.Restrict); // Ou Cascade se preferir

            // Configurar relacionamento 1 para 1 entre Client e FutureAdress (opcional)
            modelBuilder.Entity<Client>()
                .HasOne(c => c.FutureAdress)
                .WithOne()
                .HasForeignKey<Client>(c => c.FutureAdressId)
                .OnDelete(DeleteBehavior.Restrict);

            // Se quiser a navegação de volta no Adress, configure:
            modelBuilder.Entity<Adress>()
                .HasOne(a => a.Client)
                .WithOne() // Sem especificar a propriedade de navegação
                .HasForeignKey<Adress>(); // Sem chave estrangeira específica

            base.OnModelCreating(modelBuilder);
        }

    }
}
