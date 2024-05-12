using Domain.Base;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Pedido : IAggregateRoot
    {        
        public Pedido(string? numPedido, List<Produto> produtos, Cliente cliente,double valorTotal, string status, DateTime? dataHora)
        {
            NumPedido = numPedido;
            Produtos = produtos;
            Cliente = cliente;
            ValorTotal = valorTotal;
            Status = status;
            DataHora = dataHora;
        }
        
        public string? NumPedido { get; private set; }
        
        public Cliente Cliente { get; private set; }
        
        public List<Produto> Produtos { get; private set; }

        public double ValorTotal { get; private set; }

        public string Status { get; private set; }

        public DateTime? DataHora { get; private set; }

    }
}
