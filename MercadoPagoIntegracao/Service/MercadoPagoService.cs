using Domain.Entities;
using MercadoPago.Client;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.IService;
using MercadoPago.Resource.Payment;

namespace MercadoPago.Service
{
    public class MercadoPagoService : IMercadoPagoService
    {       

        public async Task<Pagamento> FakePagamento(Pedido pedido)
        {
            return new Pagamento(pedido.NumPedido, 1, "Aprovado", new DateTime(), "Pix", "555", "Mock de pagamento");
        }

        public async Task<Payment> CriarPagamento(string descricao, double valor)
        {
            MercadoPagoConfig.AccessToken = "";
            var requestOptions = new RequestOptions();
            requestOptions.CustomHeaders.Add("x-idempotency-key", "<SOME_UNIQUE_VALUE>");

            PaymentCreateRequest request = new PaymentCreateRequest
            {
                TransactionAmount = Convert.ToDecimal(valor),
                Token = "CARD_TOKEN",
                Description = descricao,
                Installments = 1,
                PaymentMethodId = "visa",
                Payer = new PaymentPayerRequest
                {
                    Email = "contato.plbatista@gmail.com",
                }
            };


            var client = new PaymentClient();
            Payment payment = await client.CreateAsync(request);

            Console.WriteLine($"Payment ID: {payment.Id}");

            Console.WriteLine("Pagamento criado com sucesso. ID do pagamento: " + payment.Id);

            return await Task.FromResult(payment);

        }        
    }
}
