using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface IDiasPreferenciaRepository
    {
        Task<DiasPreferencia> Criar(DiasPreferencia dias);
        Task<DiasPreferencia> ConsultarPorUsuarioId(string id);
        Task<List<DiasPreferencia>> ConsultarTodos();
        Task<DiasPreferencia?> Atualizar(DiasPreferencia dias);
        Task Excluir(string id);


    }
}
