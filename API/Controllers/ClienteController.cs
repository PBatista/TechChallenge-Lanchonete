using Application.IUseCase;
using Application.UseCase;
using Domain.Base;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController(ILogger<ClienteController> logger, IClienteUseCase clienteUseCase) : ControllerBase
    {
        public readonly ILogger<ClienteController> _logger = logger;
        public readonly IClienteUseCase _clienteUseCase = clienteUseCase;

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> Get()
        {
            var clientes = await _clienteUseCase.ListarClientes();
            return Ok(clientes);
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<Cliente>> GetByCpf(string cpf)
        {
            var cliente = await _clienteUseCase.ObterClientePorCpf(cpf);            
            if (cliente != null && cliente.Cpf != "") {
                return Ok(cliente);
            }
            else return BadRequest($"Cliente com CPF '{cpf}' não está cadastrado!");                        
        }

        [HttpPost]
        public async Task<ActionResult> Post(Cliente cliente)
        {
            await _clienteUseCase.SalvarCliente(cliente);
            return Ok($"Cadastro do cliente '{cliente.Nome}' foi feito com sucesso");
        }
    }
}
