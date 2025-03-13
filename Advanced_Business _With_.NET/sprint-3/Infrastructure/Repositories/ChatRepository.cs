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

        public async Task<Chat?> Atualizar(Chat chat)
        {
            var filtro = Builders<Chat>.Filter.Eq(u => u.Id, chat.Id);

            var update = Builders<Chat>.Update
                .Set(u => u.IdUsuario, chat.IdUsuario)
                .Set(u => u.Pergunta, chat.Pergunta)
                .Set(u => u.Resposta, chat.Resposta)
                .Set(u => u.Data, chat.Data);

            var resultado = await _chatCollection.UpdateOneAsync(filtro, update);

            if (resultado.ModifiedCount > 0)
            {
                return chat;
            }

            return null;
        }

        public async Task<Chat?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            var chat = await ConsultarId(id);
            if (chat == null)
            {
                return null;
            }

            var updateDefinitionBuilder = Builders<Chat>.Update;
            var updateDefinitions = new List<UpdateDefinition<Chat>>();

            foreach (var campo in camposParaAtualizar)
            {
                switch (campo.Key.ToLower())
                {
                    case "idusuario":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.IdUsuario, campo.Value.ToString()));
                        break;
                    case "pergunta":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Pergunta, campo.Value.ToString()));
                        break;
                    case "resposta":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Resposta, campo.Value.ToString()));
                        break;
                }
            }

            if (updateDefinitions.Count == 0)
            {
                return chat;
            }

            var combinedUpdate = updateDefinitionBuilder.Combine(updateDefinitions);
            
            var filtro = Builders<Chat>.Filter.Eq(c => c.Id, id);
            
            var resultado = await _chatCollection.UpdateOneAsync(filtro, combinedUpdate);

            if (resultado.ModifiedCount > 0)
            {

                return await ConsultarId(id);
            }

            return null;
        }


        public async Task Excluir(string id)
        {
            var filtro = Builders<Chat>.Filter.Eq(u => u.Id, id);
            await _chatCollection.DeleteOneAsync(filtro);
        }
    }
}
