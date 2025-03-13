using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("ServicosAgendados")] 
public class ServicosAgendadosController : Controller
{
    private readonly IServicosAgendadosService _servicoService;

    public ServicosAgendadosController(IServicosAgendadosService servicoService)
    {
        _servicoService = servicoService;
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
    public async Task<IActionResult> Criar(ServicosAgendados servico)
    {
        if (ModelState.IsValid)
        {
            await _servicoService.Criar(servico);
            TempData["SuccessMessage"] = "Serviços cadastrado com sucesso!";
            return RedirectToAction("Mensagem");
        }
        return View(servico);
    }

    [HttpGet("Mensagem")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Mensagem()
    {
        return View();
    }

    /// <summary>
    ///     Cria um novo Serviço Agendado.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Cadastrar novo Serviço Agendado no banco de dados
    /// 
    /// Use este endpoint para cadastrar um Serviço Agendado. Somente serviços cadastrados podem ser gerenciados na plataforma.
    /// 
    /// ### Campos que devem ser utilizados para criar um novo serviço agendado:
    /// - **idUsuario** string : Identificador do usuário que agendou o serviço
    /// - **nomeCliente** string : Nome completo do cliente
    /// - **data** DateOnly : Data do agendamento no formato YYYY-MM-DD
    /// - **turno** string : Período do dia (Manhã, Tarde ou Noite)
    /// - **horario** string : Horário exato do agendamento
    /// - **idClinica** string : Identificador da clínica onde ocorrerá o atendimento
    /// - **nomeClinica** string : Nome da clínica
    /// - **nomeMedico** string : Nome do médico responsável pelo atendimento
    /// - **especialidade** string : Especialidade do médico
    /// - **cepClinica** string : CEP da clínica
    /// - **estadoClinica** string : Estado onde a clínica está localizada
    /// - **cidadeClinica** string : Cidade onde a clínica está localizada
    /// - **bairroClinica** string : Bairro onde a clínica está localizada
    /// - **ruaClinica** string : Rua onde a clínica está localizada
    /// 
    /// ### Campos que não devem ser enviados na criação de um novo serviço agendado:
    /// - **id** : Gerado automaticamente pelo banco de dados
    /// 
    /// ### Exemplo de body para requisição:
    /// ```json
    /// {
    ///     "idUsuario": "12345",
    ///     "nomeCliente": "João Silva",
    ///     "data": "2025-06-15",
    ///     "turno": "Manhã",
    ///     "horario": "08:30",
    ///     "idClinica": "67890",
    ///     "nomeClinica": "Clínica Exemplo",
    ///     "nomeMedico": "Dr. Carlos",
    ///     "especialidade": "Cardiologia",
    ///     "cepClinica": "01001-000",
    ///     "estadoClinica": "SP",
    ///     "cidadeClinica": "São Paulo",
    ///     "bairroClinica": "Centro",
    ///     "ruaClinica": "Rua das Flores, 123"
    /// }
    /// ```
    /// 
    /// ### Exemplo de resposta quando o cadastro for efetuado:
    /// 
    /// ```json
    /// {
    ///     "id": "67d10436a54c286399e049f4",
    ///     "idUsuario": "12345",
    ///     "nomeCliente": "João Silva",
    ///     "data": "2025-06-15",
    ///     "turno": "Manhã",
    ///     "horario": "08:30",
    ///     "idClinica": "67890",
    ///     "nomeClinica": "Clínica Exemplo",
    ///     "nomeMedico": "Dr. Carlos",
    ///     "especialidade": "Cardiologia",
    ///     "cepClinica": "01001-000",
    ///     "estadoClinica": "SP",
    ///     "cidadeClinica": "São Paulo",
    ///     "bairroClinica": "Centro",
    ///     "ruaClinica": "Rua das Flores, 123"
    /// }
    /// ```
    /// </remarks>
    /// <response code="201">Serviço agendado criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost("CadastrarServicosAgendados")]
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CadastrarServicosAgendados([FromBody] ServicosAgendados servico)
    {
        if (ModelState.IsValid)
        {
            await _servicoService.Criar(servico);
            return CreatedAtAction(nameof(ConsultarTodosServicos), new { id = servico.Id }, servico); 
        }
        return BadRequest(ModelState); 
    }

    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var servicos = await _servicoService.ConsultarTodos(); 
        return View(servicos); 
    }

    /// <summary>
    ///     Consultar a lista com todos os serviços agendados.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar todos os registros de serviços agendados no banco de dados
    /// 
    /// Use este endpoint se seu objetivo é recuperar todos os registros de serviços agendados no banco de dados.
    /// 
    /// ### Campos disponíveis para consulta:
    /// - **id** : Identificador único do agendamento, gerado automaticamente.
    /// - **idUsuario** string : ID do usuário associado ao agendamento.
    /// - **nomeCliente** string : Nome do cliente que agendou o serviço.
    /// - **data** DateOnly : Data do agendamento.
    /// - **turno** string : Turno do agendamento (Manhã, Tarde ou Noite).
    /// - **horario** string : Horário específico do agendamento.
    /// - **idClinica** string : Identificador único da clínica onde o serviço será realizado.
    /// - **nomeClinica** string : Nome da clínica onde o serviço será realizado.
    /// - **nomeMedico** string : Nome do médico responsável pelo atendimento.
    /// - **especialidade** string : Especialidade do médico responsável pelo atendimento.
    /// - **cepClinica** string : CEP da clínica onde o serviço será realizado.
    /// - **estadoClinica** string : Estado onde a clínica está localizada.
    /// - **cidadeClinica** string : Cidade onde a clínica está localizada.
    /// - **bairroClinica** string : Bairro onde a clínica está localizada.
    /// - **ruaClinica** string : Nome da rua da clínica onde o serviço será realizado.
    /// 
    /// ### Exemplo de resposta (lista de serviços agendados):
    /// ```json
    ///     [
    ///         { 
    ///             "id": "67d10436a54c286399e049f4",
    ///             "idUsuario": "abc123",
    ///             "nomeCliente": "João Silva",
    ///             "data": "2025-03-15",
    ///             "turno": "Tarde",
    ///             "horario": "14:00",
    ///             "idClinica": "clinica456",
    ///             "nomeClinica": "Clínica Saúde Total",
    ///             "nomeMedico": "Dra. Ana Souza",
    ///             "especialidade": "Odontologia",
    ///             "cepClinica": "01001-000",
    ///             "estadoClinica": "SP",
    ///             "cidadeClinica": "São Paulo",
    ///             "bairroClinica": "Centro",
    ///             "ruaClinica": "Rua das Flores, 123"
    ///         },
    ///         {
    ///             "id": "67d10436a54c286399e049f4",
    ///             "idUsuario": "xyz789",
    ///             "nomeCliente": "Maria Oliveira",
    ///             "data": "2025-03-16",
    ///             "turno": "Manhã",
    ///             "horario": "09:30",
    ///             "idClinica": "clinica789",
    ///             "nomeClinica": "Centro Médico Vida",
    ///             "nomeMedico": "Dr. Carlos Mendes",
    ///             "especialidade": "Cardiologia",
    ///             "cepClinica": "22041-001",
    ///             "estadoClinica": "RJ",
    ///             "cidadeClinica": "Rio de Janeiro",
    ///             "bairroClinica": "Copacabana",
    ///             "ruaClinica": "Av. Atlântica, 500"
    ///         }
    ///     ]
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Consulta realizada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarTodosServicos")]
    [Produces("application/json")]
    public async Task<IActionResult> ConsultarTodosServicos()
    {
        var servicos = await _servicoService.ConsultarTodos();
        return Ok(servicos);
    }

    /// <summary>
    ///     Consultar um único serviço agendado
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar um único serviço no banco de dados pelo ID
    /// 
    /// Use este endpoint quando precisar recuperar apenas um registro de serviço agendado com todos os detalhes.
    /// 
    /// ### Parâmetro necessário para consulta:
    /// 
    /// - **id**: ID único do serviço agendado no banco de dados.
    /// 
    ///  ### Exemplo de body para requisição:
    ///  
    /// ```json
    ///     {
    ///         "id": "67cc95b32811515d372209ce"
    ///     }
    /// ```
    /// 
    /// ### Exemplo de body da resposta esperada:
    /// 
    /// ```json
    ///    {
    ///         "id": "67d10436a54c286399e049f4",
    ///         "idUsuario": "abc123",
    ///         "nomeCliente": "João Silva",
    ///         "data": "2025-03-15",
    ///         "turno": "Tarde",
    ///         "horario": "14:00",
    ///         "idClinica": "clinica456",
    ///         "nomeClinica": "Clínica Saúde Total",
    ///         "nomeMedico": "Dra. Ana Souza",
    ///         "especialidade": "Odontologia",
    ///         "cepClinica": "01001-000",
    ///         "estadoClinica": "SP",
    ///         "cidadeClinica": "São Paulo",
    ///         "bairroClinica": "Centro",
    ///         "ruaClinica": "Rua das Flores, 123"
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Serviço agendado consultado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Serviço não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarServicoId/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarServicoId(string id)
    {
        var servico = await _servicoService.ConsultarId(id);

        if (servico == null)
        {
            return NotFound(new { message = "Serviços não encontrado." });
        }

        return Ok(servico);
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

        var servico = await _servicoService.ConsultarId(userIdString);
        if (servico == null)
        {
            return NotFound();
        }

        return View(servico);
    }

    [HttpPost("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar(ServicosAgendados servico)
    {
        if (!ModelState.IsValid)
        {
            return View(servico);
        }

        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var servicoExistente = await _servicoService.ConsultarId(userIdString);

        if (servicoExistente == null)
        {
            return NotFound();
        }

        servicoExistente.IdUsuario = servico.IdUsuario;
        servicoExistente.NomeCliente = servico.NomeCliente;
        servicoExistente.Data = servico.Data;
        servicoExistente.Turno = servico.Turno;
        servicoExistente.Horario = servico.Horario;
        servicoExistente.IdClinica = servico.IdClinica;
        servicoExistente.NomeClinica = servico.NomeClinica;
        servicoExistente.CEPClinica = servico.CEPClinica;
        servicoExistente.EstadoClinica = servico.EstadoClinica;
        servicoExistente.CidadeClinica = servico.CidadeClinica;
        servicoExistente.BairroClinica = servico.BairroClinica;
        servicoExistente.RuaClinica = servico.RuaClinica;

        await _servicoService.Atualizar(servicoExistente);

        TempData["SuccessMessage"] = "Serviços atualizado com sucesso!";
        return RedirectToAction("MensagemAtualizacao");
    }

    [HttpGet("MensagemAtualizacao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemAtualizacao()
    {
        return View();
    }

    /// <summary>
    ///     Atualiza todos os dados existentes de um serviço agendado com base no ID do banco de dados.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Atualizar todas as informações de um serviço no banco
    /// 
    /// Use este endpoint se o objetivo for atualizar todos os campos de um serviço agendado no cadastro.  
    /// Se a atualização for parcial, utilize o endpoint específico para essa operação.
    /// 
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    ///     {   
    ///         "id": "67d10436a54c286399e049f4",
    ///         "idUsuario": "abc123",
    ///         "nomeCliente": "Patricia Delfos",
    ///         "data": "2025-03-20",
    ///         "turno": "Manhã",
    ///         "horario": "09:30",
    ///         "idClinica": "clinica456",
    ///         "nomeClinica": "Clínica Saúde Total",
    ///         "nomeMedico": "Dr. Marcos Lima",
    ///         "especialidade": "Odontologia",
    ///         "cepClinica": "01001-000",
    ///         "estadoClinica": "SP",
    ///         "cidadeClinica": "São Paulo",
    ///         "bairroClinica": "Centro",
    ///         "ruaClinica": "Av. Paulista, 1500"
    ///     }
    /// ``` 
    /// 
    /// ### Importante:
    /// - Todos os campos devem ser preenchidos corretamente.
    /// - Se algum campo for omitido, o sistema pode sobrescrevê-lo com um valor padrão ou inválido.
    /// </remarks>
    /// 
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do serviço no banco de dados.</param>
    /// <param name="servico">Dados do serviço agendado a serem atualizados.</param>
    /// <response code="200">Serviço atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="401">Usuário não autorizado</response>
    /// <response code="404">Serviço não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut("AtualizarServicos/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> AtualizarServicos(string id, [FromBody] ServicosAgendados servico)
    {
        if (string.IsNullOrEmpty(id) || servico == null || id != servico.Id)
        {
            return BadRequest("Id do Serviço não corresponde ao fornecido.");
        }

        var servicoExistente = await _servicoService.ConsultarId(id);

        if (servicoExistente == null)
        {
            return NotFound();
        }


        servicoExistente.IdUsuario = servico.IdUsuario;
        servicoExistente.NomeCliente = servico.NomeCliente;
        servicoExistente.Data = servico.Data;
        servicoExistente.Turno = servico.Turno;
        servicoExistente.Horario = servico.Horario;
        servicoExistente.IdClinica = servico.IdClinica;
        servicoExistente.NomeClinica = servico.NomeClinica;
        servicoExistente.CEPClinica = servico.CEPClinica;
        servicoExistente.EstadoClinica = servico.EstadoClinica;
        servicoExistente.CidadeClinica = servico.CidadeClinica;
        servicoExistente.BairroClinica = servico.BairroClinica;
        servicoExistente.RuaClinica = servico.RuaClinica;

        await _servicoService.Atualizar(servicoExistente);

        return Ok(servicoExistente); 
    }


    [HttpGet("ConfirmarExcluir/{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmarExcluir(string id)
    {
        var servico = await _servicoService.ConsultarId(id);
        
        if (servico == null)
        {
            return NotFound();
        }

        return View(servico);
    }

    /// <summary>
    ///     Atualiza parcialmente os dados de um serviço agendado existente.
    /// </summary>
    /// 
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do serviço a ser atualizado.</param>
    /// 
    /// <remarks>
    /// 
    /// ## Atualização Parcial de um Serviço Agendado
    /// 
    /// Use este endpoint quando precisar atualizar apenas alguns campos específicos de um serviço agendado,
    /// sem a necessidade de enviar todos os dados.
    /// 
    /// ### Campos que podem ser atualizados:
    /// - **data**: Data do serviço agendado no formato YYYY-MM-DD
    /// - **turno**: Período do serviço (Manhã, Tarde ou Noite)
    /// - **horario**: Horário específico do serviço
    /// - **nomeClinica**: Nome da clínica onde o serviço será realizado
    /// - **nomeMedico**: Nome do profissional responsável pelo atendimento
    /// - **especialidade**: Especialidade do serviço médico
    /// 
    /// ### Campos que não podem ser atualizados:
    /// - **idUsuario**: O ID do usuário não pode ser alterado.
    /// - **id**: O ID do serviço também não pode ser alterado.
    /// - **idClinica**: ID da clínica onde o serviço será realizado
    ///
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    /// {
    ///     "id": "67cc95b32811515d372209ce",
    ///     "horario": "10:30",
    ///     "turno": "Tarde"
    /// }
    /// ```
    /// 
    /// Somente os campos incluídos no corpo da requisição serão atualizados.
    /// 
    /// ### Exemplo de resposta da requisição:
    /// 
    /// ```json
    /// {
    ///     "id": "67d10436a54c286399e049f4",
    ///     "idUsuario": "abc123",
    ///     "nomeCliente": "Patricia Delfos",
    ///     "data": "2025-03-20",
    ///     "turno": "Tarde",
    ///     "horario": "10:30",
    ///     "idClinica": "clinica456",
    ///     "nomeClinica": "Clínica Saúde Total",
    ///     "nomeMedico": "Dr. Marcos Lima",
    ///     "especialidade": "Odontologia",
    ///     "cepClinica": "01001-000",
    ///     "estadoClinica": "SP",
    ///     "cidadeClinica": "São Paulo",
    ///     "bairroClinica": "Centro",
    ///     "ruaClinica": "Av. Paulista, 1500"
    /// }
    /// ```
    /// </remarks>
    /// 
    /// <param name="camposParaAtualizar"></param>
    /// 
    /// <response code="200">Serviço atualizado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Serviço não encontrado</response>
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

        var servicosAtualizados = await _servicoService.AtualizarParcial(id, camposParaAtualizar);

        if (servicosAtualizados == null)
        {
            return NotFound("Serviços não encontrada.");
        }

        return Ok(servicosAtualizados);
    }


    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Excluir(string id)
    {
        var servico = await _servicoService.ConsultarId(id);
        
        if (servico != null)
        {
            await _servicoService.Excluir(id);

            await HttpContext.SignOutAsync();
            
            TempData["SuccessMessage"] = "Serviços excluído com sucesso.";
            return RedirectToAction("MensagemExclusao", "ServicosAgendados"); 
        }

        TempData["ErrorMessage"] = "Serviços não encontrado.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("MensagemExclusao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemExclusao()
    {
        return View();
    }

    /// <summary>
    ///     Excluir os Serviços do banco de dados.
    /// </summary>
    ///
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do Serviços a ser excluído</param>
    /// 
    /// <remarks>
    /// 
    /// ## Excluir um Serviços do banco de dados e dos cadastros.
    /// 
    /// ### Exemplo da requisição para excluir um Serviços:
    /// 
    /// ```json
    ///     {
    ///         "id": "67d10436a54c286399e049f4",
    ///     }
    /// ```
    /// 
    /// ### Exemplo da resposta para excluir um Serviços:
    /// 
    /// ```json
    ///     {
    ///         "message": "Serviços excluído com sucesso."
    ///     }
    /// ``` 
    /// 
    /// Uma vez excluida da base, não tem reversão desta ação.
    /// </remarks>
    /// 
    /// <response code="200">Serviços criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete("ExcluirServicos/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> ExcluirServicos(string id)
    {
        var servico = await _servicoService.ConsultarId(id);
        
        if (servico == null)
        {
            return NotFound();
        }

        await _servicoService.Excluir(id);

        return Ok(new { message = "Serviços excluído com sucesso." });  
    }

}