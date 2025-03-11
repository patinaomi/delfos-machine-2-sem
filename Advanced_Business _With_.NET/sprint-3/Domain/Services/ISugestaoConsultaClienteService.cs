using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface ISugestaoConsultaClienteService
    {
        Task<SugestaoConsultaCliente> Criar(SugestaoConsultaCliente sugestao);
        Task<SugestaoConsultaCliente> ConsultarId(string id);
        Task<List<SugestaoConsultaCliente>> ConsultarTodos();
        Task<SugestaoConsultaCliente?> Atualizar(SugestaoConsultaCliente sugestao);
        Task<SugestaoConsultaCliente?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar);
        Task Excluir(string id);
    }
}
