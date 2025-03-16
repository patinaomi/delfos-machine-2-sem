using LexusTech.Models;

namespace LexusTech.Infrastructure.Interfaces
{
    public interface IClinicaRepository
    {
        Task<Clinica> Criar(Clinica clinica);
        Task<Clinica> BuscarCliente(int id);
        Task<List<Clinica>> ConsultarTodos();
        Task<Clinica> Atualizar(Clinica clinica);
        Task<Clinica> Excluir(Clinica clinica);

    }
}
