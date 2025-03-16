using LexusTech.Models;

namespace LexusTech.Infrastructure.Interfaces
{
    public interface IMedicoService
    {
        Task<Medico> Criar(Medico medico);
        Task<Medico> BuscarMedico(int id);
        Task<List<Medico>> ConsultarTodos();
        Task<Medico> Atualizar(Medico medico);
        Task<Medico> Excluir(Medico medico);
    }
}
