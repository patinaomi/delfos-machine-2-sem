using Project.Infrastructure.Interfaces;
using Project.Models;

namespace Project.Application.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;

        public MedicoService(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<Medico> Criar(Medico medico)
        {
            return await _medicoRepository.Criar(medico);
        }

        public async Task<List<Medico>> ConsultarTodos()
        {
            var medicos = await _medicoRepository.ConsultarTodos();
            return medicos.ToList(); 
        }

        public async Task<Medico> ConsultarId(string id)
        {
            return await _medicoRepository.ConsultarId(id);
        }


        public async Task<Medico?> Atualizar(Medico medico)
        {
            return await _medicoRepository.Atualizar(medico);
        }

        public async Task<Medico?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            return await _medicoRepository.AtualizarParcial(id, camposParaAtualizar);
        }

        public async Task Excluir(string id)
        {
            await _medicoRepository.Excluir(id);
        }
    }
}
