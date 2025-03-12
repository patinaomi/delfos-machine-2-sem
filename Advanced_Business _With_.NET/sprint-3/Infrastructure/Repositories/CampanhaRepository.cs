using MongoDB.Driver;
using Project.Models;
using Project.Infrastructure.Interfaces;
using Project.Domain;
using Microsoft.Extensions.Options;

namespace Project.Repositories
{
    public class CampanhaRepository : ICampanhaRepository
    {
        private readonly IMongoCollection<Campanha> _campanhaCollection;

        public CampanhaRepository(IOptions<ConfigMongoDb> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _campanhaCollection = mongoDatabase.GetCollection<Campanha>(settings.Value.CampanhaCollectionName);
        }

        public async Task<Campanha> Criar(Campanha campanha)
        {
            await _campanhaCollection.InsertOneAsync(campanha);
            return campanha;
        }

        public async Task<Campanha> ConsultarId(string id)
        {
            var filtro = Builders<Campanha>.Filter.Eq(u => u.Id, id);
            return await _campanhaCollection.Find(filtro).FirstOrDefaultAsync();
        }

        public async Task<List<Campanha>> ConsultarTodos()
        {
            return await _campanhaCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Campanha?> Atualizar(Campanha campanha)
        {
            var filtro = Builders<Campanha>.Filter.Eq(u => u.Id, campanha.Id);

            var update = Builders<Campanha>.Update
                .Set(u => u.Atividade, campanha.Atividade)
                .Set(u => u.Descricao, campanha.Descricao)
                .Set(u => u.Pontuacao, campanha.Pontuacao)
                .Set(u => u.Perfil, campanha.Perfil)
                .Set(u => u.IdUsuario, campanha.IdUsuario)
                .Set(u => u.Status, campanha.Status)
                .Set(u => u.DataCriacao, campanha.DataCriacao)
                .Set(u => u.DataConclusao, campanha.DataConclusao);

            var resultado = await _campanhaCollection.UpdateOneAsync(filtro, update);

            if (resultado.ModifiedCount > 0)
            {
                return campanha;
            }

            return null;
        }

        public async Task<Campanha?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            var campanha = await ConsultarId(id);
            if (campanha == null)
            {
                return null;
            }

            var updateDefinitionBuilder = Builders<Campanha>.Update;
            var updateDefinitions = new List<UpdateDefinition<Campanha>>();

            foreach (var campo in camposParaAtualizar)
            {
                switch (campo.Key.ToLower())
                {
                    case "atividade":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Atividade, campo.Value.ToString()));
                        break;
                    case "descricao":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Descricao, campo.Value.ToString()));
                        break;
                    case "pontuacao":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Pontuacao, campo.Value.ToString()));
                        break;
                    case "perfil":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Perfil, campo.Value.ToString()));
                        break;
                    case "idusuario":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.IdUsuario, campo.Value.ToString()));
                        break;
                    case "status":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Status, campo.Value.ToString()));
                        break;
        
                }
            }

            if (updateDefinitions.Count == 0)
            {
                return campanha;
            }

            var combinedUpdate = updateDefinitionBuilder.Combine(updateDefinitions);
            
            var filtro = Builders<Campanha>.Filter.Eq(c => c.Id, id);

            var resultado = await _campanhaCollection.UpdateOneAsync(filtro, combinedUpdate);

            if (resultado.ModifiedCount > 0)
            {
                return await ConsultarId(id);
            }

            return null;
        }

        public async Task Excluir(string id)
        {
            var filtro = Builders<Campanha>.Filter.Eq(u => u.Id, id);
            await _campanhaCollection.DeleteOneAsync(filtro);
        }
    }
}
