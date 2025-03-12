using Project.Infrastructure.Interfaces;
using Project.Models;

namespace Project.Application.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;

        public ConsultaService(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<Consulta> Criar(Consulta consulta)
        {
            return await _consultaRepository.Criar(consulta);
        }

        public async Task<List<Consulta>> ConsultarTodos()
        {
            var consultas = await _consultaRepository.ConsultarTodos();
            return consultas.ToList(); 
        }

        public async Task<Consulta> ConsultarId(string id)
        {
            return await _consultaRepository.ConsultarId(id);
        }


        public async Task<Consulta?> Atualizar(Consulta consulta)
        {
            return await _consultaRepository.Atualizar(consulta);
        }

        public async Task<Consulta?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            return await _consultaRepository.AtualizarParcial(id, camposParaAtualizar);
        }

        public async Task Excluir(string id)
        {
            await _consultaRepository.Excluir(id);
        }
    }
}
