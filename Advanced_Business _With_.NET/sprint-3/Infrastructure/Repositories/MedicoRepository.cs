using MongoDB.Driver;
using Project.Models;
using Project.Infrastructure.Interfaces;
using Project.Domain;
using Microsoft.Extensions.Options;

namespace Project.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly IMongoCollection<Medico> _medicoCollection;

        public MedicoRepository(IOptions<ConfigMongoDb> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _medicoCollection = mongoDatabase.GetCollection<Medico>(settings.Value.MedicoCollectionName);
        }

        public async Task<Medico> Criar(Medico medico)
        {
            await _medicoCollection.InsertOneAsync(medico);
            return medico;
        }

        public async Task<Medico> ConsultarId(string id)
        {
            var filtro = Builders<Medico>.Filter.Eq(u => u.Id, id);
            return await _medicoCollection.Find(filtro).FirstOrDefaultAsync();
        }

        public async Task<List<Medico>> ConsultarTodos()
        {
            return await _medicoCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Medico?> Atualizar(Medico medico)
        {
            var filtro = Builders<Medico>.Filter.Eq(u => u.Id, medico.Id);

            var update = Builders<Medico>.Update
                .Set(u => u.Nome, medico.Nome)
                .Set(u => u.CPF, medico.CPF)
                .Set(u => u.Telefone, medico.Telefone)
                .Set(u => u.Email, medico.Email)
                .Set(u => u.Senha, medico.Senha)
                .Set(u => u.Especilidade, medico.Especilidade);

            var resultado = await _medicoCollection.UpdateOneAsync(filtro, update);

            if (resultado.ModifiedCount > 0)
            {
                return medico;
            }

            return null;
        }

        public async Task<Medico?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            var medico = await ConsultarId(id);
            if (medico == null)
            {
                return null;
            }

            var updateDefinitionBuilder = Builders<Medico>.Update;
            var updateDefinitions = new List<UpdateDefinition<Medico>>();

            foreach (var campo in camposParaAtualizar)
            {
                switch (campo.Key.ToLower())
                {
                    case "nome":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Nome, campo.Value.ToString()));
                        break;
                    case "cpf":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.CPF, campo.Value.ToString()));
                        break;
                    case "telefone":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Telefone, campo.Value.ToString()));
                        break;
                    case "email":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Email, campo.Value.ToString()));
                        break;
                    case "senha":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Senha, campo.Value.ToString()));
                        break;
                    case "especilidade":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Especilidade, campo.Value.ToString()));
                        break;
                }
            }

            if (updateDefinitions.Count == 0)
            {
                return medico;
            }

            // Combinar todas as definições de atualização
            var combinedUpdate = updateDefinitionBuilder.Combine(updateDefinitions);
            
            // Filtro para encontrar o documento
            var filtro = Builders<Medico>.Filter.Eq(c => c.Id, id);
            
            // Executar a atualização
            var resultado = await _medicoCollection.UpdateOneAsync(filtro, combinedUpdate);

            // Retornar o Médico atualizado se a operação for bem-sucedida
            if (resultado.ModifiedCount > 0)
            {
                // Buscar o Médico atualizada
                return await ConsultarId(id);
            }

            return null;
        }


        public async Task Excluir(string id)
        {
            var filtro = Builders<Medico>.Filter.Eq(u => u.Id, id);
            await _medicoCollection.DeleteOneAsync(filtro);
        }
    }
}
