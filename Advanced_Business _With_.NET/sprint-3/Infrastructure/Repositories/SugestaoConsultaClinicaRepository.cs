using MongoDB.Driver;
using Project.Models;
using Project.Infrastructure.Interfaces;
using Project.Domain;
using Microsoft.Extensions.Options;

namespace Project.Repositories
{
    public class SugestaoConsultaClinicaRepository : ISugestaoConsultaClinicaRepository
    {
        private readonly IMongoCollection<SugestaoConsultaClinica> _sugestaoConsultaClinicaCollection;

        public SugestaoConsultaClinicaRepository(IOptions<ConfigMongoDb> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _sugestaoConsultaClinicaCollection = mongoDatabase.GetCollection<SugestaoConsultaClinica>(settings.Value.SugestaoConsultaClinicaCollectionName);
        }

        public async Task<SugestaoConsultaClinica> Criar(SugestaoConsultaClinica sugestao)
        {
            await _sugestaoConsultaClinicaCollection.InsertOneAsync(sugestao);
            return sugestao;
        }

        public async Task<SugestaoConsultaClinica> ConsultarId(string id)
        {
            var filtro = Builders<SugestaoConsultaClinica>.Filter.Eq(u => u.Id, id);
            return await _sugestaoConsultaClinicaCollection.Find(filtro).FirstOrDefaultAsync();
        }

        public async Task<List<SugestaoConsultaClinica>> ConsultarTodos()
        {
            return await _sugestaoConsultaClinicaCollection.Find(_ => true).ToListAsync();
        }

        public async Task<SugestaoConsultaClinica?> Atualizar(SugestaoConsultaClinica sugestao)
        {
            var filtro = Builders<SugestaoConsultaClinica>.Filter.Eq(u => u.Id, sugestao.Id);

            var update = Builders<SugestaoConsultaClinica>.Update
                .Set(u => u.IdUsuario, sugestao.IdUsuario)
                .Set(u => u.NomeCliente, sugestao.NomeCliente)
                .Set(u => u.TelefoneCliente, sugestao.TelefoneCliente)
                .Set(u => u.EmailCliente, sugestao.EmailCliente)
                .Set(u => u.DiaPreferenciaCliente, sugestao.DiaPreferenciaCliente)
                .Set(u => u.TurnoPreferenciaCliente, sugestao.TurnoPreferenciaCliente)
                .Set(u => u.HorarioPreferenciaCliente, sugestao.HorarioPreferenciaCliente)
                .Set(u => u.Especialidade, sugestao.Especialidade)
                .Set(u => u.CEPPreferenciaCliente, sugestao.CEPPreferenciaCliente)
                .Set(u => u.EstadoPreferenciaCliente, sugestao.EstadoPreferenciaCliente)
                .Set(u => u.CidadePreferenciaCliente, sugestao.CidadePreferenciaCliente)
                .Set(u => u.BairroPreferenciaCliente, sugestao.BairroPreferenciaCliente)
                .Set(u => u.RuaPreferenciaCliente, sugestao.RuaPreferenciaCliente)
                .Set(u => u.StatusSugestaoClinica, sugestao.StatusSugestaoClinica);
                //.Set(u => u.StatusSugestaoCliente, sugestao.StatusSugestaoCliente);

            var resultado = await _sugestaoConsultaClinicaCollection.UpdateOneAsync(filtro, update);

            if (resultado.ModifiedCount > 0)
            {
                return sugestao;
            }

            return null;
        }

        public async Task<SugestaoConsultaClinica?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            var sugestao = await ConsultarId(id);
            if (sugestao == null)
            {
                return null;
            }

            var updateDefinitionBuilder = Builders<SugestaoConsultaClinica>.Update;
            var updateDefinitions = new List<UpdateDefinition<SugestaoConsultaClinica>>();

            foreach (var campo in camposParaAtualizar)
            {
                switch (campo.Key.ToLower())
                {   
                    case "idUsuario":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.IdUsuario, campo.Value.ToString()));
                        break;
                    case "nomecliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.NomeCliente, campo.Value.ToString()));
                        break;
                    case "telefonecliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.TelefoneCliente, campo.Value.ToString()));
                        break;
                    case "emaicliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.EmailCliente, campo.Value.ToString()));
                        break;
                    case "diapreferenciacliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.DiaPreferenciaCliente, campo.Value.ToString()));
                        break;
                    case "turnopreferenciacliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.TurnoPreferenciaCliente, campo.Value.ToString()));
                        break;
                    case "horariopreferenciacliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.HorarioPreferenciaCliente, campo.Value.ToString()));
                        break;
                    case "especialidade":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Especialidade, campo.Value.ToString()));
                        break;
                    case "ceppreferenciacliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.CEPPreferenciaCliente, campo.Value.ToString()));
                        break;
                    case "estadopreferenciacliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.EstadoPreferenciaCliente, campo.Value.ToString()));
                        break;
                    case "cidadepreferenciacliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.CidadePreferenciaCliente, campo.Value.ToString()));
                        break;
                    case "bairropreferenciacliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.BairroPreferenciaCliente, campo.Value.ToString()));
                        break;
                    case "ruapreferenciacliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.RuaPreferenciaCliente, campo.Value.ToString()));
                        break;
                    case "statussugestaoclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.StatusSugestaoClinica, campo.Value.ToString()));
                        break;
                    /*case "statussugestaocliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.StatusSugestaoCliente, campo.Value.ToString()));
                        break;*/
                }
            }

            if (updateDefinitions.Count == 0)
            {
                return sugestao;
            }

            // Combinar todas as definições de atualização
            var combinedUpdate = updateDefinitionBuilder.Combine(updateDefinitions);
            
            // Filtro para encontrar o documento
            var filtro = Builders<SugestaoConsultaClinica>.Filter.Eq(c => c.Id, id);
            
            // Executar a atualização
            var resultado = await _sugestaoConsultaClinicaCollection.UpdateOneAsync(filtro, combinedUpdate);

            // Retornar as sugestões atualizado se a operação for bem-sucedida
            if (resultado.ModifiedCount > 0)
            {
                // Buscar as sugestões atualizadas
                return await ConsultarId(id);
            }

            return null;
        }


        public async Task Excluir(string id)
        {
            var filtro = Builders<SugestaoConsultaClinica>.Filter.Eq(u => u.Id, id);
            await _sugestaoConsultaClinicaCollection.DeleteOneAsync(filtro);
        }
    }
}
