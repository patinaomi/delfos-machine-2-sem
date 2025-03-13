using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("Medico")] 
public class MedicoController : Controller
{
    private readonly IMedicoService _medicoService;

    public MedicoController(IMedicoService medicoService)
    {
        _medicoService = medicoService;
    }

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
    public async Task<IActionResult> Criar([Bind("Id,Nome,CPF,Sobrenome,Telefone,Email,Senha,Perfil")] Medico medico)
    {
        if (ModelState.IsValid)
        {
            await _medicoService.Criar(medico);
            TempData["SuccessMessage"] = "Médico cadastrado com sucesso!";
            return RedirectToAction("Mensagem");
        }
        return View(medico);
    }

    [HttpGet("Mensagem")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Mensagem()
    {
        return View();
    }

    /// <summary>
    ///     Cria um novo Médico.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Cadastrar novo Médico no banco de dados
    /// 
    /// Use este endPoint para cadastrar um Médico. Somente Médicos cadastrados podem efetuar o Login na plataforma.
    /// 
    /// ### Campos que devem ser utilizados para criar um novo dia:
    /// - **nome** string : Nome completo do Médico
    /// - **cpf** string : CPF oficial do Médico com 11 digitos
    /// - **telefone** string : Telefone de contato
    /// - **email** string : Email principal de contato
    /// - **senha** string : Senha de acesso (teremos hash para proteger a senha)
    /// - **perfil** string : Todos os Médicos cadastrados nesta rota, nasceram com perfil padrão = "Medico"
    /// - **especilidade** string : Especilidade principal de contato
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
    ///         "perfil": "Comum",
    ///         "especilidade" : "Clinica Geral"
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
    ///         "perfil": "Comum",
    ///         "especilidade" : "Clinica Geral"
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Requisição realizada com sucesso</response>
    /// <response code="201">Médico criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost("CadastrarMedico")]
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CadastrarMedico([FromBody] Medico medico)
    {
        if (ModelState.IsValid)
        {
            await _medicoService.Criar(medico);
            return CreatedAtAction(nameof(ConsultarTodosMedicos), new { id = medico.Id }, medico); 
        }
        return BadRequest(ModelState); 
    }

    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var medicos = await _medicoService.ConsultarTodos(); 
        return View(medicos); 
    }

    /// <summary>
    ///     Consultar a lista com todo os Médicos.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar todos os registros de Médicos do banco de dados
    /// 
    /// Use este endPoint se seu objetivo é recuperar todos os registros de Médicos do banco de dados
    /// 
    /// ### Campos disponiveis para consultar os Médicos:
    /// - **id** : Id do dia que será gerado automaticamente
    /// - **nome** string : Nome completo do Médico
    /// - **cpf** string : CPF oficial do Médico com 11 digitos
    /// - **telefone** string : Telefone de contato
    /// - **email** string : Email principal de contato
    /// - **senha** string : Senha de acesso (teremos hash para proteger a senha)
    /// - **perfil** string : Todos os Médicos cadastrados nesta rota, nasceram com perfil padrão = "Comum"
    /// - **especilidade** string : Especilidade principal de contato
    /// 
    /// Exemplo de body de resposta, sempre será uma lista com todos os Médicos no banco:
    /// ```json
    ///     [
    ///         { 
    ///             "id": "67cc95b32811515d372209ce",
    ///             "nome": "claudio",
    ///             "cpf": "12345678910",
    ///             "telefone": "11958757740",
    ///             "email": "claudio_cssp@hotmail.com",
    ///             "senha": "123456",
    ///             "especilidade" : "Clinica Geral"
    ///         },
    ///         {
    ///             "id": "67cca0540924d08d2c4b7819",
    ///             "nome": "Caio",
    ///             "cpf": "12345678910",
    ///             "telefone": "11958757740",
    ///             "email": "caio@delfos.com",
    ///             "senha": "123456",
    ///             "especilidade" : "Clinica Geral"
    ///         }
    ///     ]
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Médico criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarTodosMedicos")]
    [Produces("application/json")]
    public async Task<IActionResult> ConsultarTodosMedicos()
    {
        var medicos = await _medicoService.ConsultarTodos();
        return Ok(medicos);
    }

    /// <summary>
    ///     Consultar um único registro de Médico
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar um único Médico no banco, sendo consultado pelo ID do banco de dados
    /// 
    /// Use este endpoint quando precisar consultar somente um registro com todos campos específicos.
    /// 
    /// ### Campos que devem ser utilizados para consultar um Médico:
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
    ///         "perfil": "Comum",
    ///         "especilidade" : "Clinica Geral"
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Médico consultado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarMedicoId/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarMedicoId(string id)
    {
        var medico = await _medicoService.ConsultarId(id);

        if (medico == null)
        {
            return NotFound(new { message = "Médico não encontrado." });
        }

        return Ok(medico);
    }

    [HttpGet("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar()
    {
        
        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var medico = await _medicoService.ConsultarId(userIdString);
        if (medico == null)
        {
            return NotFound();
        }

        return View(medico);
    }

    [HttpPost("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar(Medico medico)
    {
        if (!ModelState.IsValid)
        {
            return View(medico);
        }

        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var medicoExistente = await _medicoService.ConsultarId(userIdString);

        if (medicoExistente == null)
        {
            return NotFound();
        }

        medicoExistente.Nome = medico.Nome;
        medicoExistente.CPF = medico.CPF;
        medicoExistente.Telefone = medico.Telefone;
        medicoExistente.Email = medico.Email;
        medicoExistente.Senha = medico.Senha;

        await _medicoService.Atualizar(medicoExistente);

        TempData["SuccessMessage"] = "Médico atualizado com sucesso!";
        return RedirectToAction("MensagemAtualizacao");
    }

    [HttpGet("MensagemAtualizacao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemAtualizacao()
    {
        return View();
    }

    /// <summary>
    ///     Atualiza os dados completos existente do Médico, com base no ID do banco de dados
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Atualizar todas as informações do Médico no banco
    /// 
    /// Use este endpoint se o objetivo for atualizar todos os campos para o Médico no cadastro. Se for parcial, utilize outro endPoint.
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
    ///         "perfil": "Comum",
    ///         "especilidade" : "Clinica Geral"
    ///     }
    /// ``` 
    /// 
    /// ### Se preencher alguns campos e outros não, estes campos serão preenchidos com dados inválidos e default criado pelo sistema.
    /// </remarks>
    /// 
    /// 
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do Médico no banco de dados.</param>
    /// <param name="medico">Dados do Médico a serem atualizados.</param>
    /// <response code="200">Médico atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="401">Médico não autorizado</response>
    /// <response code="404">Médico não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut("AtualizarMedico/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> AtualizarMedico(string id, [FromBody] Medico medico)
    {
        if (string.IsNullOrEmpty(id) || medico == null || id != medico.Id)
        {
            return BadRequest("Id do Médico não corresponde ao fornecido.");
        }

        var medicoExistente = await _medicoService.ConsultarId(id);

        if (medicoExistente == null)
        {
            return NotFound();
        }


        medicoExistente.Nome = medico.Nome;
        medicoExistente.CPF = medico.CPF;
        medicoExistente.Telefone = medico.Telefone;
        medicoExistente.Email = medico.Email;
        medicoExistente.Senha = medico.Senha;

        await _medicoService.Atualizar(medicoExistente);

        return Ok(medicoExistente); 
    }

    [HttpGet("ConfirmarExcluir/{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmarExcluir(string id)
    {
        var medico = await _medicoService.ConsultarId(id);
        
        if (medico == null)
        {
            return NotFound();
        }

        return View(medico);
    }

    /// <summary>
    ///     Atualiza parcialmente os dados de uma Médico existente
    /// </summary>
    /// 
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do Médico a ser atualizada</param>
    /// 
    /// <remarks>
    /// 
    /// ## Atualização Parcial de um Médico
    /// 
    /// Use este endpoint quando precisar atualizar apenas alguns campos específicos de um Médico,
    /// sem a necessidade de enviar todos os dados.
    /// 
    /// ### Campos que podem ser atualizados:
    /// - **nome**: Nome do Médico
    /// - **cpf** string : CPF oficial do Médico com 11 digitos
    /// - **telefone**: Número de telefone para contato
    /// - **email**: Endereço de email para contato
    /// - **senha**: Senha de acesso (será criptografada)
    /// - **especilidade** string : Especilidade principal de contato
    /// 
    /// ### Campos que não podem ser atualizados:
    /// - **Perfil**: Perfil = Comum pois a rota utilizada foi a de clientes e não de parceiros
    /// 
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    /// {
    ///     "id": "67cc95b32811515d372209ce",
    ///     "email": "novo.email@clinica.com.br"
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
    ///     "perfil": "Comum",
    ///     "especilidade" : "Clinica Geral"
    /// }
    /// ```
    /// </remarks>
    /// 
    /// <param name="camposParaAtualizar"></param>
    /// 
    /// <response code="200">Médico atualizada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Médico não encontrada</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPatch("AtualizarParcial/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarClinica(string id, [FromBody] Dictionary<string, object> camposParaAtualizar)
    {
        if (string.IsNullOrEmpty(id) || camposParaAtualizar == null || !camposParaAtualizar.Any())
        {
            return BadRequest("Id da Clinica e/ou campos para atualização são necessários.");
        }

        var medicoAtualizado = await _medicoService.AtualizarParcial(id, camposParaAtualizar);

        if (medicoAtualizado == null)
        {
            return NotFound("Médico não encontrada.");
        }

        return Ok(medicoAtualizado);
    }


    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Excluir(string id)
    {
        var medico = await _medicoService.ConsultarId(id);
        
        if (medico != null)
        {
            await _medicoService.Excluir(id);
            //await HttpContext.SignOutAsync();
            
            TempData["SuccessMessage"] = "Médico excluído com sucesso.";
            //return RedirectToAction("MensagemExclusao", "Medico"); 
        }

        TempData["ErrorMessage"] = "Médico não encontrado.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("MensagemExclusao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemExclusao()
    {
        return View();
    }

    /// <summary>
    ///     Excluir os Médico do banco de dados.
    /// </summary>
    ///
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do Médico a ser excluído</param>
    /// 
    /// <remarks>
    /// 
    /// ## Excluir um Médico do banco de dados e dos cadastros.
    /// 
    /// ### Exemplo da requisição para excluir um Médico:
    /// 
    /// ```json
    ///     {
    ///         "id": "67cc95b32811515d372209ce",
    ///     }
    /// ```
    /// 
    /// ### Exemplo da resposta para excluir um Médico:
    /// 
    /// ```json
    ///     {
    ///         "message": "Médico excluído com sucesso."
    ///     }
    /// ``` 
    /// 
    /// Uma vez excluida da base, não tem reversão desta ação.
    /// </remarks>
    /// 
    /// <response code="200">Médico criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete("ExcluirMedico/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> ExcluirMedico(string id)
    {
        var medico = await _medicoService.ConsultarId(id);
        
        if (medico == null)
        {
            return NotFound();
        }

        await _medicoService.Excluir(id);

        return Ok(new { message = "Médico excluído com sucesso." });  
    }

}