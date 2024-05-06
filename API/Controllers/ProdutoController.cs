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

       public ProdutoController(ILogger<ProdutoController> logger, IProdutoUseCase produtoUseCase)
       {
            _logger = logger;
            _produtoUseCase = produtoUseCase;
       }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            var produtos = await _produtoUseCase.ListarProdutos();
            return Ok(produtos);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Produto produto)
        {
            await _produtoUseCase.SalvarProduto(produto);
            return Ok("Cadastro do produto feito com sucesso");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }
            try
            {
                await _produtoUseCase.EditarProduto(produto);
                return Ok("Produto editar com sucesso");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Lidar com exceções de concorrência
                return NotFound();
            }            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _produtoUseCase.DeletarProduto(id);
            return Ok("Produto deletado com sucesso"); ;
        }

        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<Produto>>> ObterProdutosPorCategoria(int categoriaId)
        {
            var produtos = await _produtoUseCase.ObterProdutosPorCategoria(categoriaId);
            return Ok(produtos);
        }
    }
}
