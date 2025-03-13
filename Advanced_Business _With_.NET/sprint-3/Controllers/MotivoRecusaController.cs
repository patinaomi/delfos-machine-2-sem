using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("MotivoRecusa")] 
public class MotivoRecusaController : Controller
{
    private readonly IMotivoRecusaService _motivoRecusaService;

    public MotivoRecusaController(IMotivoRecusaService motivoService)
    {
        _motivoRecusaService = motivoService;
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
    public async Task<IActionResult> Criar(MotivoRecusa motivoRecusa)
    {
        if (ModelState.IsValid)
        {
            await _motivoRecusaService.Criar(motivoRecusa);
            TempData["SuccessMessage"] = "Motivo de recusa cadastrado com sucesso!";
            return RedirectToAction("Mensagem");
        }
        return View(motivoRecusa);
    }

    [HttpGet("Mensagem")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Mensagem()
    {
        return View();
    }

    /// <summary>
    ///     Cria um novo Motivo de recusa.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Cadastrar novo Motivo de recusa no banco de dados
    /// 
    /// Use este endpoint para cadastrar um Motivo de recusa. Apenas motivos de recusa cadastrados podem ser utilizados na plataforma.
    /// 
    /// ### Campos que devem ser utilizados para criar um novo motivo:
    /// - **motivo** string : Descrição curta do Motivo de recusa (ex: "Cliente não compareceu").
    /// - **descricao** string : Descrição detalhada do Motivo de recusa (ex: "O cliente não compareceu na consulta agendada sem aviso prévio").
    /// 
    /// ### Campos que não devem ser utilizados para criar um novo motivo:
    /// - **id** : Id do motivo que será gerado automaticamente.
    /// 
    /// ### Exemplo de body para requisição:
    /// ```json
    ///     {
    ///         "motivo": "Cliente não compareceu",
    ///         "descricao": "O cliente não compareceu na consulta agendada sem aviso prévio."
    ///     }
    /// ```
    /// 
    /// ### Exemplo de resposta quando o cadastro for efetuado:
    /// 
    /// ```json
    ///     {   
    ///         "id": "67cc95b32811515d37220000", -- gerado pelo banco de dados
    ///         "motivo": "Cliente não compareceu",
    ///         "descricao": "O cliente não compareceu na consulta agendada sem aviso prévio."
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Requisição realizada com sucesso</response>
    /// <response code="201">Motivo de recusa criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost("CadastrarMotivo")]
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CadastrarMotivo([FromBody] MotivoRecusa motivo)
    {
        if (ModelState.IsValid)
        {
            await _motivoRecusaService.Criar(motivo);
            return CreatedAtAction(nameof(ConsultarTodosMotivos), new { id = motivo.Id }, motivo); 
        }
        return BadRequest(ModelState); 
    }

    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var motivos = await _motivoRecusaService.ConsultarTodos(); 
        return View(motivos); 
    }

    /// <summary>
    ///     Consultar a lista com todos os Motivos de recusa.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar todos os registros de Motivos de recusa do banco de dados
    /// 
    /// Use este endpoint se seu objetivo é recuperar todos os registros de Motivos de recusa do banco de dados.
    /// 
    /// ### Campos disponíveis para consultar os Motivos de recusa:
    /// - **id** : Id do motivo de recusa, gerado automaticamente.
    /// - **motivo** string : Descrição curta do Motivo de recusa (ex: "Cliente não compareceu").
    /// - **descricao** string : Descrição detalhada do Motivo de recusa (ex: "O cliente não compareceu na consulta agendada sem aviso prévio").
    /// 
    /// ### Exemplo de corpo de resposta (body), sempre será uma lista com todos os Motivos de recusa no banco:
    /// ```json
    ///     [
    ///         { 
    ///             "id": "67cc95b32811515d372209ce",
    ///             "motivo": "Cliente não compareceu",
    ///             "descricao": "O cliente não compareceu na consulta agendada sem aviso prévio."
    ///         },
    ///         {
    ///             "id": "67cca0540924d08d2c4b7819",
    ///             "motivo": "Pedido Cancelado",
    ///             "descricao": "O pedido foi cancelado por motivo pessoal do cliente."
    ///         }
    ///     ]
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Motivos de recusa recuperados com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarTodosMotivos")]
    [Produces("application/json")]
    public async Task<IActionResult> ConsultarTodosMotivos()
    {
        var motivos = await _motivoRecusaService.ConsultarTodos();
        return Ok(motivos);
    }

    /// <summary>
    ///     Consultar um único registro de Motivo de recusa
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar um único Motivo de recusa no banco, sendo consultado pelo ID do banco de dados
    /// 
    /// Use este endpoint quando precisar consultar somente um registro com todos os campos específicos.
    /// 
    /// ### Campos que devem ser utilizados para consultar um Motivo de recusa:
    /// 
    /// - **id**: ID do motivo de recusa no banco.
    /// 
    ///  ### Exemplo de corpo para requisição:
    ///  
    /// ```json
    ///     "id": "67d0b66edb7709d2ac4f7f74"
    /// ```
    /// 
    /// ### Exemplo de corpo que receberemos como resposta:
    /// 
    /// ```json
    ///    {
    ///         "id": "67d0b66edb7709d2ac4f7f74",
    ///         "motivo": "Sem sistema",
    ///         "descricao": "O sistema está inoperante no momento."
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Motivo de recusa consultado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarMotivoId/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarMotivoId(string id)
    {
        var motivo = await _motivoRecusaService.ConsultarId(id);

        if (motivo == null)
        {
            return NotFound(new { message = "Motivo de recusa não encontrado." });
        }

        return Ok(motivo);
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

        var motivo = await _motivoRecusaService.ConsultarId(userIdString);
        if (motivo == null)
        {
            return NotFound();
        }

        return View(motivo);
    }

    [HttpPost("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar(MotivoRecusa motivoRecusa)
    {
        if (!ModelState.IsValid)
        {
            return View(motivoRecusa);
        }

        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var motivoExistente = await _motivoRecusaService.ConsultarId(userIdString);

        if (motivoExistente == null)
        {
            return NotFound();
        }

        motivoExistente.Motivo = motivoRecusa.Motivo;
        motivoExistente.Descricao = motivoRecusa.Descricao;

        await _motivoRecusaService.Atualizar(motivoExistente);

        TempData["SuccessMessage"] = "Motivo de recusa atualizado com sucesso!";
        return RedirectToAction("MensagemAtualizacao");
    }

    [HttpGet("MensagemAtualizacao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemAtualizacao()
    {
        return View();
    }

    /// <summary>
    ///     Atualiza os dados completos do Motivo de recusa, com base no ID do banco de dados
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Atualizar todas as informações do Motivo de recusa no banco
    /// 
    /// Use este endpoint se o objetivo for atualizar todos os campos do Motivo de recusa no cadastro. Se a atualização for parcial, utilize outro endpoint.
    /// 
    /// ### Exemplo de requisição
    /// 
    /// ```json
    ///     {   
    ///         "id": "67d0b66edb7709d2ac4f7f74",
    ///         "motivo": "Cliente não compareceu",
    ///         "descricao": "O cliente não compareceu na consulta agendada sem aviso prévio."
    ///     }
    /// ``` 
    /// 
    /// ### Se preencher alguns campos e outros não, estes campos serão preenchidos com dados inválidos e valores default criados pelo sistema.
    /// </remarks>
    /// 
    /// <param name="id" type="string" example="67d0b66edb7709d2ac4f7f74">ID do Motivo de recusa no banco de dados.</param>
    /// <param name="motivo">Dados do Motivo de recusa a serem atualizados.</param>
    /// <response code="200">Motivo de recusa atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="401">Motivo de recusa não autorizado</response>
    /// <response code="404">Motivo de recusa não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut("AtualizarMotivo/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> AtualizarMotivo(string id, [FromBody] MotivoRecusa motivo)
    {
        if (string.IsNullOrEmpty(id) || motivo == null || id != motivo.Id)
        {
            return BadRequest("Id do Clinica não corresponde ao fornecido.");
        }

        var motivoExistente = await _motivoRecusaService.ConsultarId(id);

        if (motivoExistente == null)
        {
            return NotFound();
        }


        motivoExistente.Motivo = motivo.Motivo;
        motivoExistente.Descricao = motivo.Descricao;
      

        await _motivoRecusaService.Atualizar(motivoExistente);

        return Ok(motivoExistente); 
    }


    [HttpGet("ConfirmarExcluir/{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmarExcluir(string id)
    {
        var motivo = await _motivoRecusaService.ConsultarId(id);
        
        if (motivo == null)
        {
            return NotFound();
        }

        return View(motivo);
    }

    /// <summary>
    ///     Atualiza parcialmente os dados de uma Motivo de recusa existente
    /// </summary>
    /// 
    /// <param name="id" type="string" example="67d0b66edb7709d2ac4f7f74">ID do Motivo de recusa a ser atualizada</param>
    /// 
    /// <remarks>
    /// 
    /// ## Atualização Parcial de um Motivo de recusa
    /// 
    /// Use este endpoint quando precisar atualizar apenas alguns campos específicos de um Motivo de recusa,
    /// sem a necessidade de enviar todos os dados.
    /// 
    /// ### Campos disponíveis para atualizar nos Motivos de recusa:
    /// - **motivo** string : Descrição curta do Motivo de recusa (ex: "Cliente não compareceu").
    /// - **descricao** string : Descrição detalhada do Motivo de recusa (ex: "O cliente não compareceu na consulta agendada sem aviso prévio").
    /// 
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    ///     {   
    ///         "id": "67d0b66edb7709d2ac4f7f74",
    ///         "motivo": "Sem médicos disponíveis",
    ///     }
    /// ``` 
    /// 
    /// Somente os campos incluídos no corpo da requisição serão atualizados.
    /// 
    /// ### Exemplo de resposta da requisição:
    /// 
    /// ```json
    ///     {   
    ///         "id": "67d0b66edb7709d2ac4f7f74",
    ///         "motivo": "Sem médicos",
    ///         "descricao": "Sem especilista para atendimento na clínica"
    ///     }
    /// ``` 
    /// </remarks>
    /// 
    /// <param name="camposParaAtualizar"></param>
    /// 
    /// <response code="200">Motivo de recusa atualizada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Motivo de recusa não encontrada</response>
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
            return BadRequest("Id da Motivo e/ou campos para atualização são necessários.");
        }

        var motivoAtualizado = await _motivoRecusaService.AtualizarParcial(id, camposParaAtualizar);

        if (motivoAtualizado == null)
        {
            return NotFound("Motivo de recusa não encontrada.");
        }

        return Ok(motivoAtualizado);
    }

    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Excluir(string id)
    {
        var motivo = await _motivoRecusaService.ConsultarId(id);
        
        if (motivo != null)
        {

            await _motivoRecusaService.Excluir(id);

            //await HttpContext.SignOutAsync();
            
            TempData["SuccessMessage"] = "Motivo de recusa excluído com sucesso.";
            //return RedirectToAction("MensagemExclusao", "Motivo"); 
        }

        TempData["ErrorMessage"] = "Motivo de recusa não encontrado.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("MensagemExclusao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemExclusao()
    {
        return View();
    }

    /// <summary>
    ///     Excluir os Motivo de recusa do banco de dados.
    /// </summary>
    ///
    /// <param name="id" type="string" example="67d0b66edb7709d2ac4f7f74">ID do Motivo de recusa a ser excluído</param>
    /// 
    /// <remarks>
    /// 
    /// ## Excluir um Motivo de recusa do banco de dados e dos cadastros.
    /// 
    /// ### Exemplo da requisição para excluir um Motivo de recusa:
    /// 
    /// ```json
    ///     {
    ///         "id": "67d0b66edb7709d2ac4f7f74",
    ///     }
    /// ```
    /// 
    /// ### Exemplo da resposta para excluir um Motivo de recusa:
    /// 
    /// ```json
    ///     {
    ///         "message": "Motivo de recusa excluído com sucesso."
    ///     }
    /// ``` 
    /// 
    /// Uma vez excluida da base, não tem reversão desta ação.
    /// </remarks>
    /// 
    /// <response code="200">Motivo de recusa criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete("ExcluirMotivo/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> ExcluirMotivo(string id)
    {
        var motivo = await _motivoRecusaService.ConsultarId(id);
        
        if (motivo == null)
        {
            return NotFound();
        }

        await _motivoRecusaService.Excluir(id);

        return Ok(new { message = "Motivo de recusa excluído com sucesso." });  
    }

}