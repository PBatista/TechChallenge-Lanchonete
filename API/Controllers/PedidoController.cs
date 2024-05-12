using Application.ApplicationDTO;
using Application.IUseCase;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        public readonly ILogger<PedidoController> _logger;
        public readonly IPedidoUseCase _pedidoUseCase;
        public readonly INotificaoUseCase _notificationUseCase;

        public PedidoController(ILogger<PedidoController> logger, IPedidoUseCase pedidoUseCase, INotificaoUseCase notificationUseCase)
        {
            _logger = logger;
            _pedidoUseCase = pedidoUseCase;
            _notificationUseCase = notificationUseCase;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PedidoApplicationDTO pedido)
        {
            try
            {
                var resultado = await _pedidoUseCase.SalvarPedido(pedido);
                return Ok($"Cadastro do pedido feito com sucesso. Num do pedido: {resultado}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> Get()
        {
            var pedidos = await _pedidoUseCase.ListarPedidos();
            return Ok(pedidos);
        }

        [HttpPost("AtualizarStatus")]
        public async Task<ActionResult> AtualizarStatus(string status, string numPedido)
        {
            await _pedidoUseCase.AtualizarStatus(status, numPedido);

            if (status == "Pronto")
            {
                await _notificationUseCase.NotificarClientePedidoPronto(numPedido);
                return Ok($"Status do pedido: {numPedido} foi atualizado para: {status} com sucesso e o cliente foi notificado");
            }
            return Ok($"Status do pedido: {numPedido} foi atualizado para: {status} com sucesso");
            
        }

        [HttpGet("ListarPedidosEmAndamento")]
        public async Task<ActionResult> ListarPedidosEmAndamento()
        {
            var pedidos = await _pedidoUseCase.ListarPedidosEmAndamento();
            return Ok(pedidos);
        }
    }
}
