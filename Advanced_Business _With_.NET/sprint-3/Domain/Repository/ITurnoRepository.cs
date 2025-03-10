using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface ITurnoRepository
    {
        Task<Turno> Criar(Turno turno);
        Task<Turno> ConsultarPorUsuarioId(string id);
        Task<List<Turno>> ConsultarTodos();
        Task<Turno?> Atualizar(Turno turno);
        Task Excluir(string id);


    }
}
