using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("Feedback")] 
public class FeedbackController : Controller
{
    private readonly IFeedbackService _feedbackService;

    public FeedbackController(IFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
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
    public async Task<IActionResult> Criar(Feedback feedback)
    {
        if (ModelState.IsValid)
        {
            await _feedbackService.Criar(feedback);
            TempData["SuccessMessage"] = "Feedback cadastrado com sucesso!";
            return RedirectToAction("Mensagem");
        }
        return View(feedback);
    }

    [HttpGet("Mensagem")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Mensagem()
    {
        return View();
    }

    /// <summary>
    ///     Cria um novo Feedback.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Cadastrar novo Feedback no banco de dados
    /// 
    /// Use este endpoint para cadastrar um Feedback.
    /// 
    /// ### Campos que devem ser utilizados para criar um novo Feedback:
    /// - **Nota** string : Nota do feedback
    /// - **Comentario** string : Comentário do feedback
    /// 
    /// ### Campos que não devem ser utilizados para criar um novo Feedback:
    /// - **Id** : Id do feedback que será gerado automaticamente
    /// 
    /// ### Exemplo de body para requisição:
    /// ```json
    ///     {
    ///         "Nota": "5",
    ///         "Comentario": "Excelente atendimento!"
    ///     }
    /// ```
    /// 
    /// ### Exemplo de resposta quando o cadastro for efetuado:
    /// 
    /// ```json
    ///     {   
    ///         "Id": "67cc95b32811515d37220000", // gerado pelo banco de dados
    ///         "Nota": "5",
    ///         "Comentario": "Excelente atendimento!"
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Requisição realizada com sucesso</response>
    /// <response code="201">Feedback criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost("CadastrarFeedback")]
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CadastrarFeedback([FromBody] Feedback feedback)
    {
        if (ModelState.IsValid)
        {
            await _feedbackService.Criar(feedback);
            return CreatedAtAction(nameof(ConsultarTodosFeedbacks), new { id = feedback.Id }, feedback); 
        }
        return BadRequest(ModelState); 
    }

    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var feedbacks = await _feedbackService.ConsultarTodos(); 
        return View(feedbacks); 
    }

    /// <summary>
    ///     Consultar a lista com todos os Feedbacks.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar todos os registros de Feedbacks do banco de dados
    /// 
    /// Use este endpoint se seu objetivo é recuperar todos os registros de Feedbacks do banco de dados.
    /// 
    /// ### Exemplo de body de resposta, sempre será uma lista com todos os Feedbacks no banco:
    /// ```json
    ///     [
    ///         { 
    ///             "Id": "67cc95b32811515d372209ce",
    ///             "Nota": "5",
    ///             "Comentario": "Excelente atendimento!"
    ///         },
    ///         {
    ///             "Id": "67cca0540924d08d2c4b7819",
    ///             "Nota": "4",
    ///             "Comentario": "Bom atendimento, mas pode melhorar."
    ///         }
    ///     ]
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Requisição realizada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarTodosFeedbacks")]
    [Produces("application/json")]
    public async Task<IActionResult> ConsultarTodosFeedbacks()
    {
        var feedbacks = await _feedbackService.ConsultarTodos();
        return Ok(feedbacks);
    }


    /// <summary>
    ///     Consultar um único registro de Feedback.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar um único Feedback no banco, sendo consultado pelo ID do banco de dados.
    /// 
    /// Use este endpoint quando precisar consultar somente um registro com todos os campos específicos.
    /// 
    /// ### Campo que deve ser utilizado para consultar um Feedback:
    /// 
    /// - **Id**: ID do banco
    /// 
    /// ### Exemplo de body para requisição:
    /// 
    /// ```json
    ///     {
    ///         "Id": "67cc95b32811515d372209ce"
    ///     }
    /// ```
    /// 
    /// ### Exemplo de body que receberemos como resposta:
    /// 
    /// ```json
    ///    {
    ///         "Id": "67cc95b32811515d372209ce",
    ///         "Nota": "5",
    ///         "Comentario": "Excelente atendimento!"
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Feedback consultado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarFeedbackId/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarFeedbackId(string id)
    {
        var feedback = await _feedbackService.ConsultarId(id);

        if (feedback == null)
        {
            return NotFound(new { message = "Feedback não encontrado." });
        }

        return Ok(feedback);
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

        var feedback = await _feedbackService.ConsultarId(userIdString);
        if (feedback == null)
        {
            return NotFound();
        }

        return View(feedback);
    }

    [HttpPost("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar(Feedback feedback)
    {
        if (!ModelState.IsValid)
        {
            return View(feedback);
        }

        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var feedbackExistente = await _feedbackService.ConsultarId(userIdString);

        if (feedbackExistente == null)
        {
            return NotFound();
        }

        feedbackExistente.Nota = feedback.Nota;
        feedbackExistente.Comentario = feedback.Comentario;

        await _feedbackService.Atualizar(feedbackExistente);

        TempData["SuccessMessage"] = "Feedback atualizado com sucesso!";
        return RedirectToAction("MensagemAtualizacao");
    }

    [HttpGet("MensagemAtualizacao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemAtualizacao()
    {
        return View();
    }

    /// <summary>
    ///     Atualiza os dados completos existentes do Feedback, com base no ID do banco de dados.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Atualizar todas as informações do Feedback no banco
    /// 
    /// Use este endpoint se o objetivo for atualizar todos os campos do Feedback no cadastro. Se for parcial, utilize outro endpoint.
    /// 
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    ///     {   
    ///         "Id": "67ce4b3d61760e36f862dd59",
    ///         "Nota": "4",
    ///         "Comentario": "Bom atendimento, mas pode melhorar."
    ///     }
    /// ``` 
    /// 
    /// ### Se preencher alguns campos e outros não, estes campos serão preenchidos com dados inválidos e valores padrão criados pelo sistema.
    /// </remarks>
    /// 
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do Feedback no banco de dados.</param>
    /// <param name="feedback">Dados do Feedback a serem atualizados.</param>
    /// <response code="200">Feedback atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="401">Feedback não autorizado</response>
    /// <response code="404">Feedback não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut("AtualizarFeedback/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> AtualizarFeedback(string id, [FromBody] Feedback feedback)
    {
        if (string.IsNullOrEmpty(id) || feedback == null || id != feedback.Id)
        {
            return BadRequest("Id do Feedback não corresponde ao fornecido.");
        }

        var feedbackExistente = await _feedbackService.ConsultarId(id);

        if (feedbackExistente == null)
        {
            return NotFound();
        }


        feedbackExistente.Nota = feedback.Nota;
        feedbackExistente.Comentario = feedback.Comentario;

        await _feedbackService.Atualizar(feedbackExistente);

        return Ok(feedbackExistente); 
    }


    [HttpGet("ConfirmarExcluir/{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmarExcluir(string id)
    {
        var feedback = await _feedbackService.ConsultarId(id);
        
        if (feedback == null)
        {
            return NotFound();
        }

        return View(feedback);
    }

    /// <summary>
    ///     Atualiza parcialmente os dados de um Feedback existente.
    /// </summary>
    /// 
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do Feedback a ser atualizado</param>
    /// 
    /// <remarks>
    /// 
    /// ## Atualização Parcial de um Feedback
    /// 
    /// Use este endpoint quando precisar atualizar apenas alguns campos específicos de um Feedback,
    /// sem a necessidade de enviar todos os dados.
    /// 
    /// ### Campos que podem ser atualizados:
    /// - **Nota**: Nota do feedback
    /// - **Comentario**: Comentário do feedback
    /// 
    /// ### Campos que não podem ser atualizados:
    /// - **Id**: ID do feedback gerado automaticamente
    /// 
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    /// {
    ///     "Id": "67cc95b32811515d372209ce",
    ///     "Comentario": "Atendimento satisfatório."
    /// }
    /// ```
    /// 
    /// Somente os campos incluídos no corpo da requisição serão atualizados.
    /// 
    /// ### Exemplo de resposta da requisição:
    /// 
    /// ```json
    /// {
    ///     "Id": "67ce4b3d61760e36f862dd59",
    ///     "Nota": "4",
    ///     "Comentario": "Atendimento satisfatório."
    /// }
    /// ```
    /// </remarks>
    /// 
    /// <param name="camposParaAtualizar"></param>
    /// 
    /// <response code="200">Feedback atualizado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Feedback não encontrado</response>
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

        var feedbackAtualizado = await _feedbackService.AtualizarParcial(id, camposParaAtualizar);

        if (feedbackAtualizado == null)
        {
            return NotFound("Feedback não encontrada.");
        }

        return Ok(feedbackAtualizado);
    }


    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Excluir(string id)
    {
        var feedback = await _feedbackService.ConsultarId(id);
        
        if (feedback != null)
        {
            await _feedbackService.Excluir(id);
            await HttpContext.SignOutAsync();
            
            TempData["SuccessMessage"] = "Feedback excluído com sucesso.";
            return RedirectToAction("MensagemExclusao", "Feedback"); 
        }

        TempData["ErrorMessage"] = "Feedback não encontrado.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("MensagemExclusao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemExclusao()
    {
        return View();
    }

    /// <summary>
    ///     Excluir um Feedback do banco de dados.
    /// </summary>
    ///
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do Feedback a ser excluído</param>
    /// 
    /// <remarks>
    /// 
    /// ## Excluir um Feedback do banco de dados e dos cadastros.
    /// 
    /// ### Exemplo da requisição para excluir um Feedback:
    /// 
    /// ```json
    ///     {
    ///         "Id": "67cc95b32811515d372209ce"
    ///     }
    /// ```
    /// 
    /// ### Exemplo da resposta para excluir um Feedback:
    /// 
    /// ```json
    ///     {
    ///         "message": "Feedback excluído com sucesso."
    ///     }
    /// ``` 
    /// 
    /// Uma vez excluído da base, não há reversão desta ação.
    /// </remarks>
    /// 
    /// <response code="200">Feedback excluído com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete("ExcluirFeedback/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> ExcluirUsuario(string id)
    {
        var feedback = await _feedbackService.ConsultarId(id);
        
        if (feedback == null)
        {
            return NotFound();
        }

        await _feedbackService.Excluir(id);

        return Ok(new { message = "Feedback excluído com sucesso." });  
    }
}