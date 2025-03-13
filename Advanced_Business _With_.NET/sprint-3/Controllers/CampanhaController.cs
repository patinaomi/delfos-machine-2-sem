using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("Campanha")] 
public class CampanhaController : Controller
{
    private readonly ICampanhaService _campanhaService;

    public CampanhaController(ICampanhaService campanhaService)
    {
        _campanhaService = campanhaService;
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
    public async Task<IActionResult> Criar(Campanha campanha)
    {
        if (ModelState.IsValid)
        {
            await _campanhaService.Criar(campanha);
            TempData["SuccessMessage"] = "Ativiadade cadastrada com sucesso!";
            return RedirectToAction("Mensagem");
        }
        return View(campanha);
    }

    [HttpGet("Mensagem")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Mensagem()
    {
        return View();
    }

    /// <summary>
    ///     Cria uma nova atividade de campanha.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Cadastrar nova atividade de campanha no banco de dados
    /// 
    /// Use este endpoint para cadastrar uma atividade de campanha.
    /// 
    /// ### Campos que devem ser utilizados para criar uma nova atividade de campanha:
    /// - **atividade** string : Nome da atividade
    /// - **descricao** string : Descrição detalhada da atividade
    /// - **pontuacao** string : Pontuação da atividade (5, 10, 15 ou 20)
    /// - **perfil** string : Perfil da atividade (Clinica ou Cliente)
    /// - **idUsuario** string : Identificador do usuário
    /// - **status** string : Status da atividade (padrão = "Pendente")
    /// - **dataCriacao** string : Data de criação da atividade
    /// - **dataConclusao** string : Data de conclusão da atividade (opcional)
    /// 
    /// ### Campos que não devem ser utilizados para criar uma nova atividade de campanha:
    /// - **id** : Id da atividade que será gerado automaticamente
    /// 
    /// ### Exemplo de body para requisição:
    /// ```json
    ///     {
    ///         "atividade": "Consulta de rotina",
    ///         "descricao": "Realizar uma consulta de rotina na clínica.",
    ///         "pontuacao": "10",
    ///         "perfil": "Cliente",
    ///         "idUsuario": "1234567890",
    ///         "status": "Pendente",
    ///         "dataCriacao": "2025-03-12",
    ///         "dataConclusao": null
    ///     }
    /// ```
    /// 
    /// ### Exemplo de resposta quando o cadastro for efetuado:
    /// 
    /// ```json
    ///     {   
    ///         "id": "67cc95b32811515d37220000", // gerado pelo banco de dados
    ///         "atividade": "Consulta de rotina",
    ///         "descricao": "Realizar uma consulta de rotina na clínica.",
    ///         "pontuacao": "10",
    ///         "perfil": "Cliente",
    ///         "idUsuario": "1234567890",
    ///         "status": "Pendente",
    ///         "dataCriacao": "2025-03-12",
    ///         "dataConclusao": null
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Requisição realizada com sucesso</response>
    /// <response code="201">Atividade de campanha criada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost("CadastrarCampanha")]
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CadastrarCampanha([FromBody] Campanha campanha)
    {
        if (ModelState.IsValid)
        {
            await _campanhaService.Criar(campanha);
            return CreatedAtAction(nameof(ConsultarTodasAtividades), new { id = campanha.Id }, campanha); 
        }
        return BadRequest(ModelState); 
    }

    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var campanhas = await _campanhaService.ConsultarTodos(); 
        return View(campanhas); 
    }

    /// <summary>
    ///     Consultar a lista com todas as atividades de campanha.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar todos os registros de atividades de campanha do banco de dados
    /// 
    /// Use este endpoint se seu objetivo é recuperar todos os registros de atividades de campanha do banco de dados.
    /// 
    /// ### Campos disponíveis para consultar as atividades de campanha:
    /// - **id** : Id da atividade gerado automaticamente
    /// - **atividade** string : Nome da atividade
    /// - **descricao** string : Descrição detalhada da atividade
    /// - **pontuacao** string : Pontuação da atividade (5, 10, 15 ou 20)
    /// - **perfil** string : Perfil da atividade (Clinica ou Cliente)
    /// - **idUsuario** string : Identificador do usuário
    /// - **status** string : Status da atividade (padrão = "Pendente")
    /// - **dataCriacao** string : Data de criação da atividade
    /// - **dataConclusao** string : Data de conclusão da atividade (opcional)
    /// 
    /// ### Exemplo de body de resposta, sempre será uma lista com todas as atividades de campanha no banco:
    /// ```json
    ///     [
    ///         { 
    ///             "id": "67cc95b32811515d372209ce",
    ///             "atividade": "Consulta de rotina",
    ///             "descricao": "Realizar uma consulta de rotina na clínica.",
    ///             "pontuacao": "10",
    ///             "perfil": "Cliente",
    ///             "idUsuario": "1234567890",
    ///             "status": "Pendente",
    ///             "dataCriacao": "2025-03-12",
    ///             "dataConclusao": null
    ///         },
    ///         {
    ///             "id": "67cca0540924d08d2c4b7819",
    ///             "atividade": "Exame de sangue",
    ///             "descricao": "Realizar um exame de sangue completo.",
    ///             "pontuacao": "15",
    ///             "perfil": "Cliente",
    ///             "idUsuario": "0987654321",
    ///             "status": "Pendente",
    ///             "dataCriacao": "2025-03-12",
    ///             "dataConclusao": null
    ///         }
    ///     ]
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Requisição realizada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarTodasAtividades")]
    [Produces("application/json")]
    public async Task<IActionResult> ConsultarTodasAtividades()
    {
        var campanhas = await _campanhaService.ConsultarTodos();
        return Ok(campanhas);
    }

    /// <summary>
    ///     Consultar um único registro de atividade de campanha.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar uma única atividade de campanha no banco, sendo consultada pelo ID do banco de dados.
    /// 
    /// Use este endpoint quando precisar consultar somente um registro com todos os campos específicos.
    /// 
    /// ### Campo que deve ser utilizado para consultar uma atividade de campanha:
    /// 
    /// - **id**: ID do banco
    /// 
    /// ### Exemplo de body para requisição:
    /// 
    /// ```json
    ///     {
    ///         "id": "67cc95b32811515d372209ce"
    ///     }
    /// ```
    /// 
    /// ### Exemplo de body que receberemos como resposta:
    /// 
    /// ```json
    ///    {
    ///         "id": "67cc95b32811515d372209ce",
    ///         "atividade": "Consulta de rotina",
    ///         "descricao": "Realizar uma consulta de rotina na clínica.",
    ///         "pontuacao": "10",
    ///         "perfil": "Cliente",
    ///         "idUsuario": "1234567890",
    ///         "status": "Pendente",
    ///         "dataCriacao": "2025-03-12",
    ///         "dataConclusao": null
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Atividade de campanha consultada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarCampanhaId/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarCampanhaId(string id)
    {
        var campanha = await _campanhaService.ConsultarId(id);

        if (campanha == null)
        {
            return NotFound(new { message = "Atividade não encontrado." });
        }

        return Ok(campanha);
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

        var campanha = await _campanhaService.ConsultarId(userIdString);
        if (campanha == null)
        {
            return NotFound();
        }

        return View(campanha);
    }

    [HttpPost("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar(Campanha campanha)
    {
        if (!ModelState.IsValid)
        {
            return View(campanha);
        }

        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var campanhaExistente = await _campanhaService.ConsultarId(userIdString);

        if (campanhaExistente == null)
        {
            return NotFound();
        }

        campanhaExistente.Atividade = campanha.Atividade;
        campanhaExistente.Descricao = campanha.Descricao;
        campanhaExistente.Pontuacao = campanha.Pontuacao;
        campanhaExistente.Perfil = campanha.Perfil;
        campanhaExistente.IdUsuario = campanha.IdUsuario;
        campanhaExistente.Status = campanha.Status;
        campanhaExistente.DataCriacao = campanha.DataCriacao;
        campanhaExistente.DataConclusao = campanha.DataConclusao;

        await _campanhaService.Atualizar(campanhaExistente);

        TempData["SuccessMessage"] = "Atividade atualizada com sucesso!";
        return RedirectToAction("MensagemAtualizacao");
    }

    [HttpGet("MensagemAtualizacao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemAtualizacao()
    {
        return View();
    }

    /// <summary>
    ///     Atualiza os dados completos existentes da atividade de campanha, com base no ID do banco de dados.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Atualizar todas as informações da atividade de campanha no banco
    /// 
    /// Use este endpoint se o objetivo for atualizar todos os campos da atividade de campanha no cadastro. Se for parcial, utilize outro endpoint.
    /// 
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    ///     {   
    ///         "id": "67ce4b3d61760e36f862dd59",
    ///         "atividade": "Consulta de rotina",
    ///         "descricao": "Realizar uma consulta de rotina na clínica.",
    ///         "pontuacao": "10",
    ///         "perfil": "Cliente",
    ///         "idUsuario": "1234567890",
    ///         "status": "Pendente",
    ///         "dataCriacao": "2025-03-12",
    ///         "dataConclusao": null
    ///     }
    /// ``` 
    /// 
    /// ### Se preencher alguns campos e outros não, estes campos serão preenchidos com dados inválidos e valores padrão criados pelo sistema.
    /// </remarks>
    /// 
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID da atividade de campanha no banco de dados.</param>
    /// <param name="campanha">Dados da atividade de campanha a serem atualizados.</param>
    /// <response code="200">Atividade de campanha atualizada com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="401">Usuário não autorizado</response>
    /// <response code="404">Atividade de campanha não encontrada</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut("AtualizarCampanha/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> AtualizarCampanha(string id, [FromBody] Campanha campanha)
    {
        if (string.IsNullOrEmpty(id) || campanha == null || id != campanha.Id)
        {
            return BadRequest("Id do Clinica não corresponde ao fornecido.");
        }

        var campanhaExistente = await _campanhaService.ConsultarId(id);

        if (campanhaExistente == null)
        {
            return NotFound();
        }

        campanhaExistente.Atividade = campanha.Atividade;
        campanhaExistente.Descricao = campanha.Descricao;
        campanhaExistente.Pontuacao = campanha.Pontuacao;
        campanhaExistente.Perfil = campanha.Perfil;
        campanhaExistente.IdUsuario = campanha.IdUsuario;
        campanhaExistente.Status = campanha.Status;
        campanhaExistente.DataCriacao = campanha.DataCriacao;
        campanhaExistente.DataConclusao = campanha.DataConclusao;

        await _campanhaService.Atualizar(campanhaExistente);

        return Ok(campanhaExistente); 
    }

    [HttpGet("ConfirmarExcluir/{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmarExcluir(string id)
    {
        var campanha = await _campanhaService.ConsultarId(id);
        
        if (campanha == null)
        {
            return NotFound();
        }

        return View(campanha);
    }

    /// <summary>
    ///     Atualiza parcialmente os dados de uma campanha existente
    /// </summary>
    /// 
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID da campanha a ser atualizada</param>
    /// 
    /// <remarks>
    /// 
    /// ## Atualização Parcial de uma Campanha
    /// 
    /// Use este endpoint quando precisar atualizar apenas alguns campos específicos de uma campanha,
    /// sem a necessidade de enviar todos os dados.
    /// 
    /// ### Campos que podem ser atualizados:
    /// - **Atividade**: Atividade da campanha
    /// - **Descricao**: Descrição da campanha
    /// - **Pontuacao**: Pontuação da campanha (5, 10, 15 ou 20)
    /// - **Perfil**: Perfil da campanha (Clinica ou Cliente)
    /// - **IdUsuario**: ID do usuário associado à campanha
    /// - **Status**: Status da campanha (Pendente ou Concluída)
    /// - **DataConclusao**: Data de conclusão da campanha
    /// 
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    /// {
    ///     "id": "67cc95b32811515d372209ce",
    ///     "status": "Concluída"
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
    ///     "atividade": "Atividade Exemplo",
    ///     "descricao": "Descrição Exemplo",
    ///     "pontuacao": "10",
    ///     "perfil": "Cliente",
    ///     "idUsuario": "123456789",
    ///     "status": "Concluída",
    ///     "dataCriacao": "2025-03-12T12:00:00Z",
    ///     "dataConclusao": "2025-03-13T12:00:00Z"
    /// }
    /// ```
    /// </remarks>
    /// 
    /// <param name="camposParaAtualizar"></param>
    /// 
    /// <response code="200">Campanha atualizada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Campanha não encontrada</response>
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

        var campanhaAtualizado = await _campanhaService.AtualizarParcial(id, camposParaAtualizar);

        if (campanhaAtualizado == null)
        {
            return NotFound("Atividade não encontrada.");
        }

        return Ok(campanhaAtualizado);
    }


    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Excluir(string id)
    {
        var campanha = await _campanhaService.ConsultarId(id);
        
        if (campanha != null)
        {
            await _campanhaService.Excluir(id);

            //await HttpContext.SignOutAsync();
            
            TempData["SuccessMessage"] = "Atividade excluída com sucesso.";
            //return RedirectToAction("MensagemExclusao", "Campanha"); 
        }

        TempData["ErrorMessage"] = "Atividade não encontrado.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("MensagemExclusao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemExclusao()
    {
        return View();
    }

    /// <summary>
    ///     Excluir as atividades da campanha do banco de dados.
    /// </summary>
    ///
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID a ser excluído</param>
    /// 
    /// <remarks>
    /// 
    /// ## Excluir uma Atividade do banco de dados
    /// 
    /// ### Exemplo da requisição para excluir uma atividade:
    /// 
    /// ```json
    ///     {
    ///         "id": "67cc95b32811515d372209ce",
    ///     }
    /// ```
    /// 
    /// ### Exemplo da resposta para excluir uma atividade:
    /// 
    /// ```json
    ///     {
    ///         "message": "Atividade excluída com sucesso."
    ///     }
    /// ``` 
    /// 
    /// Uma vez excluida da base, não tem reversão desta ação.
    /// </remarks>
    /// 
    /// <response code="200">Ação registrada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete("ExcluirCampanha/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> ExcluirCampanha(string id)
    {
        var campanha = await _campanhaService.ConsultarId(id);
        
        if (campanha == null)
        {
            return NotFound();
        }

        await _campanhaService.Excluir(id);

        return Ok(new { message = "Atividade excluída com sucesso." });  
    }




}