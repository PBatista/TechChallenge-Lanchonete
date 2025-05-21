using Application.IUseCase;

namespace Application.ControllerApp
{
    public class CheckoutAppController
    {
        private readonly ICheckoutUseCase _checkoutUseCase;

        public CheckoutAppController(ICheckoutUseCase checkoutUseCase)
        {
            _checkoutUseCase = checkoutUseCase;
        }

        public async Task ProcessarPagamento(string numPedido)
        {
            await _checkoutUseCase.ProcessarPagamento(numPedido);
        }
    }
}
