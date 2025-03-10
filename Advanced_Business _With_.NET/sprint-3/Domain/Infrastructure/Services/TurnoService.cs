using Project.Infrastructure.Interfaces;
using Project.Models;

namespace Project.Application.Services
{
    public class TurnoService : ITurnoService
    {
        private readonly ITurnoRepository _turnoRepository;

        public TurnoService(ITurnoRepository turnoRepository)
        {
            _turnoRepository = turnoRepository;
        }

        public async Task<Turno> Criar(Turno turno)
        {
            return await _turnoRepository.Criar(turno);
        }

        public async Task<List<Turno>> ConsultarTodos()
        {
            var turnos = await _turnoRepository.ConsultarTodos();
            return turnos.ToList(); 
        }

        public async Task<Turno> ConsultarPorUsuarioId(string usuarioId)
        {
            return await _turnoRepository.ConsultarPorUsuarioId(usuarioId);
           
        }


        public async Task<Turno?> Atualizar(Turno turno)
        {
            return await _turnoRepository.Atualizar(turno);
        }

         public async Task<Turno?> ConsultarId(string id)
        {
            var turnos = await _turnoRepository.ConsultarTodos();
            return turnos.FirstOrDefault(e => e.Id == id);
        }

        public async Task Excluir(string id)
        {
            await _turnoRepository.Excluir(id);
        }
    }
}
