using Domain.Entities;
using Domain.Repositories;
using Infra.Sql;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly SqlDbContext _dbContext;

        public ProdutoRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Produto>> ListarProdutos()
        {
           return await _dbContext.Produto.ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorCategoria(int categoria_id)
        {
            return await _dbContext.Produto
                .Where(p => p.Categoria_id == categoria_id)
                .ToListAsync();
        }

        public async Task SalvarProduto(Produto produto)
        {
            _dbContext.Produto.Add(produto);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditarProduto(Produto produto)
        {
            _dbContext.Entry(produto).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }       

        public async Task DeletarProduto(int id)
        {
            var produto = await _dbContext.Produto.FindAsync(id);
            if (produto != null)
            {
                _dbContext.Produto.Remove(produto);
                await _dbContext.SaveChangesAsync();
            }
        }       
    }
}
