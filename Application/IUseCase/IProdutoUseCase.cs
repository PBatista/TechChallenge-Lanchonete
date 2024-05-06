using Domain.Entities;

namespace Application.IUseCase
{
    public interface IProdutoUseCase
    {
        Task<IEnumerable<Produto>> ListarProdutos();

        Task<IEnumerable<Produto>> ObterProdutosPorCategoria(int categoria_id);

        Task SalvarProduto(Produto produto);

        Task EditarProduto(Produto produto);

        Task DeletarProduto(int Id);
        
    }
}
