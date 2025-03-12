using MongoDB.Driver;
using Project.Models;
using Project.Infrastructure.Interfaces;
using Project.Domain;
using Microsoft.Extensions.Options;

namespace Project.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IMongoCollection<Feedback> _feedbackCollection;

        public FeedbackRepository(IOptions<ConfigMongoDb> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _feedbackCollection = mongoDatabase.GetCollection<Feedback>(settings.Value.FeedbackCollectionName);
        }

        public async Task<Feedback> Criar(Feedback feedback)
        {
            await _feedbackCollection.InsertOneAsync(feedback);
            return feedback;
        }

        public async Task<Feedback> ConsultarId(string id)
        {
            var filtro = Builders<Feedback>.Filter.Eq(u => u.Id, id);
            return await _feedbackCollection.Find(filtro).FirstOrDefaultAsync();
        }

        public async Task<List<Feedback>> ConsultarTodos()
        {
            return await _feedbackCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Feedback?> Atualizar(Feedback feedback)
        {
            var filtro = Builders<Feedback>.Filter.Eq(u => u.Id, feedback.Id);

            var update = Builders<Feedback>.Update
                .Set(u => u.Nota, feedback.Nota)
                .Set(u => u.Comentario, feedback.Comentario);
 

            var resultado = await _feedbackCollection.UpdateOneAsync(filtro, update);

            if (resultado.ModifiedCount > 0)
            {
                return feedback;
            }

            return null;
        }

        public async Task<Feedback?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            var feedback = await ConsultarId(id);
            if (feedback == null)
            {
                return null;
            }

            var updateDefinitionBuilder = Builders<Feedback>.Update;
            var updateDefinitions = new List<UpdateDefinition<Feedback>>();

            foreach (var campo in camposParaAtualizar)
            {
                switch (campo.Key.ToLower())
                {
                    case "nota":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Nota, campo.Value.ToString()));
                        break;
                    case "comentario":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Comentario, campo.Value.ToString()));
                        break;
    
                }
            }

            if (updateDefinitions.Count == 0)
            {
                return feedback;
            }

            var combinedUpdate = updateDefinitionBuilder.Combine(updateDefinitions);
            
            var filtro = Builders<Feedback>.Filter.Eq(c => c.Id, id);
            
            var resultado = await _feedbackCollection.UpdateOneAsync(filtro, combinedUpdate);

            if (resultado.ModifiedCount > 0)
            {

                return await ConsultarId(id);
            }

            return null;
        }


        public async Task Excluir(string id)
        {
            var filtro = Builders<Feedback>.Filter.Eq(u => u.Id, id);
            await _feedbackCollection.DeleteOneAsync(filtro);
        }
    }
}
