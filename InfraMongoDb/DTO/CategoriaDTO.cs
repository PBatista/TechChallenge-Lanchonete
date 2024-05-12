using MongoDB.Bson;

namespace InfraMongoDb.DTO
{
    public class CategoriaDTO : BaseDTO
    {
        public CategoriaDTO(ObjectId _id, string nome)
        {
            Id = _id;
            Nome = nome;            
        }
        
        public string Nome { get; private set; }
    }
}
