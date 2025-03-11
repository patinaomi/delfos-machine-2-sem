using Project.Infrastructure.Interfaces;
using Project.Models;

namespace Project.Application.Services
{
    public class SugestaoConsultaClinicaService : ISugestaoConsultaClinicaService
    {
        private readonly ISugestaoConsultaClinicaRepository _sugestaoConsultaClinicaRepository;

        public SugestaoConsultaClinicaService(ISugestaoConsultaClinicaRepository sugestaoRepository)
        {
            _sugestaoConsultaClinicaRepository = sugestaoRepository;
        }

        public async Task<SugestaoConsultaClinica> Criar(SugestaoConsultaClinica sugestao)
        {
            return await _sugestaoConsultaClinicaRepository.Criar(sugestao);
        }

        public async Task<List<SugestaoConsultaClinica>> ConsultarTodos()
        {
            var sugestaoes = await _sugestaoConsultaClinicaRepository.ConsultarTodos();
            return sugestaoes.ToList(); 
        }

        public async Task<SugestaoConsultaClinica> ConsultarId(string id)
        {
            return await _sugestaoConsultaClinicaRepository.ConsultarId(id);
        }


        public async Task<SugestaoConsultaClinica?> Atualizar(SugestaoConsultaClinica sugestao)
        {
            return await _sugestaoConsultaClinicaRepository.Atualizar(sugestao);
        }

        public async Task<SugestaoConsultaClinica?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            return await _sugestaoConsultaClinicaRepository.AtualizarParcial(id, camposParaAtualizar);
        }

        public async Task Excluir(string id)
        {
            await _sugestaoConsultaClinicaRepository.Excluir(id);
        }
    }
}
