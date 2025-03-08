using AgendaApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendaApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contato> Contatos { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}