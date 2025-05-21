using Application.ApplicationDTO;
using Application.ControllerApp;
using Application.IUseCase;
using Domain.Base;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/checkouts")]
    public class CheckoutController : ControllerBase
    {
        private readonly CheckoutAppController _appController;

        public CheckoutController(CheckoutAppController appController)
        {
            _appController = appController;
        }

        [HttpPost]
        public async Task<ActionResult> ProcessarPagamento([FromBody] CheckoutApplicationDTO dto)
        {
            try
            {
                await _appController.ProcessarPagamento(dto.NumPedido);
                return Ok($"Pagamento finalizado com sucesso. O Pedido '{dto.NumPedido}' teve o status atualizado para '{StatusPedidoEnum.RECEBIDO.GetDescription()}'.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
