using Application.ApplicationDTO;
using Application.IUseCase;
using Domain.Base;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/checkouts")]
    public class CheckoutController(ILogger<CheckoutController> logger, ICheckoutUseCase checkoutUseCase) : ControllerBase
    {
        public readonly ILogger<CheckoutController> _logger = logger;
        private readonly ICheckoutUseCase _checkoutUseCase = checkoutUseCase;

        [HttpPost]
        public async Task<ActionResult> ProcessarPagamento([FromBody] CheckoutApplicationDTO checkoutDTO)
        {
            string numPedido = checkoutDTO.NumPedido;
            await _checkoutUseCase.ProcessarPagamento(numPedido);
            return Ok($"Pagamento finalizado com sucesso. O Pedido num '{numPedido}' teve o status atualizado para '{StatusPedidoEnum.RECEBIDO.GetDescription()}'.");
        }
    }
}
