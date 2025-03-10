using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface IHorariosService
    {
        Task<Horarios> Criar(Horarios horarios);
        Task<Horarios> ConsultarPorUsuarioId(string id);
        Task<List<Horarios>> ConsultarTodos();
        Task<Horarios?> Atualizar(Horarios horarios);
        Task<Horarios?> ConsultarId(string id);
        Task Excluir(string id);
    }
}
