using LexusTech.Models;

namespace LexusTech.Infrastructure.Interfaces
{
    public interface IClinicaService
    {
        Task<Clinica> Criar(Clinica clinica);
        Task<Clinica> BuscarClinica(int id);
        Task<List<Clinica>> ConsultarTodos();
        Task<Clinica> Atualizar(Clinica clinica);
        Task<Clinica> Excluir(Clinica clinica);
    }
}
