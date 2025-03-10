using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface IDiasPreferenciaService
    {
        Task<DiasPreferencia> Criar(DiasPreferencia dias);
        Task<DiasPreferencia> ConsultarPorUsuarioId(string id);
        Task<List<DiasPreferencia>> ConsultarTodos();
        Task<DiasPreferencia?> Atualizar(DiasPreferencia dias);
        Task<DiasPreferencia?> ConsultarId(string id);
        Task Excluir(string id);
    }
}
