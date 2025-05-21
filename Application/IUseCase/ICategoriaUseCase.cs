using Application.ApplicationDTO;
using Domain.Entities;

namespace Application.IUseCase
{
    public interface ICategoriaUseCase
    {
        Task<List<Categoria>> ListarCategorias();
        Task<Categoria> ObterCategoriaPorNome(string nome);
        Task<bool> ValidarCategoria(string categoria);
        Task SalvarCategoria(CategoriaDTO categoria);
        Task EditarCategoria(string nome, CategoriaDTO categoria);
        Task DeletarCategoria(string nome);
    }
}
