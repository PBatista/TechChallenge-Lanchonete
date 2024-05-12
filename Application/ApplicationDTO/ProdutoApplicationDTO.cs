using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApplicationDTO
{
    public class ProdutoApplicationDTO
    {
        public ProdutoApplicationDTO(string nome, string categoria)
        {
            Nome = nome;
            Categoria = categoria;            
        }
        public string Nome { get; private set; }
        public string Categoria { get; private set; }        
    }
}
