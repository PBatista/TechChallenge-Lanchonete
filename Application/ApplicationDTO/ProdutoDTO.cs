using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApplicationDTO
{
    public class ProdutoDTO
    {
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public double Preco { get; set; }
        public string Descricao { get; set; }
        public List<string> Imagens { get; set; }
    }
}
