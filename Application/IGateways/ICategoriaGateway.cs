using Domain.Entities;

namespace Application.IGateways
{
    public interface ICategoriaGateway
    {
        Task<List<Categoria>> ListarCategorias();
        Task<Categoria> ObterCategoriaPorNome(string nome);
        Task<bool> CategoriaExiste(string nome);
        Task SalvarCategoria(Categoria categoria);
        Task EditarCategoria(string nome, Categoria categoria);
        Task DeletarCategoria(string nome);
    }
}
