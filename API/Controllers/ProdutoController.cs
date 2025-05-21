using Application.ApplicationDTO;
using Application.ControllerApp;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/produtos")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoAppController _appController;
        public readonly ILogger<ProdutoController> _logger;

        public ProdutoController(ProdutoAppController appController, ILogger<ProdutoController> logger)
        {
            _appController = appController;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProdutoDTO>>> Get()
        {
            var produtos = await _appController.ListarProdutos();
            return Ok(produtos);
        }

        [HttpGet("{nome}")]
        public async Task<ActionResult<ProdutoDTO>> GetByName(string nome)
        {
            var produto = await _appController.ObterProdutoPorNome(nome.Trim());
            if (produto == null)
                return NotFound($"O produto '{nome}' não foi encontrado.");
            return Ok(produto);
        }

        [HttpGet("categoria/{categoria}")]
        public async Task<ActionResult<List<ProdutoDTO>>> GetByCategoria(string categoria)
        {
            try
            {
                var produtos = await _appController.ObterProdutosPorCategoria(categoria.Trim());
                if (produtos == null || produtos.Count == 0)
                    return NotFound($"Não existem produtos cadastrados com a categoria '{categoria}'.");
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProdutoDTO dto)
        {
            try
            {
                await _appController.SalvarProduto(dto);
                return Ok($"Cadastro do produto '{dto.Nome}' foi feito com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{nome}")]
        public async Task<ActionResult> Put(string nome, [FromBody] ProdutoDTO dto)
        {
            try
            {
                await _appController.EditarProduto(nome.Trim(), dto);
                return Ok($"Produto '{nome}' editado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{nome}")]
        public async Task<ActionResult> Delete(string nome)
        {
            try
            {
                await _appController.DeletarProduto(nome.Trim());
                return Ok($"Produto '{nome}' deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
