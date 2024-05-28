using Application.IUseCase;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/categorias")]
    public class CategoriaController(ILogger<CategoriaController> logger, ICategoriaUseCase categoriaUseCase) : ControllerBase
    {
        public readonly ILogger<CategoriaController> _logger = logger;
        public readonly ICategoriaUseCase _categoriaUseCase = categoriaUseCase;

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get()
        {
            var categorias = await _categoriaUseCase.ListarCategorias();
            return Ok(categorias);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Categoria categoria)
        {
            await _categoriaUseCase.SalvarCategoria(categoria);
            return Ok($"Cadastro da categoria '{categoria.Nome}' feito com sucesso");
        }

        [HttpPut("{nome}")]
        public async Task<ActionResult> Put(string nome, Categoria categoria)
        {
            Categoria categoriaCadastrada = await _categoriaUseCase.ObterCategoriaPorNome(nome.ToUpper().Trim());
            if (categoriaCadastrada == null)
            {
                return BadRequest($"A categoria '{nome.ToUpper().Trim()}' não está cadastrada, favor escolha uma categoria válida.");
            }
            await _categoriaUseCase.EditarCategoria(nome, categoria);
            return Ok($"Edição da categoria '{categoria.Nome}' feito com sucesso");
        }

        [HttpDelete("{nome}")]
        public async Task<ActionResult> Delete(string nome)
        {
            Categoria categoriaCadastrada = await _categoriaUseCase.ObterCategoriaPorNome(nome.ToUpper().Trim());
            if (categoriaCadastrada == null)
            {
                return BadRequest($"A categoria '{nome.ToUpper().Trim()}' não está cadastrada, favor escolha uma categoria válida.");
            }
            await _categoriaUseCase.DeletarCategoria(nome.ToUpper().Trim());
            return Ok($"Categoria '{nome}' deletada com sucesso");
        }
    }
}
