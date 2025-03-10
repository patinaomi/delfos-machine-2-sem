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

        public async Task<Endereco> ConsultarId(string id)
        {
            var filtro = Builders<Endereco>.Filter.Eq(u => u.Id, id);
            return await _enderecoCollection.Find(filtro).FirstOrDefaultAsync();
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

        public async Task<Endereco?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            var endereco = await ConsultarId(id);
            if (endereco == null)
            {
                return null;
            }

            var updateDefinitionBuilder = Builders<Endereco>.Update;
            var updateDefinitions = new List<UpdateDefinition<Endereco>>();

            foreach (var campo in camposParaAtualizar)
            {
                switch (campo.Key.ToLower())
                {
                    case "cep":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.CEP, campo.Value.ToString()));
                        break;
                    case "estado":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Estado, campo.Value.ToString()));
                        break;
                    case "cidade":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Cidade, campo.Value.ToString()));
                        break;
                    case "bairro":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Bairro, campo.Value.ToString()));
                        break;
                    case "rua":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Rua, campo.Value.ToString()));
                        break;
                }
            }

            if (updateDefinitions.Count == 0)
            {
                return endereco;
            }

            // Combinar todas as definições de atualização
            var combinedUpdate = updateDefinitionBuilder.Combine(updateDefinitions);
            
            // Filtro para encontrar o documento
            var filtro = Builders<Endereco>.Filter.Eq(c => c.Id, id);
            
            // Executar a atualização
            var resultado = await _enderecoCollection.UpdateOneAsync(filtro, combinedUpdate);

            // Retornar o endereço atualizado se a operação for bem-sucedida
            if (resultado.ModifiedCount > 0)
            {
                // Buscar o endereço atualizada
                return await ConsultarId(id);
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
