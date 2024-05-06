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
        public Categoria(int id, string nome)
        {

            Id = id;
            Nome = nome;           
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
    }
}
