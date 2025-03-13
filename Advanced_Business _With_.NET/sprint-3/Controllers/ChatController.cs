using Microsoft.AspNetCore.Mvc;
using Project.Application.Services;
using Project.Infrastructure.Interfaces;
using Project.Models;

namespace Project.Controllers
{
    [Route("Chat")]
    public class ChatController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IChatService _chatService;

        public ChatController(HttpClient httpClient, IChatService chatService)
        {
            _httpClient = httpClient;
            _chatService = chatService;
            
        }

        [ApiExplorerSettings(IgnoreApi = true)]
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

        /// <summary>
        ///     Envia uma conversa para o chat e avalia a resposta
        /// </summary>
        /// <remarks>
        /// 
        /// ## Exemplo de requisição de conversa com pergunta e resposta
        /// 
        /// Use este endpoint, se o objetivo é testar o fluxo de pergunta e resposta do chatbot.
        /// 
        /// ### Este endpoint ficará em construção até a última sprint.
        /// 
        /// </remarks>
        ///
        /// <response code="200">Requisição realizada com sucesso</response>
        /// <response code="400">Dados inválidos fornecidos</response>
        /// <response code="500">Erro interno do servidor</response>
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

    /// <summary>
    ///     Cria uma nova conversa no banco de dados
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Cadastrar nova conversa do CHATBOT no banco
    /// 
    /// Use este endPoint para cadastrar as interações do usuário com o chatbot para entendermos o comportamento e as dúviadas.
    /// 
    /// ### Campos que devem ser utilizados para criar um novo dia:
    /// - **idUsuario** string : Id do usuário que esta em contato com o chat
    /// - **pergunta** string : Pergunta realizada
    /// - **resposta** string : Resposta obtida na interação
    /// - **data** string : Data do contato
    /// 
    /// ### Campos que não devem ser utilizados para criar um novo dia:
    /// - **id** : Id do dia que será gerado automaticamente
    /// 
    /// 
    /// ### Exemplo de body para requisição:
    /// ```json
    ///     {
    ///         "idUsuario": "67d0a90a9147cc4c1c99cce2",
    ///         "pergunta": "Como faço para atualizar o meu cadastro?",
    ///         "resposta": "Digite no menu a opção deseja que seu cadastro poderá ser alterado",
    ///         "data": "2025-03-13"
    ///     }
    /// ```
    /// 
    /// ### Exemplo de resposta quando o cadastro for efetuado:
    /// 
    /// ```json
    ///     {   
    ///         "id": "67cc95b32811515d37220000", -- gerado pelo banco de dados
    ///         "idUsuario": "67d0a90a9147cc4c1c99cce2",
    ///         "pergunta": "Como faço para atualizar o meu cadastro?",
    ///         "resposta": "Digite no menu a opção desejada",
    ///         "data": "2025-03-13"
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Requisição realizada com sucesso</response>
    /// <response code="201">Dados criadoo com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost("CadastrarConversa")]
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CadastrarConversa([FromBody] Chat chat)
    {
        if (ModelState.IsValid)
        {
            await _chatService.Criar(chat);
            return CreatedAtAction(nameof(ConsultarTodasConversas), new { id = chat.Id }, chat); 
        }
        return BadRequest(ModelState); 
    }

    // Rota de API
    /// <summary>
    ///     Consultar a lista com todo os usuários.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar todos os registros de usuários do banco de dados
    /// 
    /// Use este endPoint se seu objetivo é recuperar todos os registros de usuários do banco de dados
    /// 
    /// ### Campos disponiveis para consultar os usuários:
    /// - **id** : Id do dia que será gerado automaticamente
    /// - **nome** string : Nome completo do usuário
    /// - **cpf** string : CPF oficial do usuário com 11 digitos
    /// - **telefone** string : Telefone de contato
    /// - **email** string : Email principal de contato
    /// - **senha** string : Senha de acesso (teremos hash para proteger a senha)
    /// - **perfil** string : Todos os usuários cadastrados nesta rota, nasceram com perfil padrão = "Comum"
    /// 
    /// Exemplo de body de resposta, sempre será uma lista com todos os usuários no banco:
    /// ```json
    ///     [
    ///         { 
    ///            "id": "67cc95b32811515d37220000", -- gerado pelo banco de dados
    ///             "idUsuario": "67d0a90a9147cc4c1c99cce2",
    ///             "pergunta": "Como faço para atualizar o meu cadastro?",
    ///             "resposta": "Digite no menu a opção deseja que seu cadastro poderá ser alterado",
    ///             "data": "2025-03-13"
    ///         },
    ///         {
    ///             "id": "67cca0540924d08d2c4b7819",
    ///             "idUsuario": "67d0a90a9147cc4c1c99cce2",
    ///             "pergunta": "Como faço para atualizar o meu endereço?",
    ///             "resposta": "Digite no menu a opção desejada",
    ///             "data": "2025-05-20"
    ///         }
    ///     ]
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Dado criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarTodasConversas")]
    [Produces("application/json")]
    public async Task<IActionResult> ConsultarTodasConversas()
    {
        var usuarios = await _chatService.ConsultarTodos();
        return Ok(usuarios);
    }

    /// <summary>
    ///     Consultar um único registro de conversa
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar uma única conversa no banco, sendo consultado pelo ID do banco de dados
    /// 
    /// Use este endpoint quando precisar consultar somente um registro com todos campos específicos.
    /// 
    /// ### Campos que devem ser utilizados para consultar de conversa do chatbot:
    /// 
    /// - **id**: ID do banco
    /// 
    ///  ### Exemplo de body para requisição:
    ///  
    /// ```json
    ///     "id": "67cc95b32811515d372209ce"
    /// ```
    /// 
    /// ### Exemplo de body que receberemos como resposta:
    /// 
    /// ```json
    ///    {
    ///         "id": "67cc95b32811515d37220000", 
    ///         "idUsuario": "67d0a90a9147cc4c1c99cce2",
    ///         "pergunta": "Como faço para atualizar o meu cadastro?",
    ///         "resposta": "Digite no menu a opção desejada",
    ///         "data": "2025-03-13"
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Conversa consultada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarConversaId/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarConversaId(string id)
    {
        var chat = await _chatService.ConsultarId(id);

        if (chat == null)
        {
            return NotFound(new { message = "Conversa não encontrado." });
        }

        return Ok(chat);
    }
    }

    public class ChatMessage
    {
        public required string Text { get; set; }
    }

}
