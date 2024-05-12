using Application.IUseCase;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        public readonly ILogger<ProdutoController> _logger;
        public readonly IProdutoUseCase _produtoUseCase;
        public readonly ICategoriaUseCase _categoriaUseCase;

        public ProdutoController(ILogger<ProdutoController> logger, IProdutoUseCase produtoUseCase, ICategoriaUseCase categoriaUseCase)
        {
            _logger = logger;
            _produtoUseCase = produtoUseCase;
            _categoriaUseCase = categoriaUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> Get()
        {
            var produtos = await _produtoUseCase.ListarProdutos();
            return Ok(produtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Produto produto)
        {
            Produto produtoCadastrado = await _produtoUseCase.ObterProdutoPorNome(produto.Nome);
            if (produtoCadastrado != null)
            {
                return BadRequest($"O produto '{produto.Nome.Trim()}' já está cadastrado");
            }
            else if (!_categoriaUseCase.ValidarCategoria(produto.Categoria))
            {
                return BadRequest($"A categoria '{produto.Categoria.Trim()}' não existe na nossa base de dados, favor escolha uma categoria válida");
            }
            await _produtoUseCase.SalvarProduto(produto);
            return Ok($"Cadastro do produto '{produto.Nome}' foi feito com sucesso");

        }

        [HttpPut("{nome}")]
        public async Task<IActionResult> Put(string nome, Produto produto)
        {
            Produto produtoCadastrado = await _produtoUseCase.ObterProdutoPorNome(nome);
            if (produtoCadastrado == null)
            {
                return BadRequest($"O produto '{nome.Trim()}' não está cadastrado, favor escolha um produto válido.");
            }
            await _produtoUseCase.EditarProduto(nome, produto);
            return Ok($"Produto '{nome}' foi editado com sucesso");
        }

        [HttpDelete("{nome}")]
        public async Task<IActionResult> Delete(string nome)
        {
            await _produtoUseCase.DeletarProduto(nome);
            return Ok($"Produto '{nome}' deletado com sucesso");
        }

        [HttpGet("categoria/{categoria}")]
        public async Task<IActionResult> ObterProdutosPorCategoria(string categoria)
        {
            if (_categoriaUseCase.ValidarCategoria(categoria))
            {
                var produtos = await _produtoUseCase.ObterProdutosPorCategoria(categoria);
                if (produtos != null && produtos.Count > 0)
                {
                    return Ok(produtos);
                }
                else
                {
                    return BadRequest($"Não existe produtos com a categoriao {categoria.Trim()}");
                }
            }
            else return BadRequest($"A categoria {categoria.Trim()} não existe");
        }
    }
}
