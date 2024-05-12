using Domain.Entities;
using InfraMongoDb.DTO;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraMongoDb.Mapper
{
    public static class PedidoMapper
    {
        public static PedidoDTO MapToDTO(Pedido entitie)
        {
            var valorTotal = entitie.Produtos.Sum(x => x.Preco);                        

            return new PedidoDTO(ObjectId.GenerateNewId(), entitie.NumPedido, entitie.Cliente, entitie.Produtos, valorTotal, "", DateTime.Now);
        }

        public static Pedido MapToEntity(PedidoDTO dto)
        {                                 
            return new Pedido(dto.NumPedido,dto.Produtos, dto.Cliente,dto.ValorTotal, dto.Status, dto.DataHora) { };
        }
    }
}
