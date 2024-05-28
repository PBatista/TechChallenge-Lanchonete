using Domain.Entities;
using InfraMongoDb.DTO;

namespace InfraMongoDb.Mapper
{
    public static class ClienteMapper
    {
        public static Cliente MapToEntity(ClienteDTO dto)
        {
            return new Cliente(dto.Nome, dto.Cpf, dto.Email) { };
        }
    }
}
