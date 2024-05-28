using Domain.Entities;
using MongoDB.Bson;

namespace InfraMongoDb.DTO
{
    public class PedidoDTO : BaseDTO
    {

        public PedidoDTO(ObjectId _id, string numPedido, Cliente cliente, List<Produto> produtos, double valorTotal, string status, string descricao, DateTime dataHora)
        {
            Id = _id;
            NumPedido = numPedido;
            Cliente = cliente;
            Produtos = produtos;
            ValorTotal = valorTotal;
            Status = status;
            Descricao = descricao;
            DataHora = dataHora;
        }

        public string NumPedido { get; set; }
        public Cliente Cliente { get; private set; }
        public List<Produto> Produtos { get; private set; }
        public double ValorTotal { get; private set; }
        public string Descricao { get; private set; }
        public string Status { get; private set; }
        public DateTime DataHora { get; private set; }
    }
}
