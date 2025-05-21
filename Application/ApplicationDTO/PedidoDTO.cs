using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApplicationDTO
{
    public class PedidoDTO
    {
        public string NumPedido { get; set; }
        public Cliente Cliente { get; set; }
        public List<ProdutoDTO> Produtos { get; set; }
        public double ValorTotal { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public DateTime? DataHora { get; set; }
    }
}
