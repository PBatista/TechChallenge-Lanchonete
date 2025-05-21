using Application.ApplicationDTO;
using Application.ControllerApp;
using Application.IUseCase;
using Domain.Base;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/pedidos")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoAppController _appController;
        private readonly INotificaoUseCase _notificationUseCase;

        public PedidoController(PedidoAppController appController, INotificaoUseCase notificationUseCase)
        {
            _appController = appController;
            _notificationUseCase = notificationUseCase;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PedidoApplicationDTO pedido)
        {
            try
            {
                var numero = await _appController.SalvarPedido(pedido);
                return Ok(new
                {
                    mensagem = "Cadastro do pedido feito com sucesso.",
                    numPedido = numero
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<PedidoDTO>>> Get()
        {
            var pedidos = await _appController.ListarPedidos();
            return Ok(pedidos);
        }

        [HttpPatch("{numPedido}/status")]
        public async Task<ActionResult> AtualizarStatus(string numPedido, [FromBody] AtualizarStatusDTO dto)
        {
            try
            {
                var status = dto.Status.Trim().ToUpper();
                await _appController.AtualizarStatus(status, numPedido.Trim());

                if (status == StatusPedidoEnum.PRONTO.GetDescription())
                {
                    await _notificationUseCase.NotificarClientePedidoPronto(numPedido.Trim());
                    return Ok($"Status do pedido '{numPedido}' foi atualizado para '{status}' com sucesso e o cliente foi notificado.");
                }

                return Ok($"Status do pedido '{numPedido}' foi atualizado para '{status}' com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("listar-pedidos-status/{status}")]
        public async Task<ActionResult<List<PedidoDTO>>> ListarPedidosPorStatus(string status)
        {
            try
            {
                var pedidos = await _appController.ListarPedidosPorStatus(status.Trim().ToUpper());
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("listar-pedidos-andamento")]
        public async Task<ActionResult<List<PedidoDTO>>> ListarPedidosEmAndamento()
        {
            var pedidos = await _appController.ListarPedidosEmAndamento();
            return Ok(pedidos);
        }

        [HttpGet("status-pagamento/{numPedido}")]
        public async Task<ActionResult> ConsultarStatusPagamento(string numPedido)
        {
            var pedido = await _appController.ObterPedidoPorNumero(numPedido.Trim());
            if (pedido == null)
                return NotFound(new { mensagem = $"Pedido '{numPedido}' não encontrado." });

            return Ok(new
            {
                numPedido = pedido.NumPedido,
                statusPagamento = pedido.Status
            });
        }
    }
}
