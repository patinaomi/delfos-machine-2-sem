using MongoDB.Driver;
using Project.Models;
using Project.Infrastructure.Interfaces;
using Project.Domain;
using Microsoft.Extensions.Options;

namespace Project.Repositories
{
    public class SugestaoConsultaClienteRepository : ISugestaoConsultaClienteRepository
    {
        private readonly IMongoCollection<SugestaoConsultaCliente> _sugestaoConsultaClienteCollection;

        public SugestaoConsultaClienteRepository(IOptions<ConfigMongoDb> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _sugestaoConsultaClienteCollection = mongoDatabase.GetCollection<SugestaoConsultaCliente>(settings.Value.SugestaoConsultaClienteCollectionName);
        }

        public async Task<SugestaoConsultaCliente> Criar(SugestaoConsultaCliente sugestao)
        {
            await _sugestaoConsultaClienteCollection.InsertOneAsync(sugestao);
            return sugestao;
        }

        public async Task<SugestaoConsultaCliente> ConsultarId(string id)
        {
            var filtro = Builders<SugestaoConsultaCliente>.Filter.Eq(u => u.Id, id);
            return await _sugestaoConsultaClienteCollection.Find(filtro).FirstOrDefaultAsync();
        }

        public async Task<List<SugestaoConsultaCliente>> ConsultarTodos()
        {
            var filtro = Builders<SugestaoConsultaCliente>.Filter.Eq(u => u.StatusSugestaoCliente, "Pendente");
            //return await _sugestaoConsultaClienteCollection.Find(_ => true).
            //ToListAsync();
            return await _sugestaoConsultaClienteCollection.Find(filtro).ToListAsync();
        }

        public async Task<SugestaoConsultaCliente?> Atualizar(SugestaoConsultaCliente sugestao)
        {
            var filtro = Builders<SugestaoConsultaCliente>.Filter.Eq(u => u.Id, sugestao.Id);

            var update = Builders<SugestaoConsultaCliente>.Update
                .Set(u => u.IdClinica, sugestao.IdClinica)
                .Set(u => u.NomeClinica, sugestao.NomeClinica)
                .Set(u => u.TelefoneClinica, sugestao.TelefoneClinica)
                .Set(u => u.EmailClinica, sugestao.EmailClinica)
                .Set(u => u.IdUsuario, sugestao.IdUsuario)
                .Set(u => u.NomeCliente, sugestao.NomeCliente)
                .Set(u => u.TelefoneCliente, sugestao.TelefoneCliente)
                .Set(u => u.EmailCliente, sugestao.EmailCliente)
                .Set(u => u.DiaPreferenciaCliente, sugestao.DiaPreferenciaCliente)
                .Set(u => u.TurnoPreferenciaCliente, sugestao.TurnoPreferenciaCliente)
                .Set(u => u.HorarioPreferenciaCliente, sugestao.HorarioPreferenciaCliente)
                .Set(u => u.Especialidade, sugestao.Especialidade)
                .Set(u => u.CEPClinica, sugestao.CEPClinica)
                .Set(u => u.EstadoClinica, sugestao.EstadoClinica)
                .Set(u => u.CidadeClinica, sugestao.CidadeClinica)
                .Set(u => u.BairroClinica, sugestao.BairroClinica)
                .Set(u => u.RuaClinica, sugestao.RuaClinica)
                //.Set(u => u.StatusSugestaoClinica, sugestao.StatusSugestaoClinica)
                .Set(u => u.StatusSugestaoCliente, sugestao.StatusSugestaoCliente);

            var resultado = await _sugestaoConsultaClienteCollection.UpdateOneAsync(filtro, update);

            if (resultado.ModifiedCount > 0)
            {
                return sugestao;
            }

            return null;
        }

        public async Task<SugestaoConsultaCliente?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            var sugestao = await ConsultarId(id);
            if (sugestao == null)
            {
                return null;
            }

            var updateDefinitionBuilder = Builders<SugestaoConsultaCliente>.Update;
            var updateDefinitions = new List<UpdateDefinition<SugestaoConsultaCliente>>();

            foreach (var campo in camposParaAtualizar)
            {
                switch (campo.Key.ToLower())
                {
                    case "idclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.IdClinica, campo.Value.ToString()));
                        break;
                    case "nomeclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.NomeClinica, campo.Value.ToString()));
                        break;
                    case "telefoneclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.TelefoneClinica, campo.Value.ToString()));
                        break;
                    case "emailclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.EmailClinica, campo.Value.ToString()));
                        break;
                    case "idusuario":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.IdUsuario, campo.Value.ToString()));
                        break;
                    case "nomecliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.NomeCliente, campo.Value.ToString()));
                        break;
                    case "telefonecliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.TelefoneCliente, campo.Value.ToString()));
                        break;
                    case "emailcliente":
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
                    case "cepclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.CEPClinica, campo.Value.ToString()));
                        break;
                    case "estadoclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.EstadoClinica, campo.Value.ToString()));
                        break;
                    case "cidadeclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.CidadeClinica, campo.Value.ToString()));
                        break;
                    case "BairroClinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.BairroClinica, campo.Value.ToString()));
                        break;
                    case "ruaclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.RuaClinica, campo.Value.ToString()));
                        break;
                    /*case "statussugestaoclinica":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.StatusSugestaoClinica, campo.Value.ToString()));
                        break;*/
                    case "statussugestaocliente":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.StatusSugestaoCliente, campo.Value.ToString()));
                        break;
                }
            }

            if (updateDefinitions.Count == 0)
            {
                return sugestao;
            }

            // Combinar todas as definições de atualização
            var combinedUpdate = updateDefinitionBuilder.Combine(updateDefinitions);
            
            // Filtro para encontrar o documento
            var filtro = Builders<SugestaoConsultaCliente>.Filter.Eq(c => c.Id, id);
            
            // Executar a atualização
            var resultado = await _sugestaoConsultaClienteCollection.UpdateOneAsync(filtro, combinedUpdate);

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
            var filtro = Builders<SugestaoConsultaCliente>.Filter.Eq(u => u.Id, id);
            await _sugestaoConsultaClienteCollection.DeleteOneAsync(filtro);
        }
    }
}
