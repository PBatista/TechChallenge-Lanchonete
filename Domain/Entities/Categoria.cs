using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Categoria : IAggregateRoot
    {
        public Categoria(string nome)
        {            
            Nome = nome.ToUpper().Trim();
            ValidateEntity();
        }
        
        public string Nome { get; private set; }

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Nome, "O Nome não pode estar vazio");
        }
    }
   
}
