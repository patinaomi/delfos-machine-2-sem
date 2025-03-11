using MongoDB.Driver;
using Project.Models;
using Project.Infrastructure.Interfaces;
using Project.Domain;
using Microsoft.Extensions.Options;

namespace Project.Repositories
{
    public class MotivoRecusaRepository : IMotivoRecusaRepository
    {
        private readonly IMongoCollection<MotivoRecusa> _motivoRecusaCollection;

        public MotivoRecusaRepository(IOptions<ConfigMongoDb> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _motivoRecusaCollection = mongoDatabase.GetCollection<MotivoRecusa>(settings.Value.MotivoRecusaCollectionName);
        }

        public async Task<MotivoRecusa> Criar(MotivoRecusa motivoRecusa)
        {
            await _motivoRecusaCollection.InsertOneAsync(motivoRecusa);
            return motivoRecusa;
        }

        public async Task<MotivoRecusa> ConsultarId(string id)
        {
            var filtro = Builders<MotivoRecusa>.Filter.Eq(u => u.Id, id);
            return await _motivoRecusaCollection.Find(filtro).FirstOrDefaultAsync();
        }

        public async Task<List<MotivoRecusa>> ConsultarTodos()
        {
            return await _motivoRecusaCollection.Find(_ => true).ToListAsync();
        }

        public async Task<MotivoRecusa?> Atualizar(MotivoRecusa motivoRecusa)
        {
            var filtro = Builders<MotivoRecusa>.Filter.Eq(u => u.Id, motivoRecusa.Id);

            var update = Builders<MotivoRecusa>.Update
                .Set(u => u.Motivo, motivoRecusa.Motivo)
                .Set(u => u.Descricao, motivoRecusa.Descricao);

            var resultado = await _motivoRecusaCollection.UpdateOneAsync(filtro, update);

            if (resultado.ModifiedCount > 0)
            {
                return motivoRecusa;
            }

            return null;
        }

        public async Task<MotivoRecusa?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            var motivoRecusa = await ConsultarId(id);
            if (motivoRecusa == null)
            {
                return null;
            }

            var updateDefinitionBuilder = Builders<MotivoRecusa>.Update;
            var updateDefinitions = new List<UpdateDefinition<MotivoRecusa>>();

            foreach (var campo in camposParaAtualizar)
            {
                switch (campo.Key.ToLower())
                {
                    case "motivo":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Motivo, campo.Value.ToString()));
                        break;
                    case "descricao":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Descricao, campo.Value.ToString()));
                        break;
                   
                }
            }

            if (updateDefinitions.Count == 0)
            {
                return motivoRecusa;
            }

            var combinedUpdate = updateDefinitionBuilder.Combine(updateDefinitions);
            
            var filtro = Builders<MotivoRecusa>.Filter.Eq(c => c.Id, id);
            
            var resultado = await _motivoRecusaCollection.UpdateOneAsync(filtro, combinedUpdate);

            if (resultado.ModifiedCount > 0)
            {
                return await ConsultarId(id);
            }

            return null;
        }


        public async Task Excluir(string id)
        {
            var filtro = Builders<MotivoRecusa>.Filter.Eq(u => u.Id, id);
            await _motivoRecusaCollection.DeleteOneAsync(filtro);
        }
    }
}
