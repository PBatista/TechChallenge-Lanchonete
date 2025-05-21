using Application.ApplicationDTO;
using Application.IPresenters;
using Domain.Entities;

namespace API.Presenters
{
    public class CategoriaPresenter : ICategoriaPresenter
    {
        public List<CategoriaDTO> PresentMany(List<Categoria> categorias)
        {
            return categorias.Select(Present).ToList();
        }

        public CategoriaDTO Present(Categoria categoria)
        {
            return new CategoriaDTO
            {
                Nome = categoria.Nome                
            };
        }
    }
}
