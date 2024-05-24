using Domain.Entities;

namespace Application.IUseCase
{
    public interface ICategoriaUseCase
    {
        Task<List<Categoria>> ListarCategorias();
        Task<Categoria> ObterCategoriaPorNome(string nome);
        Task<bool> ValidarCategoria(string categoria);
        Task SalvarCategoria(Categoria categoria);
        Task EditarCategoria(string nome, Categoria categoria);
        Task DeletarCategoria(string nome);
    }
}
