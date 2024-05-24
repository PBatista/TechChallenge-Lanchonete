using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApplicationDTO
{
    public class ProdutoApplicationDTO
    {
        public ProdutoApplicationDTO(string nome, int quantidade)
        {
            Nome = nome;              
            Quantidade = quantidade;
        }
        public string Nome { get; private set; }        
        public int Quantidade { get; private set; }
    }
}
