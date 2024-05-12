using Domain.Entities;
using Domain.Repositories;
using InfraMongoDb.DTO;
using InfraMongoDb.Mapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace InfraMongoDb.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public List<Categoria> ListarCategorias()
        {
            List<Categoria> categorias =
            [
                new Categoria("Lanche"),
                new Categoria("Bebida"),
                new Categoria("Acompanhamento"),
                new Categoria("Sobremesa"),
            ];

            return categorias;
        }
    }
}
