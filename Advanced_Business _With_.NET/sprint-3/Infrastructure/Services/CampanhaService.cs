using Project.Infrastructure.Interfaces;
using Project.Models;

namespace Project.Application.Services
{
    public class CampanhaService : ICampanhaService
    {
        private readonly ICampanhaRepository _campanhaRepository;

        public CampanhaService(ICampanhaRepository campanhaRepository)
        {
            _campanhaRepository = campanhaRepository;
        }

        public async Task<Campanha> Criar(Campanha campanha)
        {
            return await _campanhaRepository.Criar(campanha);
        }

        public async Task<List<Campanha>> ConsultarTodos()
        {
            var campanhas = await _campanhaRepository.ConsultarTodos();
            return campanhas.ToList(); 
        }

        public async Task<Campanha> ConsultarId(string id)
        {
            return await _campanhaRepository.ConsultarId(id);
        }


        public async Task<Campanha?> Atualizar(Campanha campanha)
        {
            return await _campanhaRepository.Atualizar(campanha);
        }

        public async Task<Campanha?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            return await _campanhaRepository.AtualizarParcial(id, camposParaAtualizar);
        }

        public async Task Excluir(string id)
        {
            await _campanhaRepository.Excluir(id);
        }
    }
}
