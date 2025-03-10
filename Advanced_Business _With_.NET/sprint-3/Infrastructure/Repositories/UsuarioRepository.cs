using MongoDB.Driver;
using Project.Models;
using Project.Infrastructure.Interfaces;
using Project.Domain;
using Microsoft.Extensions.Options;

namespace Project.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IMongoCollection<Usuario> _usuarioCollection;

        public UsuarioRepository(IOptions<ConfigMongoDb> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _usuarioCollection = mongoDatabase.GetCollection<Usuario>(settings.Value.UsuarioCollectionName);
        }

        public async Task<Usuario> Criar(Usuario usuario)
        {
            await _usuarioCollection.InsertOneAsync(usuario);
            return usuario;
        }

        public async Task<Usuario> ConsultarId(string id)
        {
            var filtro = Builders<Usuario>.Filter.Eq(u => u.Id, id);
            return await _usuarioCollection.Find(filtro).FirstOrDefaultAsync();
        }

        public async Task<List<Usuario>> ConsultarTodos()
        {
            return await _usuarioCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Usuario?> Atualizar(Usuario usuario)
        {
            var filtro = Builders<Usuario>.Filter.Eq(u => u.Id, usuario.Id);

            var update = Builders<Usuario>.Update
                .Set(u => u.Nome, usuario.Nome)
                .Set(u => u.Telefone, usuario.Telefone)
                .Set(u => u.Email, usuario.Email)
                .Set(u => u.Senha, usuario.Senha);

            var resultado = await _usuarioCollection.UpdateOneAsync(filtro, update);

            if (resultado.ModifiedCount > 0)
            {
                return usuario;
            }

            return null;
        }

        public async Task Excluir(string id)
        {
            var filtro = Builders<Usuario>.Filter.Eq(u => u.Id, id);
            await _usuarioCollection.DeleteOneAsync(filtro);
        }
    }
}
