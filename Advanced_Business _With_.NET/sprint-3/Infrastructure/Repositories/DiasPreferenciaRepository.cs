using MongoDB.Driver;
using Project.Models;
using Project.Infrastructure.Interfaces;
using Project.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace Project.Repositories
{
    public class DiasPreferenciaRepository : IDiasPreferenciaRepository
    {
        private readonly IMongoCollection<DiasPreferencia> _diasPreferenciaCollection;

        public DiasPreferenciaRepository(IOptions<ConfigMongoDb> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

            _diasPreferenciaCollection = mongoDatabase.GetCollection<DiasPreferencia>(settings.Value.DiasPreferenciaCollectionName);
        }

        public async Task<DiasPreferencia> Criar(DiasPreferencia dias)
        {
            await _diasPreferenciaCollection.InsertOneAsync(dias);
            return dias;
        }

        public async Task<DiasPreferencia> ConsultarPorUsuarioId(string usuarioId)
        {
            return await _diasPreferenciaCollection
                        .Find(e => e.IdUsuario == usuarioId)
                        .FirstOrDefaultAsync(); 
        }

        public async Task<List<DiasPreferencia>> ConsultarTodos()
        {
            return await _diasPreferenciaCollection.Find(_ => true).ToListAsync();
        }

        public async Task<DiasPreferencia?> Atualizar(DiasPreferencia dias)
        {
            var filtro = Builders<DiasPreferencia>.Filter.Eq(u => u.Id, dias.Id);

            var update = Builders<DiasPreferencia>.Update
                .Set(u => u.DiasSemana, dias.DiasSemana);
                

            var resultado = await _diasPreferenciaCollection.UpdateOneAsync(filtro, update);

            if (resultado.ModifiedCount > 0)
            {
                return dias;
            }

            return null;
        }

       public async Task Excluir(string id)
        {
            // Convertendo o id string para ObjectId, se necessário
            if (ObjectId.TryParse(id, out var objectId))
            {
                var filtro = Builders<DiasPreferencia>.Filter.Eq("_id", objectId);
                await _diasPreferenciaCollection.DeleteOneAsync(filtro);
            }
            else
            {
                throw new ArgumentException("O id fornecido não é válido.");
            }
        }


    }
}
