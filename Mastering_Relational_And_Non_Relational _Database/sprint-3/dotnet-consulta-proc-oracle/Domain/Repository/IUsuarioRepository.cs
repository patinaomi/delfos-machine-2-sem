using LexusTech.Models;

namespace LexusTech.Infrastructure.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Criar(Usuario usuario);
        Task<Usuario> BuscarCliente(int id);
        Task<List<Usuario>> ConsultarTodos();
        Task<Usuario> Atualizar(Usuario usuario);
        Task<Usuario> Excluir(Usuario usuario);

    }
}
