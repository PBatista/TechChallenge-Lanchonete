using Domain.Entities;
using MercadoPago.Client;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.IService;
using MercadoPago.Resource.Payment;
using System;

namespace MercadoPago.Service
{
    public class MercadoPagoService : IMercadoPagoService
    {       

        public async Task<Pagamento> FakePagamento(Pedido pedido)
        {
            return new Pagamento(pedido.NumPedido, pedido.ValorTotal, "APROVADO", DateTime.Now, "Pix", Guid.NewGuid().ToString(), $"Mock de pagamento do pedido {pedido.NumPedido}");
        }         
    }
}
