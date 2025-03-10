using Project.Infrastructure.Interfaces;
using Project.Models;
using Project.Repositories;

namespace Project.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository, IUsuarioRepository usuarioRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<Login> Criar(Login login)
        {
            return await _loginRepository.Criar(login);
        }

        public async Task<List<Login>> ConsultarTodos()
        {
            var logins = await _loginRepository.ConsultarTodos();
            return logins.ToList(); 
        }

        public async Task<Login> ConsultarId(string id)
        {
            return await _loginRepository.ConsultarId(id);
        }

        public async Task<Usuario?> Autenticar(string email, string senha)
        {
            var usuario = await _loginRepository.Autenticar(email, senha);

            if (usuario != null)
            {
                var login = new Login
                {
                    IdUsuario = usuario.Id,
                    Email = usuario.Email
                };

                await Criar(login);
            }

            return usuario;

        }

    }
}
