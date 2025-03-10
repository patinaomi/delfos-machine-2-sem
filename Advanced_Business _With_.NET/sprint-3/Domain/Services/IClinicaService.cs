using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface IClinicaService
    {
        Task<Clinica> Criar(Clinica clinica);
        Task<Clinica> ConsultarId(string id);
        Task<List<Clinica>> ConsultarTodos();
        Task<Clinica?> Atualizar(Clinica clinica);
        Task<Clinica?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar);
        Task Excluir(string id);
    }
}
