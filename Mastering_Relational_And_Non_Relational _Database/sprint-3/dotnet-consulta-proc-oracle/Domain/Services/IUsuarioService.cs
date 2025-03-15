using LexusTech.Models;

namespace LexusTech.Infrastructure.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> Criar(Usuario usuario);
        Task<Usuario> BuscarCliente(int usuario);
        Task<List<Usuario>> ConsultarTodos();
        Task<Usuario> Atualizar(Usuario usuario);
        Task<Usuario> Excluir(Usuario usuario);
    }
}
