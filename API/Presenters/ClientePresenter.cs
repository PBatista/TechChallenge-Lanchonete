using Application.ApplicationDTO;
using Application.IPresenters;
using Domain.Entities;

namespace API.Presenters
{
    public class ClientePresenter : IClientePresenter
    {
        public List<ClienteDTO> PresentMany(List<Cliente> clientes)
        {
            return clientes.Select(Present).ToList();
        }

        public ClienteDTO Present(Cliente cliente)
        {
            return new ClienteDTO
            {
                Nome = cliente.Nome,
                Email = cliente.Email,
                Cpf = cliente.Cpf
            };
        }
    }
}
