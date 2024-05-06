﻿using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ListarProdutos();

        Task<IEnumerable<Produto>> ObterProdutosPorCategoria(int categoria_id);

        Task SalvarProduto(Produto produto);

        Task EditarProduto(Produto produto);

        Task DeletarProduto(int id);
    }
}
