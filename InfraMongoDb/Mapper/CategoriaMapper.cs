using Domain.Entities;
using InfraMongoDb.DTO;

namespace InfraMongoDb.Mapper
{
    public static class CategoriaMapper
    {
        public static Categoria MapToEntity(CategoriaDTO dto)
        {
            return new Categoria(dto.Nome)
            {
                // Id = dto.Id
            };
        }
    }
}
