using Application.ApplicationDTO;
using Application.IUseCase;
using Domain.Base;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/webhook")]
public class WebhookPagamentoController : ControllerBase
{
    private readonly IPedidoUseCase _pedidoUseCase;
    private readonly ILogger<WebhookPagamentoController> _logger;

    public WebhookPagamentoController(IPedidoUseCase pedidoUseCase, ILogger<WebhookPagamentoController> logger)
    {
        _pedidoUseCase = pedidoUseCase;
        _logger = logger;
    }

    [HttpPost("pagamento")]
    public async Task<IActionResult> ReceberNotificacaoPagamento([FromBody] WebhookPagamentoDTO webhookDto)
    {
        try
        {
            _logger.LogInformation($"Recebido webhook de pagamento para pedido {webhookDto.NumPedido} com status {webhookDto.StatusPagamento}");

            if (webhookDto.StatusPagamento.ToUpper() == "APROVADO")
            {
                await _pedidoUseCase.AtualizarStatus(StatusPedidoEnum.RECEBIDO.GetDescription(), webhookDto.NumPedido);
                return Ok($"Pagamento aprovado. Pedido {webhookDto.NumPedido} atualizado para '{StatusPedidoEnum.RECEBIDO.GetDescription()}'.");
            }
            else if (webhookDto.StatusPagamento.ToUpper() == "RECUSADO")
            {
                await _pedidoUseCase.AtualizarStatus(StatusPedidoEnum.AGUARDANDO_PAGAMENTO.GetDescription(), webhookDto.NumPedido);
                return Ok($"Pagamento recusado. Pedido {webhookDto.NumPedido} continua aguardando pagamento.");
            }
            else
            {
                return BadRequest("Status de pagamento inválido.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao processar webhook: {ex.Message}");
            return StatusCode(500, "Erro interno ao processar webhook.");
        }
    }
}
