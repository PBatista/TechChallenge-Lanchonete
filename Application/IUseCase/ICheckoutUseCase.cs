
namespace Application.IUseCase
{
    public interface ICheckoutUseCase
    {
        Task ProcessarPagamento(string NumPedido);

    }
}
