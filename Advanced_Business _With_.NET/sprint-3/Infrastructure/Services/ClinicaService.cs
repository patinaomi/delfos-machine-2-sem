using System.Reflection;
using Project.Infrastructure.Interfaces;
using Project.Models;

namespace Project.Application.Services
{
    public class ClinicaService : IClinicaService
    {
        private readonly IClinicaRepository _clinicaRepository;

        public ClinicaService(IClinicaRepository ClinicaRepository)
        {
            _clinicaRepository = ClinicaRepository;
        }

        public async Task<Clinica> Criar(Clinica Clinica)
        {
            return await _clinicaRepository.Criar(Clinica);
        }

        public async Task<List<Clinica>> ConsultarTodos()
        {
            var clinicas = await _clinicaRepository.ConsultarTodos();
            return clinicas.ToList(); 
        }

        public async Task<Clinica> ConsultarId(string id)
        {
            return await _clinicaRepository.ConsultarId(id);
        }


        public async Task<Clinica?> Atualizar(Clinica Clinica)
        {
            return await _clinicaRepository.Atualizar(Clinica);
        }

        public async Task<Clinica?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            return await _clinicaRepository.AtualizarParcial(id, camposParaAtualizar);
        }


        public async Task Excluir(string id)
        {
            await _clinicaRepository.Excluir(id);
        }
    }
}
