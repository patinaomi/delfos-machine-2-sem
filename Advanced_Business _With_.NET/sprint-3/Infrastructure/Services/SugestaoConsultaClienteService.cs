using Project.Infrastructure.Interfaces;
using Project.Models;

namespace Project.Application.Services
{
    public class SugestaoConsultaClienteService : ISugestaoConsultaClienteService
    {
        private readonly ISugestaoConsultaClienteRepository _sugestaoConsultaClienteRepository;

        public SugestaoConsultaClienteService(ISugestaoConsultaClienteRepository sugestaoRepository)
        {
            _sugestaoConsultaClienteRepository = sugestaoRepository;
        }

        public async Task<SugestaoConsultaCliente> Criar(SugestaoConsultaCliente sugestao)
        {
            return await _sugestaoConsultaClienteRepository.Criar(sugestao);
        }

        public async Task<List<SugestaoConsultaCliente>> ConsultarTodos()
        {
            var sugestaoes = await _sugestaoConsultaClienteRepository.ConsultarTodos();
            return sugestaoes.ToList(); 
        }

        public async Task<SugestaoConsultaCliente> ConsultarId(string id)
        {
            return await _sugestaoConsultaClienteRepository.ConsultarId(id);
        }


        public async Task<SugestaoConsultaCliente?> Atualizar(SugestaoConsultaCliente sugestao)
        {
            return await _sugestaoConsultaClienteRepository.Atualizar(sugestao);
        }

        public async Task<SugestaoConsultaCliente?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            return await _sugestaoConsultaClienteRepository.AtualizarParcial(id, camposParaAtualizar);
        }

        public async Task Excluir(string id)
        {
            await _sugestaoConsultaClienteRepository.Excluir(id);
        }
    }
}
