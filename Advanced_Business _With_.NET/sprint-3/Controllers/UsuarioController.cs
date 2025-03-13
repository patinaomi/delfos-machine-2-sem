using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("Usuario")] 
public class UsuarioController : Controller
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    // usar essaa tag para permitir que todos possam fazer cadastrado, mas quem não estiver logado, não vai conseguir acessar nada.
    [AllowAnonymous]
    [HttpGet("Criar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Criar()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost("Criar")]
    [ValidateAntiForgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Criar([Bind("Id,Nome,CPF,Sobrenome,Telefone,Email,Senha,Perfil")] Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            await _usuarioService.Criar(usuario);
            TempData["SuccessMessage"] = "Usuário cadastrado com sucesso!";
            return RedirectToAction("Mensagem");
        }
        return View(usuario);
    }

    [HttpGet("Mensagem")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Mensagem()
    {
        return View();
    }


    /// <summary>
    ///     Cria um novo usuário.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Cadastrar novo usuário no banco de dados
    /// 
    /// Use este endPoint para cadastrar um usuário. Somente usuários cadastrados podem efetuar o Login na plataforma.
    /// 
    /// ### Campos que devem ser utilizados para criar um novo dia:
    /// - **nome** string : Nome completo do usuário
    /// - **cpf** string : CPF oficial do usuário com 11 digitos
    /// - **telefone** string : Telefone de contato
    /// - **email** string : Email principal de contato
    /// - **senha** string : Senha de acesso (teremos hash para proteger a senha)
    /// - **perfil** string : Todos os usuários cadastrados nesta rota, nasceram com perfil padrão = "Comum"
    /// 
    /// ### Campos que não devem ser utilizados para criar um novo dia:
    /// - **id** : Id do dia que será gerado automaticamente
    /// - **idUsuario** : IdUsuario será uma cópia do Id e utilizado nas demais tabelas. Será gerado de forma automática.   
    /// 
    /// 
    /// ### Exemplo de body para requisição:
    /// ```json
    ///     {
    ///         "nome": "João",
    ///         "cpf": "12345678910",
    ///         "sobrenome": "Silva",
    ///         "email": "joao@exemplo.com",
    ///         "senha": "senha123",
    ///         "perfil": "Comum"
    ///     }
    /// ```
    /// 
    /// ### Exemplo de resposta quando o cadastro for efetuado:
    /// 
    /// ```json
    ///     {   
    ///         "id": "67cc95b32811515d37220000", -- gerado pelo banco de dados
    ///         "nome": "Delfos Machine",
    ///         "cpf": "12345678910",
    ///         "telefone": "11975776758",
    ///         "email": "delfos@delfos.com",
    ///         "senha": "123456",
    ///         "perfil": "Comum"
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Requisição realizada com sucesso</response>
    /// <response code="201">Usuário criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost("CadastrarUsuario")]
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CadastrarUsuario([FromBody] Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            await _usuarioService.Criar(usuario);
            return CreatedAtAction(nameof(ConsultarTodosUsuarios), new { id = usuario.Id }, usuario); 
        }
        return BadRequest(ModelState); 
    }


    // Rota de View
    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var usuarios = await _usuarioService.ConsultarTodos(); 
        return View(usuarios); 
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
    ///             "id": "67cc95b32811515d372209ce",
    ///             "nome": "claudio",
    ///             "cpf": "12345678910",
    ///             "telefone": "11958757740",
    ///             "email": "claudio_cssp@hotmail.com",
    ///             "senha": "123456"
    ///         },
    ///         {
    ///             "id": "67cca0540924d08d2c4b7819",
    ///             "nome": "Caio",
    ///             "cpf": "12345678910",
    ///             "telefone": "11958757740",
    ///             "email": "caio@delfos.com",
    ///             "senha": "123456"
    ///         }
    ///     ]
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Usuário criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarTodosUsuarios")]
    [Produces("application/json")]
    public async Task<IActionResult> ConsultarTodosUsuarios()
    {
        var usuarios = await _usuarioService.ConsultarTodos();
        return Ok(usuarios);
    }

    /// <summary>
    ///     Consultar um único registro de usuário
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar um único usuário no banco, sendo consultado pelo ID do banco de dados
    /// 
    /// Use este endpoint quando precisar consultar somente um registro com todos campos específicos.
    /// 
    /// ### Campos que devem ser utilizados para consultar um usuário:
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
    ///         "id": "67cc95b32811515d372209ce",
    ///         "nome": "Claudio",
    ///         "cpf": "12345678910",
    ///         "telefone": "11958755567",
    ///         "email": "delfos@delfosmachine.com",
    ///         "senha": "123456",
    ///         "perfil": "Comum"
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Usuário consultado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarUsuarioId/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarUsuarioId(string id)
    {
        var usuario = await _usuarioService.ConsultarId(id);

        if (usuario == null)
        {
            return NotFound(new { message = "Usuário não encontrado." });
        }

        return Ok(usuario);
    }

    // View para atualizar um usuário
    [HttpGet("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar()
    {
        //var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var usuario = await _usuarioService.ConsultarId(userIdString);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    [HttpPost("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar(Usuario usuario)
    {
        if (!ModelState.IsValid)
        {
            return View(usuario);
        }

        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var usuarioExistente = await _usuarioService.ConsultarId(userIdString);

        if (usuarioExistente == null)
        {
            return NotFound();
        }

        usuarioExistente.Nome = usuario.Nome;
        usuarioExistente.CPF = usuario.CPF;
        usuarioExistente.Telefone = usuario.Telefone;
        usuarioExistente.Email = usuario.Email;
        usuarioExistente.Senha = usuario.Senha;

        await _usuarioService.Atualizar(usuarioExistente);

        TempData["SuccessMessage"] = "Dados atualizados com sucesso!";
        //return RedirectToAction("MensagemAtualizacao");
        return View(usuarioExistente);
    }

    [HttpGet("MensagemAtualizacao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemAtualizacao()
    {
        return View();
    }

    /// <summary>
    ///     Atualiza os dados completos existente do usuário, com base no ID do banco de dados
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Atualizar todas as informações do usuário no banco
    /// 
    /// Use este endpoint se o objetivo for atualizar todos os campos para o usuário no cadastro. Se for parcial, utilize outro endPoint.
    /// 
    /// ### Exemplo de requisição
    /// 
    /// ```json
    ///     {   
    ///         "id": "67ce4b3d61760e36f862dd59",
    ///         "nome": "Patricia Delfos",
    ///         "cpf": "12345678910",
    ///         "telefone": "1155122066",
    ///         "email": "patricia@delfos.com", 
    ///         "senha": "123456",
    ///         "perfil": "Comum"
    ///     }
    /// ``` 
    /// 
    /// ### Se preencher alguns campos e outros não, estes campos serão preenchidos com dados inválidos e default criado pelo sistema.
    /// </remarks>
    /// 
    /// 
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do usuário no banco de dados.</param>
    /// <param name="usuario">Dados do usuário a serem atualizados.</param>
    /// <response code="200">Usuário atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="401">Usuário não autorizado</response>
    /// <response code="404">Usuário não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut("AtualizarUsuario/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> AtualizarUsuario(string id, [FromBody] Usuario usuario)
    {
        if (string.IsNullOrEmpty(id) || usuario == null || id != usuario.Id)
        {
            return BadRequest("Id não corresponde ao fornecido.");
        }

        var usuarioExistente = await _usuarioService.ConsultarId(id);

        if (usuarioExistente == null)
        {
            return NotFound();
        }


        usuarioExistente.Nome = usuario.Nome;
        usuarioExistente.CPF = usuario.CPF;
        usuarioExistente.Telefone = usuario.Telefone;
        usuarioExistente.Email = usuario.Email;
        usuarioExistente.Senha = usuario.Senha;

        await _usuarioService.Atualizar(usuarioExistente);

        return Ok(usuarioExistente); 
    }


    [HttpGet("ConfirmarExcluir/{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmarExcluir(string id)
    {
        var usuario = await _usuarioService.ConsultarId(id);
        
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    /// <summary>
    ///     Atualiza parcialmente os dados de uma usuário existente
    /// </summary>
    /// 
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do usuário a ser atualizada</param>
    /// 
    /// <remarks>
    /// 
    /// ## Atualização Parcial de um Usuário
    /// 
    /// Use este endpoint quando precisar atualizar apenas alguns campos específicos de um usuário,
    /// sem a necessidade de enviar todos os dados.
    /// 
    /// ### Campos que podem ser atualizados:
    /// - **nome**: Nome do usuário
    /// - **cpf** string : CPF oficial do usuário com 11 digitos
    /// - **telefone**: Número de telefone para contato
    /// - **email**: Endereço de email para contato
    /// - **senha**: Senha de acesso (será criptografada)
    /// 
    /// ### Campos que não podem ser atualizados:
    /// - **Perfil**: Perfil = Comum pois a rota utilizada foi a de clientes e não de parceiros
    /// 
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    /// {
    ///     "id": "67cc95b32811515d372209ce",
    ///     "email": "novo.email@delfo.com.br"
    /// }
    /// ```
    /// 
    /// Somente os campos incluídos no corpo da requisição serão atualizados.
    /// 
    /// ### Exemplo de resposta da requisição:
    /// 
    /// ```json
    /// {
    ///     "id": "67ce4b3d61760e36f862dd59",
    ///     "nome": "Delfos",
    ///     "cpf": "12345678910",
    ///     "telefone": "string",
    ///     "email": "delfos@delfos.com", -- Validação pode ser feita pelo campo informado!
    ///     "senha": "string",
    ///     "perfil": "Comum"
    /// }
    /// ```
    /// </remarks>
    /// 
    /// <param name="camposParaAtualizar"></param>
    /// 
    /// <response code="200">Usuário atualizada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Usuário não encontrada</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPatch("AtualizarParcial/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarParcial(string id, [FromBody] Dictionary<string, object> camposParaAtualizar)
    {
        if (string.IsNullOrEmpty(id) || camposParaAtualizar == null || !camposParaAtualizar.Any())
        {
            return BadRequest("Id dos campos     ///  atualização são necessários.");
        }

        var usuarioAtualizado = await _usuarioService.AtualizarParcial(id, camposParaAtualizar);

        if (usuarioAtualizado == null)
        {
            return NotFound("Usuário não encontrada.");
        }

        return Ok(usuarioAtualizado);
    }


    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Excluir(string id)
    {
        var usuario = await _usuarioService.ConsultarId(id);
        
        if (usuario != null)
        {
            // Exclui o usuário do banco de dados
            await _usuarioService.Excluir(id);

            // Desloga o usuário
            //await _context.SaveChangesAsync();
            await HttpContext.SignOutAsync();
            
            // Redireciona para a página de login ou para onde você preferir
            TempData["SuccessMessage"] = "Usuário excluído com sucesso.";
            return RedirectToAction("MensagemExclusao", "Usuario"); 
        }

        TempData["ErrorMessage"] = "Usuário não encontrado.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("MensagemExclusao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemExclusao()
    {
        return View();
    }

    /// <summary>
    ///     Excluir os Usuário do banco de dados.
    /// </summary>
    ///
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do Usuário a ser excluído</param>
    /// 
    /// <remarks>
    /// 
    /// ## Excluir um Usuário do banco de dados e dos cadastros.
    /// 
    /// ### Exemplo da requisição para excluir um Usuário:
    /// 
    /// ```json
    ///     {
    ///         "id": "67cc95b32811515d372209ce",
    ///     }
    /// ```
    /// 
    /// ### Exemplo da resposta para excluir um Usuário:
    /// 
    /// ```json
    ///     {
    ///         "message": "Usuário excluído com sucesso."
    ///     }
    /// ``` 
    /// 
    /// Uma vez excluida da base, não tem reversão desta ação.
    /// </remarks>
    /// 
    /// <response code="200">Usuário criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete("ExcluirUsuario/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> ExcluirUsuario(string id)
    {
        var usuario = await _usuarioService.ConsultarId(id);
        
        if (usuario == null)
        {
            return NotFound();
        }

        await _usuarioService.Excluir(id);

        return Ok(new { message = "Usuário excluído com sucesso." });  
    }

}