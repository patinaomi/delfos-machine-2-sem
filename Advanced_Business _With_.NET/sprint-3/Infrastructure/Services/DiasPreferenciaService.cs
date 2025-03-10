using Project.Infrastructure.Interfaces;
using Project.Models;

namespace Project.Application.Services
{
    public class DiasPreferenciaService : IDiasPreferenciaService
    {
        private readonly IDiasPreferenciaRepository _diasPreferenciaRepository;

        public DiasPreferenciaService(IDiasPreferenciaRepository diasPreferenciaRepository)
        {
            _diasPreferenciaRepository = diasPreferenciaRepository;
        }

        public async Task<DiasPreferencia> Criar(DiasPreferencia dias)
        {
            return await _diasPreferenciaRepository.Criar(dias);
        }

        public async Task<List<DiasPreferencia>> ConsultarTodos()
        {
            var dias = await _diasPreferenciaRepository.ConsultarTodos();
            return dias.ToList(); 
        }

        public async Task<DiasPreferencia> ConsultarPorUsuarioId(string usuarioId)
        {
            return await _diasPreferenciaRepository.ConsultarPorUsuarioId(usuarioId);
           
        }


        public async Task<DiasPreferencia?> Atualizar(DiasPreferencia dias)
        {
            return await _diasPreferenciaRepository.Atualizar(dias);
        }

         public async Task<DiasPreferencia?> ConsultarId(string id)
        {
            var dias = await _diasPreferenciaRepository.ConsultarTodos();
            return dias.FirstOrDefault(e => e.Id == id);
        }

        public async Task Excluir(string id)
        {
            await _diasPreferenciaRepository.Excluir(id);
        }
    }
}
