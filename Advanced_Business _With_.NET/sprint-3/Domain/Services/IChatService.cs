using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface IChatService
    {
        Task<Chat> Criar(Chat chat);
        Task<Chat> ConsultarId(string id);
        Task<List<Chat>> ConsultarTodos();
    }
}
