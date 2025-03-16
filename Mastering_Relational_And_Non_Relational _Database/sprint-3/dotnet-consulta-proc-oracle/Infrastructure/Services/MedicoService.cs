using LexusTech.Infrastructure.Interfaces;
using LexusTech.Models;

namespace LexusTech.Application.Services
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

        public async Task<Medico> BuscarCliente(int id)
        {
            return await _medicoRepository.BuscarCliente(id);
        }


        public async Task<Medico> Atualizar(Medico medico)
        {
            return await _medicoRepository.Atualizar(medico);
        }

        public async Task<Medico> Excluir(Medico medico)
        {
            return await _medicoRepository.Excluir(medico);
        }
    }
}
