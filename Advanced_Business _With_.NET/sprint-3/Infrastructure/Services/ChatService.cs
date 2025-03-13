using Project.Infrastructure.Interfaces;
using Project.Models;

namespace Project.Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<Chat> Criar(Chat chat)
        {
            return await _chatRepository.Criar(chat);
        }

        public async Task<List<Chat>> ConsultarTodos()
        {
            var chats = await _chatRepository.ConsultarTodos();
            return chats.ToList(); 
        }

        public async Task<Chat> ConsultarId(string id)
        {
            return await _chatRepository.ConsultarId(id);
        }


        public async Task<Chat?> Atualizar(Chat chat)
        {
            return await _chatRepository.Atualizar(chat);
        }

        public async Task<Chat?> AtualizarParcial(string id, Dictionary<string, object> camposParaAtualizar)
        {
            return await _chatRepository.AtualizarParcial(id, camposParaAtualizar);
        }

        public async Task Excluir(string id)
        {
            await _chatRepository.Excluir(id);
        }
    }
}
