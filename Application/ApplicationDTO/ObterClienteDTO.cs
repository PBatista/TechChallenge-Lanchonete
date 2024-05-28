using Domain.Entities;

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
