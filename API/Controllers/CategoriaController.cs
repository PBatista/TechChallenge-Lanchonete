using Application.IUseCase;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        public readonly ILogger<CategoriaController> _logger;
        public readonly ICategoriaUseCase _categoriaUseCase;

        public CategoriaController(ILogger<CategoriaController> logger, ICategoriaUseCase categoriaUseCase)
        {
            _logger = logger;
            _categoriaUseCase = categoriaUseCase;
        }

        [HttpGet]
        public ActionResult<List<Categoria>> Get()
        {
            var categorias = _categoriaUseCase.ListarCategorias();
            return Ok(categorias);
        }
    }
}
