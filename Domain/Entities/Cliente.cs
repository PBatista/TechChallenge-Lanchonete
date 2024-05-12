using Domain.Base;

namespace Domain.Entities
{
    public class Cliente
    {
        public Cliente (string nome, string cpf, string email, string? descricao)
        {
            Nome = nome; 
            Cpf = cpf; 
            Email = email;
            Descricao = descricao;
        }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public string? Descricao { get; private set; }

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Nome, "O Nome não pode estar vazio");
            AssertionConcern.AssertArgumentNotNull(Cpf, "O CPF não pode estar vazio");
            AssertionConcern.AssertArgumentNotNull(Email, "O E-mail não pode estar vazio");
        }
    }
}
