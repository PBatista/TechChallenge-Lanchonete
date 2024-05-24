using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApplicationDTO
{
    public class ObterClienteResult
    {
        public Cliente Cliente { get; set; }
        public bool Encontrado { get; set; }

        public ObterClienteResult(Cliente cliente, bool encontrado)
        {
            Cliente = cliente;
            Encontrado = encontrado;
        }
    }

}
