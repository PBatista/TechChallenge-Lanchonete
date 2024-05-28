using MongoDB.Bson;

namespace InfraMongoDb.DTO
{
    public class ClienteDTO : BaseDTO
    {
        public ClienteDTO(ObjectId _id, string nome, string cpf, string email, string descricao)
        {

            Id = _id;            
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Descricao = descricao;
        }
             
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public string Descricao { get; private set; }
    }
}
