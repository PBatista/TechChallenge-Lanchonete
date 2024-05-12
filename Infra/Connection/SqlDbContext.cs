using Domain.Entities;
using Infra.DTO;
using Microsoft.EntityFrameworkCore;

namespace Infra.Sql
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Pedido> Pedido { get; set; }

        public DbSet<Categoria> Categoria { get; set; }

    }

}
