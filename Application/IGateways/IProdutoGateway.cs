using Domain.Entities;

namespace Application.IGateways;

public interface IProdutoGateway
{
    Task<List<Produto>> ListarProdutos();
    Task<List<Produto>> ObterProdutosPorCategoria(string categoria);
    Task<Produto> ObterProdutoPorNome(string nome);
    Task SalvarProduto(Produto produto);
    Task EditarProduto(string nome, Produto produto);
    Task DeletarProduto(string nome);
}
