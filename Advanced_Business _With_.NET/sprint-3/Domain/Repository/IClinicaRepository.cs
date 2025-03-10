using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface IClinicaRepository
    {
        Task<Clinica> Criar(Clinica clinica);
        Task<Clinica> ConsultarId(string id);
        Task<List<Clinica>> ConsultarTodos();
        Task<Clinica?> Atualizar(Clinica clinica);
        Task Excluir(string id);

    }
}
