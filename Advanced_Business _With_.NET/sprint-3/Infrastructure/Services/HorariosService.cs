using Project.Infrastructure.Interfaces;
using Project.Models;

namespace Project.Application.Services
{
    public class HorariosService : IHorariosService
    {
        private readonly IHorariosRepository _horariosRepository;

        public HorariosService(IHorariosRepository horarioRepository)
        {
            _horariosRepository = horarioRepository;
        }

        public async Task<Horarios> Criar(Horarios horario)
        {
            return await _horariosRepository.Criar(horario);
        }

        public async Task<List<Horarios>> ConsultarTodos()
        {
            var horarios = await _horariosRepository.ConsultarTodos();
            return horarios.ToList(); 
        }

        public async Task<Horarios> ConsultarPorUsuarioId(string usuarioId)
        {
            return await _horariosRepository.ConsultarPorUsuarioId(usuarioId);
           
        }


        public async Task<Horarios?> Atualizar(Horarios horario)
        {
            return await _horariosRepository.Atualizar(horario);
        }

         public async Task<Horarios?> ConsultarId(string id)
        {
            var horarios = await _horariosRepository.ConsultarTodos();
            return horarios.FirstOrDefault(e => e.Id == id);
        }

        public async Task Excluir(string id)
        {
            await _horariosRepository.Excluir(id);
        }
    }
}
