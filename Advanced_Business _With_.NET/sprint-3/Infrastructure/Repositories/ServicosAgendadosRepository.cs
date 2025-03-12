using MongoDB.Driver;
using Project.Models;
using Project.Infrastructure.Interfaces;
using Project.Domain;
using Microsoft.Extensions.Options;

namespace Project.Repositories
{
    public class ServicosAgendadosRepository : IServicosAgendadosRepository
    {
        private readonly IMongoCollection<ServicosAgendados> _servicoCollection;

        public ServicosAgendadosRepository(IOptions<ConfigMongoDb> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _servicoCollection = mongoDatabase.GetCollection<ServicosAgendados>(settings.Value.ServicosAgendadosCollectionName);
        }

        public async Task<ServicosAgendados> Criar(ServicosAgendados servico)
        {
            await _servicoCollection.InsertOneAsync(servico);
            return servico;
        }

        public async Task<ServicosAgendados> ConsultarId(string id)
        {
            var filtro = Builders<ServicosAgendados>.Filter.Eq(u => u.Id, id);
            return await _servicoCollection.Find(filtro).FirstOrDefaultAsync();
        }

        public async Task<List<ServicosAgendados>> ConsultarTodos()
        {
            return await _servicoCollection.Find(_ => true).ToListAsync();
        }

        public async Task<ServicosAgendados?> Atualizar(ServicosAgendados servico)
        {
            var filtro = Builders<ServicosAgendados>.Filter.Eq(u => u.Id, servico.Id);

            var update = Builders<ServicosAgendados>.Update
                .Set(u => u.IdUsuario, servico.IdUsuario)
                .Set(u => u.NomeCliente, servico.NomeCliente)
                .Set(u => u.Data, servico.Data)
                .Set(u => u.Turno, servico.Turno)
                .Set(u => u.Horario, servico.Horario)
                .Set(u => u.IdClinica, servico.IdClinica)
                .Set(u => u.NomeClinica, servico.NomeClinica)
                .Set(u => u.NomeMedico, servico.NomeMedico)
                .Set(u => u.Especialidade, servico.Especialidade)
                .Set(u => u.CEPClinica, servico.CEPClinica)
                .Set(u => u.EstadoClinica, servico.EstadoClinica)
                .Set(u => u.CidadeClinica, servico.CidadeClinica)
                .Set(u => u.BairroClinica, servico.BairroClinica)
                .Set(u => u.RuaClinica, servico.RuaClinica);

            var resultado = await _servicoCollection.UpdateOneAsync(filtro, update);

            if (resultado.ModifiedCount > 0)
            {
                return servico;
            }

            return null;
        }

        public async Task<ServicosAgendados?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            var servico = await ConsultarId(id);
            if (servico == null)
            {
                return null;
            }

            var updateDefinitionBuilder = Builders<ServicosAgendados>.Update;
            var updateDefinitions = new List<UpdateDefinition<ServicosAgendados>>();

            foreach (var campo in camposParaAtualizar)
            {
                switch (campo.Key.ToLower())
                {
                    case "idusuario":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.IdUsuario, campo.Value.ToString()));
                        break;
                    case "nomecliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.NomeCliente, campo.Value.ToString()));
                        break;
                    case "turno":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Turno, campo.Value.ToString()));
                        break;
                    case "horario":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Horario, campo.Value.ToString()));
                        break;
                    case "idclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.IdClinica, campo.Value.ToString()));
                        break;
                    case "nomeclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.NomeClinica, campo.Value.ToString()));
                        break;
                    case "nomemedico":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.NomeMedico, campo.Value.ToString()));
                        break;
                    case "cepclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.CEPClinica, campo.Value.ToString()));
                        break;
                    case "estadoclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.EstadoClinica, campo.Value.ToString()));
                        break;
                    case "cidadeclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.CidadeClinica, campo.Value.ToString()));
                        break;
                    case "bairroclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.BairroClinica, campo.Value.ToString()));
                        break;
                    case "ruaclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.RuaClinica, campo.Value.ToString()));
                        break;
                    
                }
            }

            if (updateDefinitions.Count == 0)
            {
                return servico;
            }

            // Combinar todas as definições de atualização
            var combinedUpdate = updateDefinitionBuilder.Combine(updateDefinitions);

            var filtro = Builders<ServicosAgendados>.Filter.Eq(c => c.Id, id);

            var resultado = await _servicoCollection.UpdateOneAsync(filtro, combinedUpdate);

            if (resultado.ModifiedCount > 0)
            {
                return await ConsultarId(id);
            }

            return null;
        }


        public async Task Excluir(string id)
        {
            var filtro = Builders<ServicosAgendados>.Filter.Eq(u => u.Id, id);
            await _servicoCollection.DeleteOneAsync(filtro);
        }
    }
}
