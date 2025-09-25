using Microsoft.EntityFrameworkCore;

namespace financeiro.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Models.TransacaoModel> Transacao { get; set; }
        public DbSet<Models.CategoriaModel> Categoria { get; set; }
    }
}
