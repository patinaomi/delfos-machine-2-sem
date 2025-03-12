using Project.Infrastructure.Interfaces;
using Project.Models;

namespace Project.Application.Services
{
    public class ServicosAgendadosService : IServicosAgendadosService
    {
        private readonly IServicosAgendadosRepository _servicosRepository;

        public ServicosAgendadosService(IServicosAgendadosRepository servicoRepository)
        {
            _servicosRepository = servicoRepository;
        }

        public async Task<ServicosAgendados> Criar(ServicosAgendados servico)
        {
            return await _servicosRepository.Criar(servico);
        }

        public async Task<List<ServicosAgendados>> ConsultarTodos()
        {
            var servicos = await _servicosRepository.ConsultarTodos();
            return servicos.ToList(); 
        }

        public async Task<ServicosAgendados> ConsultarId(string id)
        {
            return await _servicosRepository.ConsultarId(id);
        }


        public async Task<ServicosAgendados?> Atualizar(ServicosAgendados servico)
        {
            return await _servicosRepository.Atualizar(servico);
        }

        public async Task<ServicosAgendados?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            return await _servicosRepository.AtualizarParcial(id, camposParaAtualizar);
        }

        public async Task Excluir(string id)
        {
            await _servicosRepository.Excluir(id);
        }
    }
}
