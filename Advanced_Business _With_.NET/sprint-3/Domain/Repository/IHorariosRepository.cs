using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface IHorariosRepository
    {
        Task<Horarios> Criar(Horarios horarios);
        Task<Horarios> ConsultarPorUsuarioId(string id);
        Task<List<Horarios>> ConsultarTodos();
        Task<Horarios?> Atualizar(Horarios horarios);
        Task Excluir(string id);


    }
}
