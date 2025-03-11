using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface IMedicoService
    {
        Task<Medico> Criar(Medico medico);
        Task<Medico> ConsultarId(string id);
        Task<List<Medico>> ConsultarTodos();
        Task<Medico?> Atualizar(Medico medico);
        Task<Medico?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar);
        Task Excluir(string id);
    }
}
