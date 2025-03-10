using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface ITurnoService
    {
        Task<Turno> Criar(Turno turno);
        Task<Turno> ConsultarPorUsuarioId(string id);
        Task<List<Turno>> ConsultarTodos();
        Task<Turno?> Atualizar(Turno turno);
        Task<Turno?> ConsultarId(string id);
        Task Excluir(string id);
    }
}
