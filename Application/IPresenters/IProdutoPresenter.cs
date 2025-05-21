using Application.ApplicationDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IPresenters
{
    public interface IProdutoPresenter
    {
        List<ProdutoDTO> PresentMany(List<Produto> produtos);
        ProdutoDTO Present(Produto produto);
    }
}
