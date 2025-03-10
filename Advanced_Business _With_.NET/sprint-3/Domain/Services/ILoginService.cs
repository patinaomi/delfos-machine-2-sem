using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface ILoginService
    {
        Task<Login> Criar(Login login);
        Task<Login> ConsultarId(string id);
        Task<List<Login>> ConsultarTodos();
        Task<Usuario?> Autenticar(string email, string senha);


    }
}
