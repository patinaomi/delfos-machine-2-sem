using MongoDB.Driver;
using Project.Models;
using Project.Infrastructure.Interfaces;
using Project.Domain;
using Microsoft.Extensions.Options;

namespace Project.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly IMongoCollection<Chat> _chatCollection;

        public ChatRepository(IOptions<ConfigMongoDb> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _chatCollection = mongoDatabase.GetCollection<Chat>(settings.Value.ChatCollectionName);
        }

        public async Task<Chat> Criar(Chat chat)
        {
            await _chatCollection.InsertOneAsync(chat);
            return chat;
        }

        public async Task<Chat> ConsultarId(string id)
        {
            var filtro = Builders<Chat>.Filter.Eq(u => u.Id, id);
            return await _chatCollection.Find(filtro).FirstOrDefaultAsync();
        }

        public async Task<List<Chat>> ConsultarTodos()
        {
            return await _chatCollection.Find(_ => true).ToListAsync();
        }

    }
}
