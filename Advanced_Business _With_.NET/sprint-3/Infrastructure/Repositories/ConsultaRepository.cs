using MongoDB.Driver;
using Project.Models;
using Project.Infrastructure.Interfaces;
using Project.Domain;
using Microsoft.Extensions.Options;

namespace Project.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly IMongoCollection<Consulta> _consultaCollection;

        public ConsultaRepository(IOptions<ConfigMongoDb> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _consultaCollection = mongoDatabase.GetCollection<Consulta>(settings.Value.ConsultaCollectionName);
        }

        public async Task<Consulta> Criar(Consulta consulta)
        {
            await _consultaCollection.InsertOneAsync(consulta);
            return consulta;
        }

        public async Task<Consulta> ConsultarId(string id)
        {
            var filtro = Builders<Consulta>.Filter.Eq(u => u.Id, id);
            return await _consultaCollection.Find(filtro).FirstOrDefaultAsync();
        }

        public async Task<List<Consulta>> ConsultarTodos()
        {
            return await _consultaCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Consulta?> Atualizar(Consulta consulta)
        {
            var filtro = Builders<Consulta>.Filter.Eq(u => u.Id, consulta.Id);

            var update = Builders<Consulta>.Update
                .Set(u => u.IdUsuario, consulta.IdUsuario)
                .Set(u => u.NomeCliente, consulta.NomeCliente)
                .Set(u => u.CPFCliente, consulta.CPFCliente)
                .Set(u => u.Data, consulta.Data)
                .Set(u => u.Turno, consulta.Turno)
                .Set(u => u.Horario, consulta.Horario)
                .Set(u => u.IdClinica, consulta.IdClinica)
                .Set(u => u.NomeClinica, consulta.NomeClinica)
                .Set(u => u.CNPJClinica, consulta.CNPJClinica)
                .Set(u => u.NomeMedico, consulta.NomeMedico)
                .Set(u => u.Especialidade, consulta.Especialidade)
                .Set(u => u.CEPClinica, consulta.CEPClinica)
                .Set(u => u.EstadoClinica, consulta.EstadoClinica)
                .Set(u => u.CidadeClinica, consulta.CidadeClinica)
                .Set(u => u.BairroClinica, consulta.BairroClinica)
                .Set(u => u.RuaClinica, consulta.RuaClinica)
                .Set(u => u.StatusConsulta, consulta.StatusConsulta)
                .Set(u => u.RespostaFeedback, consulta.RespostaFeedback);

            var resultado = await _consultaCollection.UpdateOneAsync(filtro, update);

            if (resultado.ModifiedCount > 0)
            {
                return consulta;
            }

            return null;
        }

        public async Task<Consulta?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            var consulta = await ConsultarId(id);
            if (consulta == null)
            {
                return null;
            }

            var updateDefinitionBuilder = Builders<Consulta>.Update;
            var updateDefinitions = new List<UpdateDefinition<Consulta>>();

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
                    case "cpfcliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.CPFCliente, campo.Value.ToString()));
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
                    case "cnpjclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.CNPJClinica, campo.Value.ToString()));
                        break;
                    case "nomemedico":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.NomeMedico, campo.Value.ToString()));
                        break;
                    case "especialidade":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Especialidade, campo.Value.ToString()));
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
                    case "statusconsulta":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.StatusConsulta, campo.Value.ToString()));
                        break;
                    case "respostafeedback":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.RespostaFeedback, campo.Value.ToString()));
                        break;
                }
            }

            if (updateDefinitions.Count == 0)
            {
                return consulta;
            }

            var combinedUpdate = updateDefinitionBuilder.Combine(updateDefinitions);

            var filtro = Builders<Consulta>.Filter.Eq(c => c.Id, id);
            
            var resultado = await _consultaCollection.UpdateOneAsync(filtro, combinedUpdate);

            // Retornar o usuário atualizado se a operação for bem-sucedida
            if (resultado.ModifiedCount > 0)
            {
                // Buscar o usuário atualizada
                return await ConsultarId(id);
            }

            return null;
        }


        public async Task Excluir(string id)
        {
            var filtro = Builders<Consulta>.Filter.Eq(u => u.Id, id);
            await _consultaCollection.DeleteOneAsync(filtro);
        }
    }
}
