using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface ISugestaoConsultaClinicaRepository
    {
        Task<SugestaoConsultaClinica> Criar(SugestaoConsultaClinica sugestaoConsultaClinica);
        Task<SugestaoConsultaClinica> ConsultarId(string id);
        Task<List<SugestaoConsultaClinica>> ConsultarTodos();
        Task<SugestaoConsultaClinica?> Atualizar(SugestaoConsultaClinica sugestaoConsultaClinica);
        Task<SugestaoConsultaClinica?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar);
        Task Excluir(string id);

    }
}
