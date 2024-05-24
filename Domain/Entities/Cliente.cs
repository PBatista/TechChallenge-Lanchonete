using Domain.Base;
using System.Text.RegularExpressions;

namespace Domain.Entities
{
    public class Cliente
    {
        public Cliente (string nome, string cpf, string email)
        {
            Nome = nome; 
            Cpf = cpf; 
            Email = email;            
            ValidateEntity();
        }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }        

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Nome, "O Nome não pode estar vazio");
            AssertionConcern.AssertArgumentNotEmpty(Cpf, "O CPF não pode estar vazio");            
            // AssertionConcern.IsValidCPF(Cpf, "O CPF não é válido");
            AssertionConcern.AssertArgumentNotEmpty(Email, "O e-mail não pode estar vazio");
            AssertionConcern.IsValidEmail(Email, "O e-mail é inválido");            
        }                
    }
}
