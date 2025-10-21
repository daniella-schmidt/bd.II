using EFAereoNuvem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFAereoNuvem.Data.Mapping;

public class ReservationMap : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
               .ValueGeneratedOnAdd();

        builder.Property(r => r.CodeRersevation)
            .IsRequired()               
            .HasColumnType("uniqueidentifier") // tipo Guid no SQL Server
            .HasDefaultValueSql("NEWID()");   // gera automaticamente no banco

        builder.Property(r => r.Price)
               .IsRequired()
               .HasColumnType("float");

        builder.Property(r => r.DateReservation)
               .IsRequired()
               .HasColumnType("datetime");

        builder.HasOne(r => r.Client)
               .WithMany(c => c.Reservations) 
               .HasForeignKey(r => r.ClientId)
               .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento 1:N com Flight
        builder.HasOne(r => r.Flight)
               .WithMany(f => f.Reservations)
               .HasForeignKey(r => r.FlightId)
               .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento 1:1 opcional com Armchair
        builder.HasOne(r => r.Armchair)
               .WithMany(a => a.Reservations)
               .HasForeignKey(r => r.ArmchairId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict); // não deletar reserva se cadeira for apagada
    }
}
