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
    ///     Cadastra um novo horário de preferência para o usuário.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Cadastrar um novo horário de preferência para um usuário
    /// 
    /// Use este endPoint se o objetivo e cadastrar um novo horário de preferência, lembrando que o usuário pode ter vários em seu cadastro.
    /// 
    /// ### Campos que devem ser utilizados para cadastrar um horário de preferência:
    /// - **idUsuario**: ID do banco e não o idUsuario
    /// - **HorariosPreferencia**: Informe o horário como string e no formato "08:00"
    /// 
    /// ### Campos que não devem ser utilizados para cadastrar um horário de preferência:
    /// - **Id**: Não é necessário informar o id, pois ele será gerado automaticamente pelo
    /// 
    /// ### Exemplo de body da requisição:
    /// ```json
    ///     {   
    ///         "idUsuario": "67cc95b32811515d372209ce",
    ///         "horariosPreferencia": "07:00"
    ///     }
    /// ``` 
    /// 
    /// ### Exemplo de body da resposto quando o cadastro é bem sucedido
    /// ```json
    ///     {
    ///         "id": "67cf6c2a446498c2b6eb0a90",
    ///         "idUsuario": "67cc95b32811515d372209ce",
    ///         "horariosPreferencia": "22:00"
    ///     }
    ///```
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

        if (string.IsNullOrEmpty(horariosDTO.IdUsuario))
        return BadRequest("O campo IdUsuario é obrigatório.");

        var horario = new Horarios
        {
            IdUsuario = horariosDTO.IdUsuario,
            HorariosPreferencia = horariosDTO.HorariosPreferencia
            
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
    ///     Consultar todos os registros de horários de preferência no banco de dados.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar todos os registros de horários do banco de dados
    /// 
    /// Use este endPoint se seu objetivo é recuperar todos os registros de horários do banco de dados
    /// 
    /// ### Campos que disponíveis na requisição:
    /// - **id** : Id do banco de dados, que foi gerado automaticamente.
    /// - **idUsuario** : IdUsuario que deseja registrar o endereço
    /// - **HorariosPreferencia** string : Horário de preferência, podem ser vários.
    /// 
    /// ### Exemplo de body que virá de resposta:
    /// 
    /// ```json
    ///[
    ///    {
    ///        "id": "67cdee51b304fd2aaac177c9",
    ///        "idUsuario": "67cc95b32811515d372209ce",
    ///        "HorariosPreferencia": "07:00",
    ///    },
    ///    {
    ///        "id": "67cdee91b304fd2aaac177ca",
    ///        "idUsuario": "67cc95b32811515d372209ce",
    ///        "HorariosPreferencia": "22:00",
    ///    }
    ///]
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Horário consultado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarTodosHorarios")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarTodosHorarios()
    {
        var horarios = await _horariosService.ConsultarTodos(); 
        return Ok(horarios);
    }

    /// <summary>
    ///     Consultar um único registro de preferência de horário que o usuário cadastrou.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar um único horário de preferência registrado, sendo consultado pelo ID do banco de dados
    /// 
    /// Use este endpoint quando precisar consultar somente um registro com todos campos específicos.
    /// 
    /// ### Campos que devem ser utilizados para consultar um horário de preferência:
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
    ///         "horarioPreferencia": "22:00"
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Dia consultado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarHorarioId/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarHorarioId(string id)
    {
        var horario = await _horariosService.ConsultarId(id);

        if (horario == null)
        {
            return NotFound(new { message = "Horário não encontrado." });
        }

        return Ok(horario);
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
    ///     Atualiza as preferências de hoários de um usuário autenticado.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Atualizar a preferência de horários cadastrado pelo usuário
    /// 
    /// Use este endpoint se o objetivo for alterar no cadastro um ou mais hoários de preferência que o usuário deseja ser atendido.
    /// 
    /// ### Campos que devem ser utilizados para consultar um Horário de preferência:
    /// - **id**: ID do banco e não o idUsuario
    /// - **idUsuario**: IdUsuario gerado pelo banco de dados
    /// - **horariosPreferencia**: Campo em string com o horário de preferência neste exemplo "07:00"
    /// 
    /// ### Exemplo de requisição
    /// 
    /// ```json
    ///     {   
    ///         "id": "67ce3bb1c9562c029b01d3fe",
    ///         "idUsuario": "67cc95b32811515d372209ce",
    ///         "HorariosPreferencia": "24:00"
    ///     }
    /// 
    /// ``` 
    /// 
    /// ### Exemplo do modelo de resposta quando ocorre sucesso na alteração
    /// 
    /// ```json
    ///     {
    ///         "id": "67ce3bb1c9562c029b01d3fe",
    ///         "idUsuario": "67cc95b32811515d372209ce",
    ///         "HorariosPreferencia": "24:00"
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
    [HttpPut("AtualizarHorario/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarHorario(string id, [FromBody] HorariosDTO horariosDTO)
    {
        if (horariosDTO.Id == null)
            return BadRequest(new { message = "ID do horário não pode ser nulo." });

        var horarioExistente = await _horariosService.ConsultarId(horariosDTO.Id);
        if (horarioExistente == null)
            return NotFound(new { message = "Horário não encontrado." });

        horarioExistente.HorariosPreferencia = horariosDTO.HorariosPreferencia;

        await _horariosService.Atualizar(horarioExistente);

        return Ok(new
        {
            id = horarioExistente.Id,
            idUsuario = horarioExistente.IdUsuario,
            horariosPreferencia = horarioExistente.HorariosPreferencia,
            mensagem = "Horário atualizado com sucesso!"
        });
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
    ///     Exclui um horário de preferência do usuário.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Excluir um horário de preferência do usuário do cadastro feito no banco.
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
    ///         "message": "Horário de preferência excluído com sucesso."
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