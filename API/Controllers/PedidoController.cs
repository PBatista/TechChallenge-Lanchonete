using Application.ApplicationDTO;
using Application.IUseCase;
using Domain.Base;
using Domain.Entities;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/pedidos")]
    public class PedidoController(ILogger<PedidoController> logger, IPedidoUseCase pedidoUseCase, INotificaoUseCase notificationUseCase) : ControllerBase
    {
        public readonly ILogger<PedidoController> _logger = logger;
        public readonly IPedidoUseCase _pedidoUseCase = pedidoUseCase;
        public readonly INotificaoUseCase _notificationUseCase = notificationUseCase;

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PedidoApplicationDTO pedido)
        {
            try
            {
                var resultado = await _pedidoUseCase.SalvarPedido(pedido);
                return Ok($"Cadastro do pedido feito com sucesso. Num do pedido '{resultado}'");
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

        [HttpPatch("{numPedido}/status")]
        public async Task<ActionResult> AtualizarStatus(string numPedido, [FromBody]AtualizarStatusDTO atualizarStatusDto)
        {
            string status = atualizarStatusDto.Status.Trim().ToUpper();
            if (await _pedidoUseCase.ValidarStatusPedido(status, numPedido)) await _pedidoUseCase.AtualizarStatus(status, numPedido.Trim());

            if (atualizarStatusDto.Equals(StatusPedidoEnum.PRONTO.GetDescription()))
            {
                await _notificationUseCase.NotificarClientePedidoPronto(numPedido.Trim());
                return Ok($"Status do pedido '{numPedido}' foi atualizado para '{status}' com sucesso e o cliente foi notificado");
            }
            return Ok($"Status do pedido '{numPedido}' foi atualizado para '{status}' com sucesso");
        }

        [HttpGet("listar-pedidos-status/{status}")]
        public async Task<ActionResult> ListarPedidosPorStatus(string status)
        {
            var pedidos = await _pedidoUseCase.ListarPedidosPorStatus(status.ToUpper());
            return Ok(pedidos);
        }

        [HttpGet("listar-pedidos-andamento")]
        public async Task<ActionResult> ListarPedidosEmAndamento()
        {
            var pedidos = await _pedidoUseCase.ListarPedidosEmAndamento();
            return Ok(pedidos);
        }
    }
}
