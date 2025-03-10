using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("Horarios")] 
public class HorariosController : Controller
{
    private readonly IHorariosService _horariosService;

    public HorariosController(IHorariosService horariosService)
    {
        _horariosService = horariosService;
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
    public async Task<IActionResult> Criar(HorariosDTO horariosDTO)
    {

        if (ModelState.IsValid)
        {

            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;
            if (string.IsNullOrEmpty(idUsuario))
                return Unauthorized("Usuário não logado.");

            var horario = new Horarios
            {
                HorariosPreferencia = horariosDTO.HorariosPreferencia,
                IdUsuario = idUsuario
            };

            await _horariosService.Criar(horario);

            TempData["SuccessMessage"] = "Preferencia de horário cadastrado com sucesso!";
           
        }
        return View(horariosDTO);
    }

    [HttpGet("Mensagem")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Mensagem()
    {
        return View();
    }
    
    /// <summary>
    /// Cadastra um novo horário de preferência para o usuário.
    /// </summary>
    /// <remarks>
    /// Exemplo de body:
    /// 
    /// {   
    ///     "idUsuario": "67cc95b32811515d372209ce",
    ///     "horariosPreferencia": "07:00"
    /// }
    /// </remarks>
    /// <response code="201">Horário criado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="500">Erro interno</response>
    [HttpPost("CadastrarHorario")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CadastrarHorario([FromBody] HorariosDTO horariosDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;
        if (string.IsNullOrEmpty(idUsuario))
            return Unauthorized("Usuário não logado.");


        var horario = new Horarios
        {
            HorariosPreferencia = horariosDTO.HorariosPreferencia,
            IdUsuario = idUsuario
        };

        await _horariosService.Criar(horario);

        return CreatedAtAction(nameof(Consultar), new { id = horario.Id }, horario);
    }


    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var horarios = await _horariosService.ConsultarTodos(); 
        return View(horarios); 
    }

    /// <summary>
    ///     Consultar o horário de preferência do usuário.
    /// </summary>
    /// 
    /// <remarks>
    /// Exemplo de body que virá de resposta:
    /// 
    /// {
    ///      "id": "67ce3a37f153b188528f8456",
    ///      "idUsuario": "67cc95b32811515d372209ce",
    ///      "horariosPreferencia": "07:00"
    /// }
    /// </remarks>
    /// 
    /// <response code="200">Horário consultado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarHorario")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarHorario()
    {
        var horarios = await _horariosService.ConsultarTodos(); 
        return Ok(horarios);
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

        var horario = await _horariosService.ConsultarPorUsuarioId(userIdString);
        if (horario == null)
        {
            return NotFound();
        }

        return View(horario);
    }

    [HttpPost("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar(Horarios horario)
    {
        if (!ModelState.IsValid)
        {
            return View(horario);
        }

        //var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var horarioExistente = await _horariosService.ConsultarPorUsuarioId(userIdString);

        if (horarioExistente == null)
        {
            return NotFound();
        }

        horarioExistente.HorariosPreferencia = horario.HorariosPreferencia;
       

        await _horariosService.Atualizar(horarioExistente);

        TempData["SuccessMessage"] = "Dado atualizado com sucesso!";
        return RedirectToAction("Consultar");
    }

    /// <summary>
    /// Atualiza um horário existente do usuário, com base no ID do usuário.
    /// </summary>
    /// <param name="id" type="string" example="67ce3bb1c9562c029b01d3fe">ID do usuário a ser atualizado o horário.</param>
    /// /// <param name="idUsuario" type="string" example="67cc95b32811515d372209ce">ID do usuário a ser atualizado o horário.</param>
    /// <param name="horariosPreferencia" type="string" example="08:00">Horário que deseja atualizar "08:00"</param>
    /// <response code="200">Horário atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="401">Usuário não autorizado</response>
    /// <response code="404">Horário não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut("AtualizarHorario/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarHorario(string id, [FromBody] HorariosDTO horariosDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;
        if (string.IsNullOrEmpty(idUsuario))
            return Unauthorized("Usuário não logado.");

        var horarioExistente = await _horariosService.ConsultarPorUsuarioId(id);
        if (horarioExistente == null || horarioExistente.IdUsuario != idUsuario)
            return NotFound("Horário não encontrado ou não pertence ao usuário.");

        horarioExistente.HorariosPreferencia = horariosDTO.HorariosPreferencia;

        await _horariosService.Atualizar(horarioExistente);

        return Ok(new { message = "Horário atualizado com sucesso!" });
    }



    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Excluir(string idDia)
    {
        Console.WriteLine($"Excluindo Horário com Id: {idDia}");

        try
        {
            await _horariosService.Excluir(idDia);
            TempData["SuccessMessage"] = "Exclusão realizada com sucesso!";
        }
        catch (ArgumentException ex)
        {
            TempData["ErrorMessage"] = $"Erro: {ex.Message}";
        }

        return RedirectToAction("Consultar");
    }


    /// <summary>
    /// Exclui um Horário de preferência do usuário.
    /// </summary>
    /// <param name="id" type="string" example="67cdee51b304fd2aaac177c9">ID do Horário a ser excluído.</param>
    /// <response code="200">Horário excluído com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    /// <response code="404">Horário não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete("ExcluirHorario/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExcluirHorario(string id)
    {
        if (string.IsNullOrEmpty(id))
        return BadRequest("ID do horário não pode ser vazio.");

        var horario = await _horariosService.ConsultarId(id);
        if (horario == null)
            return NotFound("Horário não encontrado.");

        await _horariosService.Excluir(id);
        return Ok(new { message = "Horário excluído com sucesso." });
    }


}