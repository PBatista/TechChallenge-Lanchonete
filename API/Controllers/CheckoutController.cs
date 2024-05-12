using Application.IUseCase;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutController : ControllerBase
    {
        public readonly ILogger<CheckoutController> _logger;
        private readonly ICheckoutUseCase _checkoutUseCase;
        public CheckoutController(ILogger<CheckoutController> logger, ICheckoutUseCase checkoutUseCase)
        {
            _logger = logger;
            _checkoutUseCase = checkoutUseCase;
        }

        [HttpPost]
        public async Task<ActionResult> ProcessarPagamento(string numPedido)
        {
            await _checkoutUseCase.ProcessarPagamento(numPedido);
            return Ok("Pagamento finalizado com sucesso! Pedido teve o status atualizado para 'Recebido'.");
        }
    }
}
