using Application.ApplicationDTO;
using Domain.Entities;

namespace Application.IPresenters
{
    public interface ICategoriaPresenter
    {
        List<CategoriaDTO> PresentMany(List<Categoria> categorias);
        CategoriaDTO Present(Categoria categorias);
    }
}
