using MongoDB.Driver;
using Project.Models;
using Project.Infrastructure.Interfaces;
using Project.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace Project.Repositories
{
    public class TurnoRepository : ITurnoRepository
    {
        private readonly IMongoCollection<Turno> _turnoCollection;

        public TurnoRepository(IOptions<ConfigMongoDb> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

            _turnoCollection = mongoDatabase.GetCollection<Turno>(settings.Value.TurnoCollectionName);
        }

        public async Task<Turno> Criar(Turno turno)
        {
            await _turnoCollection.InsertOneAsync(turno);
            return turno;
        }

        public async Task<Turno> ConsultarPorUsuarioId(string usuarioId)
        {
            return await _turnoCollection
                        .Find(e => e.IdUsuario == usuarioId)
                        .FirstOrDefaultAsync(); 
        }

        public async Task<List<Turno>> ConsultarTodos()
        {
            return await _turnoCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Turno?> Atualizar(Turno turno)
        {
            var filtro = Builders<Turno>.Filter.Eq(u => u.Id, turno.Id);

            var update = Builders<Turno>.Update
                .Set(u => u.TurnoPreferencia, turno.TurnoPreferencia);
                

            var resultado = await _turnoCollection.UpdateOneAsync(filtro, update);

            if (resultado.ModifiedCount > 0)
            {
                return turno;
            }

            return null;
        }

       public async Task Excluir(string id)
        {
            // Convertendo o id string para ObjectId, se necessário
            if (ObjectId.TryParse(id, out var objectId))
            {
                var filtro = Builders<Turno>.Filter.Eq("_id", objectId);
                await _turnoCollection.DeleteOneAsync(filtro);
            }
            else
            {
                throw new ArgumentException("O id fornecido não é válido.");
            }
        }


    }
}
