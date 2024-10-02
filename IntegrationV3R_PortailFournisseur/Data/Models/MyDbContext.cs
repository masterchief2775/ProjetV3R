using Microsoft.EntityFrameworkCore;

namespace IntegrationV3R_PortailFournisseur.Data.Models
{
    public class MyDbContext
    {        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        // Définis tes DbSet pour les entités ici
        public DbSet<MonEntite> MonEntites { get; set; }
    }
}
