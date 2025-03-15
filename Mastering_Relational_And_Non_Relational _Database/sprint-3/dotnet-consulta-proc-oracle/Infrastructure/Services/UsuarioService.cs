using LexusTech.Infrastructure.Interfaces;
using LexusTech.Models;

namespace LexusTech.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> Criar(Usuario usuario)
        {
            return await _usuarioRepository.Criar(usuario);
        }

        public async Task<List<Usuario>> ConsultarTodos()
        {
            var usuarios = await _usuarioRepository.ConsultarTodos();
            return usuarios.ToList(); 
        }

        public async Task<Usuario> BuscarCliente(int id)
        {
            return await _usuarioRepository.BuscarCliente(id);
        }


        public async Task<Usuario> Atualizar(Usuario usuario)
        {
            return await _usuarioRepository.Atualizar(usuario);
        }

        public async Task<Usuario> Excluir(Usuario usuario)
        {
            return await _usuarioRepository.Excluir(usuario);
        }
    }
}
