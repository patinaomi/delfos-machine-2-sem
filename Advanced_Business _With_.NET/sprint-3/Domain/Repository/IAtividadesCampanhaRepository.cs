using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface ICampanhaRepository
    {
        Task<Campanha> Criar(Campanha campanha);
        Task<Campanha> ConsultarId(string id);
        Task<List<Campanha>> ConsultarTodos();
        Task<Campanha?> Atualizar(Campanha campanha);
        Task<Campanha?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar);
        Task Excluir(string id);

    }
}
