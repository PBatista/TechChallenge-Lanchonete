using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Sql
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produto { get; set; }
    }
}
