using Application.IUseCase;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutController(ILogger<CheckoutController> logger, ICheckoutUseCase checkoutUseCase) : ControllerBase
    {
        public readonly ILogger<CheckoutController> _logger = logger;
        private readonly ICheckoutUseCase _checkoutUseCase = checkoutUseCase;

        [HttpPost]
        public async Task<ActionResult> ProcessarPagamento(string numPedido)
        {
            await _checkoutUseCase.ProcessarPagamento(numPedido);
            return Ok($"Pagamento finalizado com sucesso. O Pedido num '{numPedido}' teve o status atualizado para 'RECEBIDO'.");
        }
    }
}
