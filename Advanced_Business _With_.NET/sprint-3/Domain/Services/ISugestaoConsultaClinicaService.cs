using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface ISugestaoConsultaClinicaService
    {
        Task<SugestaoConsultaClinica> Criar(SugestaoConsultaClinica sugestao);
        Task<SugestaoConsultaClinica> ConsultarId(string id);
        Task<List<SugestaoConsultaClinica>> ConsultarTodos();
        Task<SugestaoConsultaClinica?> Atualizar(SugestaoConsultaClinica sugestao);
        Task<SugestaoConsultaClinica?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar);
        Task Excluir(string id);
    }
}
