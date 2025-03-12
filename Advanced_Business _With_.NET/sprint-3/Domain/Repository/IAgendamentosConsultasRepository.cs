using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface IServicosAgendadosRepository
    {
        Task<ServicosAgendados> Criar(ServicosAgendados servicos);
        Task<ServicosAgendados> ConsultarId(string id);
        Task<List<ServicosAgendados>> ConsultarTodos();
        Task<ServicosAgendados?> Atualizar(ServicosAgendados servicos);
        Task<ServicosAgendados?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar);
        Task Excluir(string id);

    }
}
