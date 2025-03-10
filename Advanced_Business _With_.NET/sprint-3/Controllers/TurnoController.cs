using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
    /// Cadastra um novo Turno de preferência para o usuário.
    /// </summary>
    /// <remarks>
    /// Exemplo de body:
    /// 
    /// {   
    ///     "idUsuario": "67cc95b32811515d372209ce",
    ///     "Turno": "Manhã"
    /// }
    /// </remarks>
    /// <response code="201">Turno criado com sucesso</response>
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

        var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;
        if (string.IsNullOrEmpty(idUsuario))
            return Unauthorized("Usuário não logado.");


        var turno = new Turno
        {
            TurnoPreferencia = turnoDTO.TurnoPreferencia,
            IdUsuario = idUsuario
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
    ///     Consultar o turno de preferência do usuário.
    /// </summary>
    /// 
    /// <remarks>
    /// Exemplo de body que virá de resposta:
    /// 
    /// {
    ///      "id": "67ce3a37f153b188528f8456",
    ///      "idUsuario": "67cc95b32811515d372209ce",
    ///      "turno": "Tarde"
    /// }
    /// </remarks>
    /// 
    /// <response code="200">Turno consultado com sucesso</response>
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

        //var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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
    /// Atualiza um Turno existente do usuário, com base no ID do usuário.
    /// </summary>
    /// <param name="id" type="string" example="67ce3bb1c9562c029b01d3fe">ID do usuário a ser atualizado o horário.</param>
    /// <param name="idUsuario" type="string" example="67cc95b32811515d372209ce">ID do usuário a ser atualizado o horário.</param>
    /// <param name="horariosPreferencia" type="string" example="Tarde">Turno que deseja atualizar</param>
    /// <response code="200">Turno atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="401">Usuário não autorizado</response>
    /// <response code="404">Turno não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
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

        var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;
        if (string.IsNullOrEmpty(idUsuario))
            return Unauthorized("Usuário não logado.");

        var turnoExistente = await _turnoService.ConsultarPorUsuarioId(id);
        if (turnoExistente == null || turnoExistente.IdUsuario != idUsuario)
            return NotFound("Horário não encontrado ou não pertence ao usuário.");

        turnoExistente.TurnoPreferencia = turnoDTO.TurnoPreferencia;

        await _turnoService.Atualizar(turnoExistente);

        return Ok(new { message = "Turno atualizado com sucesso!" });
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
    /// Exclui um Turno de preferência do usuário.
    /// </summary>
    /// <param name="id" type="string" example="67cdee51b304fd2aaac177c9">ID do Turno a ser excluído.</param>
    /// <response code="200">Turno excluído com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    /// <response code="404">Turno não encontrado</response>
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