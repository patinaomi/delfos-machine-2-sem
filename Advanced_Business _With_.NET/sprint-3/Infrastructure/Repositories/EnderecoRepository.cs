using MongoDB.Driver;
using Project.Models;
using Project.Infrastructure.Interfaces;
using Project.Domain;
using Microsoft.Extensions.Options;

namespace Project.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly IMongoCollection<Endereco> _enderecoCollection;

        public EnderecoRepository(IOptions<ConfigMongoDb> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

            _enderecoCollection = mongoDatabase.GetCollection<Endereco>(settings.Value.EnderecoCollectionName);
        }

        public async Task<Endereco> Criar(Endereco endereco)
        {
            await _enderecoCollection.InsertOneAsync(endereco);
            return endereco;
        }

        public async Task<Endereco> ConsultarPorUsuarioId(string usuarioId)
        {
            return await _enderecoCollection
                        .Find(e => e.IdUsuario == usuarioId)
                        .FirstOrDefaultAsync(); 
        }

        public async Task<List<Endereco>> ConsultarTodos()
        {
            return await _enderecoCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Endereco?> Atualizar(Endereco endereco)
        {
            var filtro = Builders<Endereco>.Filter.Eq(u => u.Id, endereco.Id);

            var update = Builders<Endereco>.Update
                .Set(u => u.CEP, endereco.CEP)
                .Set(u => u.Estado, endereco.Estado)
                .Set(u => u.Cidade, endereco.Cidade)
                .Set(u => u.Bairro, endereco.Bairro)
                .Set(u => u.Rua, endereco.Rua);

            var resultado = await _enderecoCollection.UpdateOneAsync(filtro, update);

            if (resultado.ModifiedCount > 0)
            {
                return endereco;
            }

            return null;
        }


        public async Task Excluir(string id)
        {
            var filtro = Builders<Endereco>.Filter.Eq(u => u.Id, id);
            await _enderecoCollection.DeleteOneAsync(filtro);
        }
    }
}
