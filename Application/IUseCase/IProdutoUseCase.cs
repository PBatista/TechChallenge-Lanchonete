using Application.ApplicationDTO;
using Domain.Entities;

namespace Application.IUseCase
{
    public interface IProdutoUseCase
    {
        Task<List<Produto>> ListarProdutos();
        Task<List<Produto>> ListarProdutos(PedidoApplicationDTO pedidoDTO);

        Task<List<Produto>> ObterProdutosPorCategoria(string categoria);

        Task<Produto> ObterProdutoPorNome(string nome);

        Task SalvarProduto(Produto produto);

        Task EditarProduto(string nome, Produto produto);

        Task DeletarProduto(string nome);

    }
}
