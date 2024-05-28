namespace Application.ApplicationDTO
{
    public class CheckoutApplicationDTO
    {
        public CheckoutApplicationDTO(string numPedido)
        {
            NumPedido = numPedido;
        }
        public string NumPedido { get; set; }
    }

}