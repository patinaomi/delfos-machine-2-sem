using MongoDB.Driver;
using Project.Models;
using Project.Infrastructure.Interfaces;
using Project.Domain;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

namespace Project.Repositories
{
    public class ClinicaRepository : IClinicaRepository
    {
        private readonly IMongoCollection<Clinica> _clinicaCollection;

        public ClinicaRepository(IOptions<ConfigMongoDb> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _clinicaCollection = mongoDatabase.GetCollection<Clinica>(settings.Value.ClinicaCollectionName);
        }

        public async Task<Clinica> Criar(Clinica clinica)
        {
            await _clinicaCollection.InsertOneAsync(clinica);
            return clinica;
        }

        public async Task<Clinica> ConsultarId(string id)
        {
            var filtro = Builders<Clinica>.Filter.Eq(u => u.Id, id);
            return await _clinicaCollection.Find(filtro).FirstOrDefaultAsync();
        }

        public async Task<List<Clinica>> ConsultarTodos()
        {
            return await _clinicaCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Clinica?> Atualizar(Clinica Clinica)
        {
            var filtro = Builders<Clinica>.Filter.Eq(u => u.Id, Clinica.Id);

            var update = Builders<Clinica>.Update
                .Set(u => u.Nome, Clinica.Nome)
                .Set(u => u.CNPJ, Clinica.CNPJ)
                .Set(u => u.Telefone, Clinica.Telefone)
                .Set(u => u.Email, Clinica.Email)
                .Set(u => u.Senha, Clinica.Senha)
                .Set(u => u.Perfil, Clinica.Perfil);

            var resultado = await _clinicaCollection.UpdateOneAsync(filtro, update);

            if (resultado.ModifiedCount > 0)
            {
                return Clinica;
            }

            return null;
        }

        private IActionResult BadRequest(object value)
        {
            throw new NotImplementedException();
        }

        public async Task<Clinica?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            // Verificar se a clínica existe
            var clinica = await ConsultarId(id);
            if (clinica == null)
            {
                return null;
            }

            // Criar o builder de update para MongoDB
            var updateDefinitionBuilder = Builders<Clinica>.Update;
            var updateDefinitions = new List<UpdateDefinition<Clinica>>();

            // Adicionar cada campo ao update definition
            foreach (var campo in camposParaAtualizar)
            {
                // Adiciona a operação de atualização conforme o nome do campo
                switch (campo.Key.ToLower())
                {
                    case "nome":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Nome, campo.Value.ToString()));
                        break;
                    case "cnpj":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.CNPJ, campo.Value.ToString()));
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
                    case "perfil":
                        updateDefinitions.Add(updateDefinitionBuilder.Set(c => c.Perfil, campo.Value.ToString()));
                        break;
                    // Adicione outros campos conforme necessário
                }
            }

            // Se não houver campos para atualizar, retornar a clínica sem alterações
            if (updateDefinitions.Count == 0)
            {
                return clinica;
            }

            // Combinar todas as definições de atualização
            var combinedUpdate = updateDefinitionBuilder.Combine(updateDefinitions);
            
            // Filtro para encontrar o documento
            var filtro = Builders<Clinica>.Filter.Eq(c => c.Id, id);
            
            // Executar a atualização
            var resultado = await _clinicaCollection.UpdateOneAsync(filtro, combinedUpdate);

            // Retornar a clínica atualizada se a operação for bem-sucedida
            if (resultado.ModifiedCount > 0)
            {
                // Buscar a clínica atualizada
                return await ConsultarId(id);
            }

            return null;
        }

        
        public async Task Excluir(string id)
        {
            var filtro = Builders<Clinica>.Filter.Eq(u => u.Id, id);
            await _clinicaCollection.DeleteOneAsync(filtro);
        }
    }
}
