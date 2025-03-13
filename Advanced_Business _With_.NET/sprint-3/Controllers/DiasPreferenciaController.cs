using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Route("DiasPreferencia")] 
public class DiasPreferenciaController : Controller
{
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

            var dia = new DiasPreferencia
            {
                DiasSemana = diasPreferenciaDTO.DiasSemana,
                IdUsuario = idUsuario
            };

            await _diasPreferenciaService.Criar(dia);

            TempData["SuccessMessage"] = "Preferência do dia cadastrado com sucesso!";
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
    ///     Cadastra um novo dia de preferência para um usuário autenticado.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Cadastra um ou mais dias de preferência que o usuário pode ter de recomendação para uma consulta
    /// 
    /// Use este endpoint quando precisar registrar um dia como preferência para receber sugestão de consultas.
    /// 
    /// ### Campos que devem ser utilizados para criar um novo dia:
    /// 
    /// - **idUsuario**: IdUsuario logado na sessão
    /// - **DiasSemana**: Dia deseja sendo Segunda-feira, Terça-feira, Quarta-feira, Quinta-feira, Sexta-feira ou Sábado.
    /// 
    /// ### Exemplo de body a ser informado para criar o dia de preferência do usuário:
    /// 
    /// ```json
    ///     {   
    ///         "idUsuario": "67cc95b32811515d372209ce",
    ///         "diaSemana": "Segunda-feira"
    ///     }
    /// ``` 
    /// 
    /// ### Campos que não devem ser utilizados para criar um novo dia:
    /// - **id** : Id será criado de forma automática pelo banco de dados.
    /// 
    /// ### Exemplo de body de retorno:
    /// 
    /// ```json
    ///     "Dia de preferência cadastrado com sucesso."
    /// ```
    /// </remarks>
    /// 
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
        if (diasPreferenciaDTO == null || string.IsNullOrEmpty(diasPreferenciaDTO.IdUsuario) || string.IsNullOrEmpty(diasPreferenciaDTO.DiasSemana))
        {
            return BadRequest("Todos os campos são obrigatórios.");
        }

        var dia = new DiasPreferencia
        {
            IdUsuario = diasPreferenciaDTO.IdUsuario,
            DiasSemana = diasPreferenciaDTO.DiasSemana
            
        };

        await _diasPreferenciaService.Criar(dia);
        return Ok("Dia de preferência cadastrado com sucesso.");
    }

    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var dias = await _diasPreferenciaService.ConsultarTodos(); 
        return View(dias); 
    }


    /// <summary>
    ///     Obtém todas as preferências de dias cadastradas no sistema pelo cliente.
    /// </summary>
    /// <remarks>
    /// 
    /// ## Consultar uma lista dos dias que foram cadastrados como preferência
    /// 
    /// Use este endpoint quando precisar consultar todos os dias cadastrados pelo usuário com todos campos específicos de uma clínica de retorno.
    /// 
    /// ### Campos que devem ser utilizados para consultar um dia de preferência:
    /// 
    /// - **Não é necessário informar nenhum dado, apenas clicar em executar que uma lista será concedida**
    /// 
    /// ### Exemplo da resposta que será devolvida quando a consulta está correta:
    /// 
    /// ```json
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
    /// ]
    /// ``` 
    /// 
    /// </remarks>
    /// <returns>Retorna uma lista contendo todas as preferências de dias.</returns>
    /// <response code="200">Retorna a lista de preferências.</response>
    [HttpGet("ConsultarTodosDias")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarTodosDias()
    {
        var dias = await _diasPreferenciaService.ConsultarTodos();
        return Ok(dias);
    }

    /// <summary>
    ///     Consultar um único registro de preferência do dia que o usuário cadastrou.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar uma único dia de preferência registrado, sendo consultado pelo ID do banco de dados
    /// 
    /// Use este endpoint quando precisar consultar somente um registro com todos campos específicos.
    /// 
    /// ### Campos que devem ser utilizados para consultar um dia de preferência:
    /// 
    /// - **id**: ID do banco e não o idUsuario
    /// 
    ///  ### Exemplo de body para requisição:
    ///  
    /// ```json
    ///     "id": "67ce3f3b334baff73edd38b6"
    /// ```
    /// 
    /// ### Exemplo de body que receberemos como resposta:
    /// 
    /// ```json
    ///    {
    ///        "id": "67ce3f3b334baff73edd38b6",
    ///        "idUsuario": "67cc95b32811515d372209ce",
    ///        "diaSemana": "Segunda-feira"
    ///     }
    ///  
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Dia consultado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarDiaId/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarDiaId(string id)
    {
        var clinica = await _diasPreferenciaService.ConsultarId(id);

        if (clinica == null)
        {
            return NotFound(new { message = "Clínica não encontrada." });
        }

        return Ok(clinica);
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
    ///     Atualiza as preferências de dias de um usuário autenticado.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Atualizar a preferência de dia cadastrado pelo usuário
    /// 
    /// Use este endpoint se o objetivo for alterar no cadastro um ou mais dias de preferência que o usuário deseja ser atendido.
    /// 
    /// ### Exemplo de requisição
    /// 
    /// ```json
    ///     {   
    ///         "id": "67ce4b3d61760e36f862dd59",
    ///         "idUsuario": "67cc95b32811515d372209ce",
    ///         "DiasSemana": "Sábado"
    ///     }
    /// 
    /// ``` 
    /// 
    /// ### Exemplo do modelo de resposta quando ocorre sucesso na alteração
    /// 
    /// ```json
    ///     {
    ///         "id": "67cf5a8b8f6c3c886a4deb56",
    ///         "idUsuario": "67cc95b32811515d372209ce",
    ///         "diasSemana": "Segunda-feira"
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
    [HttpPut("AtualizarDiaPreferencia")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarDiaPreferencia(string id, [FromBody] DiasPreferencia diasPreferencia)
    {
        if (diasPreferencia == null || string.IsNullOrEmpty(id) || string.IsNullOrEmpty(diasPreferencia.DiasSemana))
        {
            return BadRequest("Todos os campos são obrigatórios.");
        }

        var diaExistente = await _diasPreferenciaService.ConsultarId(id);
        if (diaExistente == null)
            return NotFound("Preferência de dias não encontrada.");

        diaExistente.DiasSemana = diasPreferencia.DiasSemana;
        
        await _diasPreferenciaService.Atualizar(diaExistente);

        return Ok(diaExistente);
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
    /// 
    /// <remarks>
    /// 
    /// ## Excluir um dia de preferência do usuário do cadastro
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
    ///         "message": "Dia de preferência excluído com sucesso."
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
        return Ok(new { message = "Dia de preferência excluído com sucesso." });
    }

}