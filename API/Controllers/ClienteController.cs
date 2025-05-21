using Application.ApplicationDTO;
using Application.ControllerApp;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteAppController _appController;

        public ClienteController(ClienteAppController appController)
        {
            _appController = appController;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteDTO>>> Get()
        {
            var clientes = await _appController.ListarClientes();
            return Ok(clientes);
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<ClienteDTO>> GetByCpf(string cpf)
        {
            var cliente = await _appController.ObterPorCpf(cpf);
            if (cliente == null)
                return NotFound($"Cliente com CPF '{cpf}' não está cadastrado.");

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClienteDTO dto)
        {
            try
            {
                await _appController.SalvarCliente(dto);
                return Ok($"Cadastro do cliente '{dto.Nome}' foi feito com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
