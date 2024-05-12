using Domain.Entities;
using InfraMongoDb.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraMongoDb.Mapper
{
    public static class ClienteMapper
    {
        public static Cliente MapToEntity(ClienteDTO dto)
        {
            return new Cliente(dto.Nome, dto.Cpf, dto.Email, dto.Descricao)
            {
                // Id = dto.Id
            };
        }
    }
}
