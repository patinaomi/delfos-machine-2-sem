using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface ISugestaoConsultaClienteRepository
    {
        Task<SugestaoConsultaCliente> Criar(SugestaoConsultaCliente sugestaoConsultaCliente);
        Task<SugestaoConsultaCliente> ConsultarId(string id);
        Task<List<SugestaoConsultaCliente>> ConsultarTodos();
        Task<SugestaoConsultaCliente?> Atualizar(SugestaoConsultaCliente sugestaoConsultaCliente);
        Task<SugestaoConsultaCliente?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar);
        Task Excluir(string id);

    }
}
