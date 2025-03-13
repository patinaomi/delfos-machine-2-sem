using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    [Route("Chat")]
    public class ChatController : Controller
    {
        private readonly HttpClient _httpClient;

        public ChatController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("GetMenu")]
        public async Task<IActionResult> GetMenu()
        {
            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

            if (string.IsNullOrEmpty(idUsuario))
            {
                return Unauthorized("Usuário não logado.");
            }

            var response = await _httpClient.GetAsync($"http://localhost:3001/Usuario/ConsultarUsuarioId/{idUsuario}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Erro ao consultar usuário.");
            }

            var usuario = await response.Content.ReadFromJsonAsync<Usuario>();

            var menuOpcoes = new
            {
                response = $"Olá, {usuario?.Nome ?? "usuário"}! Escolha uma opção para os dados pessoais:\n" +
                           "1️⃣ Dados Pessoais\n" +
                           "2️⃣ Endereço de Preferência de atendimento\n" +
                           "3️⃣ Turno\n" +
                           "4️⃣ Dias Peferência\n" +
                           "5️⃣ Horário\n" +
                           "Digite o número da opção desejada."
            };

            return Ok(menuOpcoes);
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage message)
        {
            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

            if (string.IsNullOrEmpty(idUsuario))
            {
                return Unauthorized("Usuário não logado.");
            }

            var response = await _httpClient.GetAsync($"http://localhost:3001/Usuario/ConsultarUsuarioId/{idUsuario}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Erro ao consultar usuário.");
            }

            var usuario = await response.Content.ReadFromJsonAsync<Usuario>();

            string respostaApi;
            Dictionary<string, string> dadosFiltrados = null;
            string campoEscolhido = string.Empty; 

            switch (message.Text.Trim())
            {
                case "1":
                    // Pesquisar os dados pessoais do usuário no banco
                    var responseDados = await _httpClient.GetAsync($"http://localhost:3001/Usuario/ConsultarUsuarioId/{idUsuario}");

                    if (!responseDados.IsSuccessStatusCode)
                    {
                        return StatusCode((int)responseDados.StatusCode, "Erro ao consultar dados pessoais.");
                    }

                    var dadosPessoais = await responseDados.Content.ReadFromJsonAsync<Dictionary<string, string>>();

                    if (dadosPessoais == null || dadosPessoais.Count == 0)
                    {
                        return Ok(new { response = "Nenhum dado pessoal encontrado para atualização." });
                    }

                    // 🔹 Lista de campos que NÃO devem aparecer
                    var camposRestritos = new HashSet<string> { "id", "perfil", "senha", "token" };

                    // Filtrar os dados removendo os campos restritos
                    dadosFiltrados = dadosPessoais
                        .Where(campo => !camposRestritos.Contains(campo.Key))
                        .ToDictionary(campo => campo.Key, campo => campo.Value);

                    if (dadosFiltrados.Count == 0)
                    {
                        return Ok(new { response = "Nenhum dado editável disponível." });
                    }

                    // Criar o menu dinâmico com os campos disponíveis
                    var opcoesMenu = "📋 Selecione um campo para atualizar:\n\n";
                    int opcao = 1;

                    foreach (var campo in dadosFiltrados)
                    {
                        opcoesMenu += $"{opcao}️⃣ {campo.Key}: {campo.Value}\n";
                        opcao++;
                    }

                    opcoesMenu += "\nDigite o número do campo que deseja atualizar.";

                    return Ok(new { response = opcoesMenu });

                
                default:
                    respostaApi = "Desculpe, não entendi sua escolha. Digite **menu** para ver as opções.";
                    break;
            }

            return await GetMenu();
        }
    }

    public class ChatMessage
    {
        public required string Text { get; set; }
    }
}
