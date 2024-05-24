using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICategoriaRepository
    {
        Task SalvarCategoria(Categoria categoria);
        Task EditarCategoria(string nome, Categoria categoria);
        Task<List<Categoria>> ListarCategorias();
        Task<Categoria> ObterCategoriaPorNome(string nome);
        Task DeletarCategoria(string nome);
    }
}
