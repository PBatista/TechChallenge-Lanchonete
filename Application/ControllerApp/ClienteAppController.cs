using Application.ApplicationDTO;
using Application.IPresenters;
using Application.IUseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ControllerApp
{
    public class ClienteAppController
    {
        private readonly IClienteUseCase _useCase;
        private readonly IClientePresenter _presenter;

        public ClienteAppController(IClienteUseCase useCase, IClientePresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<List<ClienteDTO>> ListarClientes()
        {
            var clientes = await _useCase.ListarClientes();
            return _presenter.PresentMany(clientes);
        }

        public async Task<ClienteDTO> ObterPorCpf(string cpf)
        {
            var cliente = await _useCase.ObterClientePorCpf(cpf);
            return cliente == null ? null : _presenter.Present(cliente);
        }

        public async Task SalvarCliente(ClienteDTO dto)
        {
            await _useCase.SalvarCliente(dto);
        }
    }
}
