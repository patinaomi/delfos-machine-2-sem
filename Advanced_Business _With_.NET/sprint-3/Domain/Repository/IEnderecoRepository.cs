using Project.Models;

namespace Project.Infrastructure.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<Endereco> Criar(Endereco endereco);
        Task<Endereco> ConsultarPorUsuarioId(string id);
        Task<List<Endereco>> ConsultarTodos();
        Task<Endereco?> Atualizar(Endereco endereco);

        Task Excluir(string id);

    }
}
