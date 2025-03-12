using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface IFeedbackService
    {
        Task<Feedback> Criar(Feedback feedback);
        Task<Feedback> ConsultarId(string id);
        Task<List<Feedback>> ConsultarTodos();
        Task<Feedback?> Atualizar(Feedback feedback);
        Task<Feedback?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar);
        Task Excluir(string id);
    }
}
