using Application.IUseCase;
using Domain.Base;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCase
{
    public class CategoriaUseCase : ICategoriaUseCase
    {
        public readonly ICategoriaRepository _categoriaRepository;

        public CategoriaUseCase(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public List<Categoria> ListarCategorias()
        {
            return _categoriaRepository.ListarCategorias();
        }

        public bool ValidarCategoria(string categoria)
        {
            try
            {
                List<Categoria> categorias = _categoriaRepository.ListarCategorias();
                return categorias.Any(x => x.Nome.Trim() == categoria.Trim());
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível validar a categoria '{categoria}'.", ex);                
            }           
        }
    }
}
