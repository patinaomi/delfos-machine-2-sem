using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> Criar(Usuario usuario);
        Task<Usuario> ConsultarId(string id);
        Task<List<Usuario>> ConsultarTodos();
        Task<Usuario?> Atualizar(Usuario usuario);
        Task Excluir(string id);
    }
}
