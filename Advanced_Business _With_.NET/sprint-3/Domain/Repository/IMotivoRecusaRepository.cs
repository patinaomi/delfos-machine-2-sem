using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface IMotivoRecusaRepository
    {
        Task<MotivoRecusa> Criar(MotivoRecusa motivo);
        Task<MotivoRecusa> ConsultarId(string id);
        Task<List<MotivoRecusa>> ConsultarTodos();
        Task<MotivoRecusa?> Atualizar(MotivoRecusa motivo);
        Task<MotivoRecusa?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar);
        Task Excluir(string id);

    }
}
