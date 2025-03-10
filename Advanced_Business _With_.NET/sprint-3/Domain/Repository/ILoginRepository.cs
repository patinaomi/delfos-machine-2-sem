
using Project.Models;

namespace Project.Repositories
{
    public interface ILoginRepository
    {
        Task<Login> Criar(Login login);
        Task<List<Login>> ConsultarTodos();
        Task<Login> ConsultarId(string id);
        Task<Usuario?> Autenticar(string email, string senha);

    }
}
