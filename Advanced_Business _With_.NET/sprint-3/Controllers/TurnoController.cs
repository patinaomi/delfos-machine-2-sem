using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("Turno")] 
public class TurnoController : Controller
{
    private readonly ITurnoService _turnoService;

    public TurnoController(ITurnoService turnoService)
    {
        _turnoService = turnoService;
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
    public async Task<IActionResult> Criar(TurnoDTO turnoDTO)
    {

        if (ModelState.IsValid)
        {

            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;
            if (string.IsNullOrEmpty(idUsuario))
                return Unauthorized("Usuário não logado.");

            var turno = new Turno
            {
                TurnoPreferencia = turnoDTO.TurnoPreferencia,
                IdUsuario = idUsuario
            };

            await _turnoService.Criar(turno);

            TempData["SuccessMessage"] = "Preferencia de turno cadastrado com sucesso!";
            //return RedirectToAction("Mensagem");
        }
        return View(turnoDTO);
    }

    [HttpGet("Mensagem")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Mensagem()
    {
        return View();
    }

    /// <summary>
    ///     Cadastra um novo Turno de preferência para o usuário.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Cadastrar um novo Turno de preferência para um usuário
    /// 
    /// Use este endPoint se o objetivo e cadastrar um novo Turno de preferência, lembrando que o usuário pode ter vários em seu cadastro.
    /// 
    /// ### Campos que devem ser utilizados para cadastrar um Turno de preferência:
    /// - **idUsuario**: ID do banco e não o idUsuario
    /// - **TurnoPreferencia**: Informe o Turno como string e no formato "08:00"
    /// 
    /// ### Campos que não devem ser utilizados para cadastrar um Turno de preferência:
    /// - **Id**: Não é necessário informar o id, pois ele será gerado automaticamente pelo
    /// 
    /// ### Exemplo de body da requisição:
    /// ```json
    ///     {   
    ///         "idUsuario": "67cc95b32811515d372209ce",
    ///         "TurnoPreferencia": "Manhã"
    ///     }
    /// ``` 
    /// 
    /// ### Exemplo de body da resposto quando o cadastro é bem sucedido
    /// ```json
    ///     {
    ///         "id": "67cf6c2a446498c2b6eb0a90",
    ///         "idUsuario": "67cc95b32811515d372209ce",
    ///         "TurnoPreferencia": "Noite"
    ///     }
    ///```
    /// </remarks>
    /// <response code="201">Horário criado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="500">Erro interno</response>
    [HttpPost("CadastrarTurno")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CadastrarTurno([FromBody] TurnoDTO turnoDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (string.IsNullOrEmpty(turnoDTO.IdUsuario))
        return BadRequest("O campo IdUsuario é obrigatório.");


        var turno = new Turno
        {
            TurnoPreferencia = turnoDTO.TurnoPreferencia,
            IdUsuario = turnoDTO.IdUsuario
        };

        await _turnoService.Criar(turno);

        return CreatedAtAction(nameof(Consultar), new { id = turno.Id }, turno);
    }


    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var turnos = await _turnoService.ConsultarTodos(); 
        return View(turnos); 
    }

    /// <summary>
    ///     Consultar todos os registros de Turnos de preferência no banco de dados.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar todos os registros de Turnos do banco de dados
    /// 
    /// Use este endPoint se seu objetivo é recuperar todos os registros de Turnos do banco de dados
    /// 
    /// ### Campos que disponíveis na requisição:
    /// - **id** : Id do banco de dados, que foi gerado automaticamente.
    /// - **idUsuario** : IdUsuario que deseja registrar o endereço
    /// - **TurnoPreferencia** string : Turno de preferência, podem ser vários.
    /// 
    /// ### Exemplo de body que virá de resposta:
    /// 
    /// ```json
    ///[
    ///    {
    ///        "id": "67cdee51b304fd2aaac177c9",
    ///        "idUsuario": "67cc95b32811515d372209ce",
    ///        "TurnoPreferencia": "Tarde",
    ///    },
    ///    {
    ///        "id": "67cdee91b304fd2aaac177ca",
    ///        "idUsuario": "67cc95b32811515d372209ce",
    ///        "TurnoPreferencia": "Manhã",
    ///    }
    ///]
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Turnos consultado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarTurno")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarTurno()
    {
        var turnos = await _turnoService.ConsultarTodos(); 
        return Ok(turnos);
    }

    /// <summary>
    ///     Consultar um único registro de preferência de Turno que o usuário cadastrou.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar um único Turno de preferência registrado, sendo consultado pelo ID do banco de dados
    /// 
    /// Use este endpoint quando precisar consultar somente um registro com todos campos específicos.
    /// 
    /// ### Campos que devem ser utilizados para consultar um Turno de preferência:
    /// 
    /// - **id**: ID do banco e não o idUsuario
    /// 
    ///  ### Exemplo de body para requisição:
    ///  
    /// ```json
    ///     "id": "67ce3bb1c9562c029b01d3fe"
    /// ```
    /// 
    /// ### Exemplo de body que receberemos como resposta:
    /// 
    /// ```json
    ///    {
    ///         "id": "67ce3bb1c9562c029b01d3fe",
    ///         "idUsuario": "67cc95b32811515d372209ce",
    ///         "TurnoPreferencia": "Tarde"
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Dia consultado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarTurnoId/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarTurnoId(string id)
    {
        var horario = await _turnoService.ConsultarId(id);

        if (horario == null)
        {
            return NotFound(new { message = "Turno não encontrado." });
        }

        return Ok(horario);
    }


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

        var turno = await _turnoService.ConsultarPorUsuarioId(userIdString);
        if (turno == null)
        {
            return NotFound();
        }

        return View(turno);
    }

    [HttpPost("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar(Turno turno)
    {
        if (!ModelState.IsValid)
        {
            return View(turno);
        }

        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var turnoExistente = await _turnoService.ConsultarPorUsuarioId(userIdString);

        if (turnoExistente == null)
        {
            return NotFound();
        }

        turnoExistente.TurnoPreferencia = turno.TurnoPreferencia;
       

        await _turnoService.Atualizar(turnoExistente);

        TempData["SuccessMessage"] = "Dado atualizado com sucesso!";
        return RedirectToAction("Consultar");
    }

    /// <summary>
    ///     Atualiza as preferências de turnos de um usuário autenticado.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Atualizar a preferência de turnos cadastrado pelo usuário
    /// 
    /// Use este endpoint se o objetivo for alterar no cadastro um ou mais turnos de preferência que o usuário deseja ser atendido.
    /// 
    /// ### Campos que devem ser utilizados para consultar um Turno de preferência:
    /// - **id**: ID do banco e não o idUsuario
    /// - **idUsuario**: IdUsuario gerado pelo banco de dados
    /// - **turnoPreferencia**: Campo em string com o Turno de preferência neste exemplo "Manhã"
    /// 
    /// ### Exemplo de requisição
    /// 
    /// ```json
    ///     {   
    ///         "id": "67ce3d45c9562c029b01d3ff",
    ///         "idUsuario": "67cc95b32811515d372209ce",
    ///         "turnoPreferencia": "Tarde"
    ///     }
    /// 
    /// ``` 
    /// 
    /// ### Exemplo do modelo de resposta quando ocorre sucesso na alteração
    /// 
    /// ```json
    ///     {
    ///         "id": "67ce3d45c9562c029b01d3ff",
    ///         "idUsuario": "67cc95b32811515d372209ce",
    ///         "turnoPreferencia": "Noite"
    ///         "mensagem": "Horário atualizado com sucesso!"
    ///    }
    /// ```
    /// 
    /// </remarks>
    /// 
    /// <returns>Retorna 204 No Content se a atualização for bem-sucedida.</returns>
    /// <response code="204">Dados atualizados com sucesso.</response>
    /// <response code="400">Erro na requisição (dados inválidos).</response>
    /// <response code="401">Usuário não autenticado.</response>
    /// <response code="404">Nenhum registro encontrado para o usuário.</response>
    [HttpPut("AtualizarTurno/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarTurno(string id, [FromBody] TurnoDTO turnoDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (turnoDTO.Id == null)
            return BadRequest(new { message = "ID do turno não pode ser nulo." });

        var turnoExistente = await _turnoService.ConsultarId(turnoDTO.Id);
        if (turnoExistente == null)
            return NotFound(new { message = "Horário não encontrado." });

        turnoExistente.TurnoPreferencia = turnoDTO.TurnoPreferencia;

        await _turnoService.Atualizar(turnoExistente);

        return Ok(new
        {
            id = turnoExistente.Id,
            idUsuario = turnoExistente.IdUsuario,
            turnosPreferencia = turnoExistente.TurnoPreferencia,
            mensagem = "Turno atualizado com sucesso!"
        });
    }


    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Excluir(string idDia)
    {
        Console.WriteLine($"Excluindo Turno com Id: {idDia}");

        try
        {
            await _turnoService.Excluir(idDia);
            TempData["SuccessMessage"] = "Exclusão realizada com sucesso!";
        }
        catch (ArgumentException ex)
        {
            TempData["ErrorMessage"] = $"Erro: {ex.Message}";
        }

        return RedirectToAction("Consultar");
    }

    /// <summary>
    ///     Exclui um turno de preferência do usuário.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Excluir um turno de preferência do usuário do cadastro feito no banco.
    /// 
    /// Use este endoPoint se seu objetivo é excluir um cadastro contendo o dia de preferência cadastrado errado. 
    /// 
    /// ### Exemplo da requisição para excluir:
    /// 
    /// ```json
    ///     {
    ///         "id": "67cf3f8f8d3a256253f2dab5",
    ///     }
    /// ```
    /// 
    /// ### Exemplo da resposta para excluir uma clínica:
    /// 
    /// ```json
    ///     {
    ///         "message": "Turno de preferência excluído com sucesso."
    ///     }
    /// ``` 
    /// 
    /// Uma vez excluida da base, não tem reversão desta ação.
    /// 
    /// </remarks>
    /// 
    /// <param name="id" type="string" example="67cdee51b304fd2aaac177c9">ID do dia a ser excluído.</param>
    /// <response code="200">Dia excluído com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    /// <response code="404">Dia não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete("ExcluirTurno/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExcluirTurno(string id)
    {
        if (string.IsNullOrEmpty(id))
        return BadRequest("ID do Turno não pode ser vazio.");

        var endereco = await _turnoService.ConsultarId(id);
        if (endereco == null)
            return NotFound("Turno não encontrado.");

        await _turnoService.Excluir(id);
        return Ok(new { message = "Turno excluído com sucesso." });
    }



}