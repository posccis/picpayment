using Microsoft.EntityFrameworkCore;
using PicPayment.Domain.Domains;


namespace PicPayment.Persistence.Context
{
    public class PicPaymentContext : DbContext
    {

        public PicPaymentContext(DbContextOptions op) : base(op) 
        { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Transferencia> Transferencias { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasIndex(u => new { u.Id, u.CPF, u.Email }).IsUnique();
            modelBuilder.Entity<Transferencia>().HasIndex(t => t.Id).IsUnique();

            modelBuilder.Entity<Transferencia>()
                .HasOne(t => t.ContaOrigem)
                .WithMany(u => u.TransferenciasOrigem)
                .HasForeignKey(t => t.IdContaOrigem)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transferencia>()
                .HasOne(t => t.ContaDestino)
                .WithMany(u => u.TransferenciasDestino)
                .HasForeignKey(t => t.IdContaDestino)
                .OnDelete(DeleteBehavior.Restrict);


            //var usuarios = Seed.GetUsuarioSeed();
            //var transferencias = Seed.GetTransferenciaSeed(usuarios);

            //modelBuilder.Entity<Usuario>().HasData(usuarios);
            //modelBuilder.Entity<Transferencia>().HasData(transferencias);

            base.OnModelCreating(modelBuilder);
            
        }


    }
}
