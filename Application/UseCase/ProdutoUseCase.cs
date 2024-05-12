using Application.IUseCase;
using Domain.Base;
using Domain.Entities;
using Domain.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Printing;

namespace Application.UseCase
{
    public class ProdutoUseCase : IProdutoUseCase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoUseCase(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }       

        public async Task<List<Produto>> ListarProdutos()
        {
            try
            {
                return await _produtoRepository.ListarProdutos();
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível listar os produtos.", ex);                
            }            
        }

        public async Task<List<Produto>> ObterProdutosPorCategoria(string categoria)
        {
            try
            {
                return await _produtoRepository.ObterProdutosPorCategoria(categoria.Trim());
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível obter os produtos pela categoria '{categoria.Trim()}'.", ex);                
            }
            
        }

        public async Task SalvarProduto(Produto produto)
        {
            try
            {
                await _produtoRepository.SalvarProduto(produto);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível salvar o produto '{produto.Nome}'.", ex);                
            }
            
        }

        public async Task EditarProduto(string nome, Produto produto)
        {
            try
            {
                await _produtoRepository.EditarProduto(nome, produto);
            }
            catch (Exception ex)
            { 
                throw new DomainException($"Não foi possível editar o produto '{nome}'.", ex);                
            }            
        }

        public async Task DeletarProduto(string nome)
        {
            try
            {
                await _produtoRepository.DeletarProduto(nome);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível deletar o produto '{nome}'.", ex);                
            }            
        }

        public async Task<Produto> ObterProdutoPorNome(string nome)
        {
            try
            {
                return await _produtoRepository.ObterProdutosPorNome(nome.Trim());
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível obter o produto.", ex);
            }
        }

    }
}
