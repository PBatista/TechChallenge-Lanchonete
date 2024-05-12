namespace Domain.Entities
{
    public class Pagamento
    {
        public Pagamento(string numPedido, double valorTotal, string statusPagamento, DateTime dataHoraPagamento, string tipoPagamento, string idTransacao, string detalhes)
        {
            NumPedido = numPedido;
            ValorTotal = valorTotal;
            StatusPagamento = statusPagamento;
            DataHoraPagamento = dataHoraPagamento;
            TipoPagamento = tipoPagamento;
            IdTransacao = idTransacao;
            Detalhes = detalhes;
        }

        public string NumPedido { get; private set; }
        public double ValorTotal { get; private set; }
        public string StatusPagamento { get; private set; }
        public DateTime DataHoraPagamento { get; private set; }
        public string TipoPagamento { get; private set; }
        public string IdTransacao { get; private set; }
        public string Detalhes { get; private set; }
    }
}
