using Domain.Entities;

namespace Application.IUseCase
{
    public interface IClienteUseCase 
    {
        Task<List<Cliente>> ListarClientes();

        Task<Cliente> ObterClientePorCpf(string cpf);

        Task SalvarCliente(Cliente cliente);
    }
}
