using Application.IUseCase;
using Domain.Base;
using Domain.Entities;
using Domain.Repositories;
using InfraMongoDb.Repositories;

namespace Application.UseCase
{
    public class CategoriaUseCase : ICategoriaUseCase
    {
        public readonly ICategoriaRepository _categoriaRepository;

        public CategoriaUseCase(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public Task<List<Categoria>> ListarCategorias()
        {
            return _categoriaRepository.ListarCategorias();
        }

        public async Task<bool> ValidarCategoria(string categoria)
        {
            try
            {
                var categorias = await _categoriaRepository.ListarCategorias();
                return categorias.Any(x => x.Nome.Trim() == categoria.Trim());
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível validar a categoria '{categoria}'.", ex);                
            }           
        }

        public async Task SalvarCategoria(Categoria categoria)
        {
            try
            {
                var clienteExistente = await _categoriaRepository.ObterCategoriaPorNome(categoria.Nome);
                if (clienteExistente == null) await _categoriaRepository.SalvarCategoria(categoria);
                else throw new DomainException($"Não foi possível salvar, pois a categoria '{categoria.Nome}' já está cadastrada!");
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível salvar a categoria", ex);
            }
        }

        public async Task EditarCategoria(string nome, Categoria categoria)
        {
            try
            {
                await _categoriaRepository.EditarCategoria(nome, categoria);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível editar a categoria '{nome}'.", ex);
            }
        }

        public async Task<Categoria> ObterCategoriaPorNome(string nome)
        {
            try
            {
                return await _categoriaRepository.ObterCategoriaPorNome(nome);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi obter a categoria '{nome}'", ex);
            }
        }

        public async Task DeletarCategoria(string nome)
        {
            try
            {
                await _categoriaRepository.DeletarCategoria(nome.ToUpper());
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível deletar o produto '{nome}'.", ex);
            }
        }
    }
}
