using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace App.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
             Database.EnsureCreated();
        }

        public DbSet<Persona> Personas { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Cuenta> Cuentas { get; set; } = null!;
        public DbSet<Movimiento> Movimientos { get; set; } = null!;
    }
}