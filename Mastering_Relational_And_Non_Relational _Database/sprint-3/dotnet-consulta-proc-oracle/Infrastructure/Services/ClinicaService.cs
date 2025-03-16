using LexusTech.Infrastructure.Interfaces;
using LexusTech.Models;

namespace LexusTech.Application.Services
{
    public class ClinicaService : IClinicaService
    {
        private readonly IClinicaRepository _clinicaRepository;

        public ClinicaService(IClinicaRepository clinicaRepository)
        {
            _clinicaRepository = clinicaRepository;
        }

        public async Task<Clinica> Criar(Clinica clinica)
        {
            return await _clinicaRepository.Criar(clinica);
        }

        public async Task<List<Clinica>> ConsultarTodos()
        {
            var clinicas = await _clinicaRepository.ConsultarTodos();
            return clinicas.ToList(); 
        }

        public async Task<Clinica> BuscarCliente(int id)
        {
            return await _clinicaRepository.BuscarCliente(id);
        }


        public async Task<Clinica> Atualizar(Clinica clinica)
        {
            return await _clinicaRepository.Atualizar(clinica);
        }

        public async Task<Clinica> Excluir(Clinica clinica)
        {
            return await _clinicaRepository.Excluir(clinica);
        }
    }
}
