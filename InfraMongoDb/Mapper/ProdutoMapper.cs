using Domain.Entities;
using InfraMongoDb.DTO;

namespace InfraMongoDb.Mapper
{
    public static class ProdutoMapper
    {
        public static Produto MapToEntity(ProdutoDTO dto)
        {
            return new Produto(dto.Nome, dto.Categoria, dto.Preco, dto.Descricao, dto.Imagens)
            {
                // Id = dto.Id.ToString()
            };
        }
    }
}
