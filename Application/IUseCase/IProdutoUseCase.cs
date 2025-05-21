using Application.ApplicationDTO;
using Domain.Entities;

namespace Application.IUseCase
{
    public interface IProdutoUseCase
    {
        Task<List<Produto>> ListarProdutos(); // uso interno (AppController usa presenter)
        Task<List<Produto>> ObterProdutosPorCategoria(string categoria);
        Task<Produto> ObterProdutoPorNome(string nome);
        Task<List<Produto>> ListarProdutos(PedidoApplicationDTO pedidoDTO); // manter como está
        Task SalvarProduto(ProdutoDTO dto);
        Task EditarProduto(string nome, ProdutoDTO dto);
        Task DeletarProduto(string nome);

    }
}
