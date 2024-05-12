using Domain.Entities;

namespace Application.IUseCase
{
    public interface ICategoriaUseCase
    {
        List<Categoria> ListarCategorias();

        bool ValidarCategoria(string categoria);
    }
}
