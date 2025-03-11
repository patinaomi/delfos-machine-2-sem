using Project.Infrastructure.Interfaces;
using Project.Models;

namespace Project.Application.Services
{
    public class MotivoRecusaService : IMotivoRecusaService
    {
        private readonly IMotivoRecusaRepository _motivoRecusaRepository;

        public MotivoRecusaService(IMotivoRecusaRepository motivoRecusaRepository)
        {
            _motivoRecusaRepository = motivoRecusaRepository;
        }

        public async Task<MotivoRecusa> Criar(MotivoRecusa motivoRecusa)
        {
            return await _motivoRecusaRepository.Criar(motivoRecusa);
        }

        public async Task<List<MotivoRecusa>> ConsultarTodos()
        {
            var motivos = await _motivoRecusaRepository.ConsultarTodos();
            return motivos.ToList(); 
        }

        public async Task<MotivoRecusa> ConsultarId(string id)
        {
            return await _motivoRecusaRepository.ConsultarId(id);
        }


        public async Task<MotivoRecusa?> Atualizar(MotivoRecusa motivo)
        {
            return await _motivoRecusaRepository.Atualizar(motivo);
        }

        public async Task<MotivoRecusa?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            return await _motivoRecusaRepository.AtualizarParcial(id, camposParaAtualizar);
        }

        public async Task Excluir(string id)
        {
            await _motivoRecusaRepository.Excluir(id);
        }
    }
}
