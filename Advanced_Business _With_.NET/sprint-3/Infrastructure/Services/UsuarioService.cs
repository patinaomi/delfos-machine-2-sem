using Project.Infrastructure.Interfaces;
using Project.Models;

namespace Project.Application.Services
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

        public async Task<Usuario> ConsultarId(string id)
        {
            return await _usuarioRepository.ConsultarId(id);
        }


        public async Task<Usuario?> Atualizar(Usuario usuario)
        {
            return await _usuarioRepository.Atualizar(usuario);
        }

        public async Task<Usuario?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            return await _usuarioRepository.AtualizarParcial(id, camposParaAtualizar);
        }

        public async Task Excluir(string id)
        {
            await _usuarioRepository.Excluir(id);
        }
    }
}
