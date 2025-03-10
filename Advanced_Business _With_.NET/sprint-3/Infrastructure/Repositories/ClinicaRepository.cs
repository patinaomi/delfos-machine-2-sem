using MongoDB.Driver;
using Project.Models;
using Project.Infrastructure.Interfaces;
using Project.Domain;
using Microsoft.Extensions.Options;

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

        public async Task Excluir(string id)
        {
            var filtro = Builders<Clinica>.Filter.Eq(u => u.Id, id);
            await _clinicaCollection.DeleteOneAsync(filtro);
        }
    }
}
