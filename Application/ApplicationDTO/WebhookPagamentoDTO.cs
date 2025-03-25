namespace Application.ApplicationDTO
{
    public class WebhookPagamentoDTO
    {
        public WebhookPagamentoDTO(string numPedido, string statusPagamento)
        {
            NumPedido = numPedido;
            StatusPagamento = statusPagamento;
        }

        public string NumPedido { get; set; }
        public string StatusPagamento { get; set; }
    }
}
