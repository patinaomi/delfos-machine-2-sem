using MongoDB.Driver;
using Project.Models;
using Project.Infrastructure.Interfaces;
using Project.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace Project.Repositories
{
    public class HorariosRepository : IHorariosRepository
    {
        private readonly IMongoCollection<Horarios> _horarioCollection;

        public HorariosRepository(IOptions<ConfigMongoDb> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

            _horarioCollection = mongoDatabase.GetCollection<Horarios>(settings.Value.HorariosCollectionName);
        }

        public async Task<Horarios> Criar(Horarios horario)
        {
            await _horarioCollection.InsertOneAsync(horario);
            return horario;
        }

        public async Task<Horarios> ConsultarPorUsuarioId(string usuarioId)
        {
            return await _horarioCollection
                        .Find(e => e.IdUsuario == usuarioId)
                        .FirstOrDefaultAsync(); 
        }

        public async Task<List<Horarios>> ConsultarTodos()
        {
            return await _horarioCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Horarios?> Atualizar(Horarios horario)
        {
            var filtro = Builders<Horarios>.Filter.Eq(u => u.Id, horario.Id);

            var update = Builders<Horarios>.Update
                .Set(u => u.HorariosPreferencia, horario.HorariosPreferencia);
                

            var resultado = await _horarioCollection.UpdateOneAsync(filtro, update);

            if (resultado.ModifiedCount > 0)
            {
                return horario;
            }

            return null;
        }

       public async Task Excluir(string id)
        {
            // Convertendo o id string para ObjectId, se necessário
            if (ObjectId.TryParse(id, out var objectId))
            {
                var filtro = Builders<Horarios>.Filter.Eq("_id", objectId);
                await _horarioCollection.DeleteOneAsync(filtro);
            }
            else
            {
                throw new ArgumentException("O id fornecido não é válido.");
            }
        }


    }
}
