using MongoDB.Driver;
using Project.Models;
using Microsoft.Extensions.Options;
using Project.Domain;

namespace Project.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IMongoCollection<Login> _loginCollection;
        private readonly IMongoCollection<Usuario> _usuarioCollection;

        public LoginRepository(IOptions<ConfigMongoDb> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

            // Tabelas
            _loginCollection = mongoDatabase.GetCollection<Login>(settings.Value.LoginCollectionName);
            _usuarioCollection = mongoDatabase.GetCollection<Usuario>(settings.Value.UsuarioCollectionName);

        }

        public async Task<Login> Criar(Login login)
        {
            await _loginCollection.InsertOneAsync(login);
            return login;
        }

        public async Task<Login> ConsultarId(string id)
        {
            var filtro = Builders<Login>.Filter.Eq(u => u.Id, id);
            return await _loginCollection.Find(filtro).FirstOrDefaultAsync();
        }

        public async Task<List<Login>> ConsultarTodos()
        {
            return await _loginCollection.Find(_ => true).ToListAsync();
        }

        // Método para autenticar o usuário, verificando o email e senha
        public async Task<Usuario?> Autenticar(string email, string senha)
        {
            var usuario = await _usuarioCollection
                .Find(l => l.Email == email && l.Senha == senha) 
                .FirstOrDefaultAsync();
            
            if (usuario == null)
            return null;

            // Cria histórico de login
            var login = new Login
            {
                IdUsuario = usuario.Id,
                Email = usuario.Email
                
            };

            await Criar(login);

            return usuario;
        }
    }
}
