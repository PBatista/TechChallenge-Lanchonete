using Application.ApplicationDTO;
using Application.ControllerApp;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/categorias")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaAppController _appController;
        public readonly ILogger<CategoriaController> _logger;

        public CategoriaController(CategoriaAppController appController, ILogger<CategoriaController> logger)
        {
            _appController = appController;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaDTO>>> Get()
        {
            var result = await _appController.ListarCategorias();
            return Ok(result);
        }

        [HttpGet("{nome}")]
        public async Task<ActionResult<CategoriaDTO>> GetByName(string nome)
        {
            var categoria = await _appController.ObterPorNome(nome.Trim().ToUpper());
            if (categoria == null)
                return NotFound($"Categoria '{nome}' não encontrada.");

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoriaDTO dto)
        {
            await _appController.SalvarCategoria(dto);
            return Ok($"Cadastro da categoria '{dto.Nome}' feito com sucesso.");
        }

        [HttpPut("{nome}")]
        public async Task<ActionResult> Put(string nome, [FromBody] CategoriaDTO dto)
        {
            await _appController.EditarCategoria(nome.Trim().ToUpper(), dto);
            return Ok($"Categoria '{dto.Nome}' editada com sucesso.");
        }

        [HttpDelete("{nome}")]
        public async Task<ActionResult> Delete(string nome)
        {
            await _appController.DeletarCategoria(nome.Trim().ToUpper());
            return Ok($"Categoria '{nome}' deletada com sucesso.");
        }

    }
}
