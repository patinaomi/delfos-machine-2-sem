using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface IConsultaService
    {
        Task<Consulta> Criar(Consulta consulta);
        Task<Consulta> ConsultarId(string id);
        Task<List<Consulta>> ConsultarTodos();
        Task<Consulta?> Atualizar(Consulta consulta);
        Task<Consulta?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar);
        Task Excluir(string id);
    }
}
