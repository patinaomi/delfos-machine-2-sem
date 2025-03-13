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

    }
}
