using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("DiasPreferencia")] 
public class DiasPreferenciaController : Controller
{
    //private readonly ApplicationDbContext _context;
    private readonly IDiasPreferenciaService _diasPreferenciaService;

    public DiasPreferenciaController(IDiasPreferenciaService diasPreferenciaService)
    {
        _diasPreferenciaService = diasPreferenciaService;
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
    public async Task<IActionResult> Criar(DiasPreferenciaDTO diasPreferenciaDTO)
    {

        if (ModelState.IsValid)
        {

            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;
            if (string.IsNullOrEmpty(idUsuario))
                return Unauthorized("Usuário não logado.");



            // Cria o objeto Endereco
            var dia = new DiasPreferencia
            {
                DiasSemana = diasPreferenciaDTO.DiasSemana,
                IdUsuario = idUsuario
            };

            await _diasPreferenciaService.Criar(dia);

            TempData["SuccessMessage"] = "Preferencia do dia cadastrado com sucesso!";
            //return RedirectToAction("Mensagem");
        }
        return View(diasPreferenciaDTO);
    }

    [HttpGet("Mensagem")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Mensagem()
    {
        return View();
    }

    /// <summary>
    /// Cadastra um novo dia de preferência para um usuário autenticado.
    /// </summary>
    /// <remarks>
    /// Exemplo de body a ser informado para criar o dia de preferência do usuário:
    /// 
    /// {   
    ///     "idUsuario": "67cc95b32811515d372209ce",
    ///     "diaSemana": "Segunda-feira"
    /// }
    /// </remarks>
    /// <returns>Retorna 201 Created se o cadastro for bem-sucedido.</returns>
    /// <response code="201">Preferência de dia cadastrada com sucesso.</response>
    /// <response code="400">Erro na requisição (dados inválidos).</response>
    /// <response code="401">Usuário não autenticado.</response>
    [HttpPost("CadastrarDia")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CadastrarDia([FromBody] DiasPreferenciaDTO diasPreferenciaDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest("Dados inválidos.");

        var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;
        if (string.IsNullOrEmpty(idUsuario))
            return Unauthorized("Usuário não autenticado.");

        var dia = new DiasPreferencia
        {
            DiasSemana = diasPreferenciaDTO.DiasSemana,
            IdUsuario = idUsuario
        };

        await _diasPreferenciaService.Criar(dia);
        return CreatedAtAction(nameof(Consultar), new { usuarioId = idUsuario }, dia);
    }

    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var dias = await _diasPreferenciaService.ConsultarTodos(); 
        return View(dias); 
    }


    /// <summary>
    /// Obtém todas as preferências de dias cadastradas no sistema pelo cliente.
    /// </summary>
    /// <remarks>
    /// Exemplo da resposta que será devolvida quando a consulta está correta:
    /// 
    /// [
    ///   {
    ///      "id": "67ce27cdd664f98dbe755904",
    ///      "idUsuario": "67cc95b32811515d372209ce",
    ///      "diasSemana": "Segunda"
    ///  },
    ///  {
    ///      "id": "67ce27d2d664f98dbe755905",
    ///      "idUsuario": "67cc95b32811515d372209ce",
    ///      "diasSemana": "Segunda"
    ///  },
    ///  {
    ///      "id": "67ce33dff151c271f57510af",
    ///      "idUsuario": "67cc95b32811515d372209ce",
    ///      "diasSemana": "Terça-feira"
    ///  }
    ///]
    /// </remarks>
    /// <returns>Retorna uma lista contendo todas as preferências de dias.</returns>
    /// <response code="200">Retorna a lista de preferências.</response>
    [HttpGet("ConsultarDias")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarDias()
    {
        var dias = await _diasPreferenciaService.ConsultarTodos();
        return Ok(dias);
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

        var dia = await _diasPreferenciaService.ConsultarPorUsuarioId(userIdString);
        if (dia == null)
        {
            return NotFound();
        }

        return View(dia);
    }

    [HttpPost("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar(DiasPreferencia dia)
    {
        if (!ModelState.IsValid)
        {
            return View(dia);
        }

        //var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var diaExistente = await _diasPreferenciaService.ConsultarPorUsuarioId(userIdString);

        if (diaExistente == null)
        {
            return NotFound();
        }

        diaExistente.DiasSemana = dia.DiasSemana;
       

        await _diasPreferenciaService.Atualizar(diaExistente);

        TempData["SuccessMessage"] = "Dado atualizado com sucesso!";
        return RedirectToAction("Consultar");
    }

    /// <summary>
    /// Atualiza as preferências de dias de um usuário autenticado.
    /// </summary>
    /// <param name="diasPreferencia">Objeto contendo os novos dias de preferência.</param>
    /// <returns>Retorna 204 No Content se a atualização for bem-sucedida.</returns>
    /// <response code="204">Dados atualizados com sucesso.</response>
    /// <response code="400">Erro na requisição (dados inválidos).</response>
    /// <response code="401">Usuário não autenticado.</response>
    /// <response code="404">Nenhum registro encontrado para o usuário.</response>
    [HttpPut("AtualizarDiaPreferencia")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarDia([FromBody] DiasPreferencia diasPreferencia)
    {
        if (!ModelState.IsValid)
            return BadRequest("Dados inválidos.");

        var userId = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("Usuário não autenticado.");

        var diaExistente = await _diasPreferenciaService.ConsultarPorUsuarioId(userId);
        if (diaExistente == null)
            return NotFound("Preferência de dias não encontrada.");

        diaExistente.DiasSemana = diasPreferencia.DiasSemana;
        await _diasPreferenciaService.Atualizar(diaExistente);

        return NoContent();
    }



    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Excluir(string idDia)
    {
        Console.WriteLine($"Excluindo DiasPreferencia com Id: {idDia}");

        try
        {
            await _diasPreferenciaService.Excluir(idDia);
            TempData["SuccessMessage"] = "Exclusão realizada com sucesso!";
        }
        catch (ArgumentException ex)
        {
            TempData["ErrorMessage"] = $"Erro: {ex.Message}";
        }

        return RedirectToAction("Consultar");
    }

    
    /// <summary>
    /// Exclui um dia de preferência do usuário.
    /// </summary>
    /// <param name="id" type="string" example="67cdee51b304fd2aaac177c9">ID do dia a ser excluído.</param>
    /// <response code="200">Dia excluído com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    /// <response code="404">Dia não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete("ExcluirEndereco/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExcluirEndereco(string id)
    {
        if (string.IsNullOrEmpty(id))
        return BadRequest("ID do dia não pode ser vazio.");

        var endereco = await _diasPreferenciaService.ConsultarId(id);
        if (endereco == null)
            return NotFound("Dia não encontrado.");

        await _diasPreferenciaService.Excluir(id);
        return Ok(new { message = "Dia excluído com sucesso." });
    }

}