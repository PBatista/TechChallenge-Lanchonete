using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> ListarProdutos();
        Task<List<Produto>> ObterProdutosPorCategoria(string categoria);
        Task<Produto> ObterProdutosPorNome(string nome);
        Task SalvarProduto(Produto produto);
        Task EditarProduto(string nome, Produto produto);
        Task DeletarProduto(string nome);
    }
}
