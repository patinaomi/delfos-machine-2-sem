using Project.Infrastructure.Interfaces;
using Project.Models;

namespace Project.Application.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public async Task<Endereco> Criar(Endereco endereco)
        {
            return await _enderecoRepository.Criar(endereco);
        }

        public async Task<List<Endereco>> ConsultarTodos()
        {
            var enderecos = await _enderecoRepository.ConsultarTodos();
            return enderecos.ToList(); 
        }

        public async Task<Endereco> ConsultarPorUsuarioId(string usuarioId)
        {
            return await _enderecoRepository.ConsultarPorUsuarioId(usuarioId);
           
        }
        public async Task<Endereco?> ConsultarId(string id)
        {
            var enderecos = await _enderecoRepository.ConsultarTodos();
            return enderecos.FirstOrDefault(e => e.Id == id);
        }


        public async Task<Endereco?> Atualizar(Endereco endereco)
        {
            return await _enderecoRepository.Atualizar(endereco);
        }


        public async Task<Endereco?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            return await _enderecoRepository.AtualizarParcial(id, camposParaAtualizar);
        }


        public async Task Excluir(string id)
        {
            await _enderecoRepository.Excluir(id);
        }
    }
}
