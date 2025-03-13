using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("SugestaoConsultaCliente")] 
public class SugestaoConsultaClienteController : Controller
{
    private readonly ISugestaoConsultaClienteService _sugestaoService;

    public SugestaoConsultaClienteController(ISugestaoConsultaClienteService sugestaoService)
    {
        _sugestaoService = sugestaoService;
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
    public async Task<IActionResult> Criar(SugestaoConsultaCliente sugestao)
    {
        if (ModelState.IsValid)
        {
            await _sugestaoService.Criar(sugestao);
            TempData["SuccessMessage"] = "Sugestão cadastrada com sucesso!";
        }
        return View(sugestao);
    }


    /// <summary>
    ///     Cria uma novo Sugestão.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Cadastrar uma nova Sugestão no banco de dados
    /// 
    /// Use este endPoint para cadastrar um Sugestão. Somente Sugestãos cadastrados podem efetuar o Login na plataforma.
    /// 
    /// ### Campos disponíveis para consultar as Sugestões:
    /// - **id** : Id do registro da sugestão, gerado automaticamente.
    /// - **IdClinica** : Identificador único da clínica associada à sugestão.
    /// - **NomeClinica** : Nome da clínica associada à sugestão.
    /// - **TelefoneClinica** : Número de telefone de contato da clínica.
    /// - **EmailClinica** : Endereço de e-mail principal de contato da clínica.
    /// - **IdUsuario** : Identificador único do usuário associado à sugestão.
    /// - **NomeCliente** : Nome completo do cliente associado à sugestão.
    /// - **TelefoneCliente** : Número de telefone de contato do cliente.
    /// - **EmailCliente** : Endereço de e-mail principal de contato do cliente.
    /// - **DiaPreferenciaCliente** : Dia de preferência do cliente para atendimento.
    /// - **TurnoPreferenciaCliente** : Turno de preferência do cliente para atendimento (ex: manhã, tarde ou noite).
    /// - **HorarioPreferenciaCliente** : Horário de preferência do cliente para atendimento.
    /// - **Especialidade** : Especialidade desejada para o atendimento (ex: Ortodontia).
    /// - **CEPPreferenciaCliente** : CEP de preferência do cliente para o atendimento.
    /// - **EstadoPreferenciaCliente** : Estado de preferência do cliente para o atendimento.
    /// - **CidadePreferenciaCliente** : Cidade de preferência do cliente para o atendimento.
    /// - **BairroPreferenciaCliente** : Bairro de preferência do cliente para o atendimento.
    /// - **RuaPreferenciaCliente** : Rua de preferência do cliente para o atendimento.
    /// - **StatusSugestaoClinica** : Status da sugestão na clínica (ex: Aprovada, Aceita).
    /// - **StatusSugestaoCliente** : Status da sugestão para o cliente (ex: Confirmada, Aceita).
    /// - **DataAlteracao** : Data da última alteração realizada na sugestão.
    /// - **DataConsulta** : Data base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// - **HorarioConsulta** : Hora base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// 
    /// ### Exemplo de body para requisição:
    /// ```json
    ///    
    ///     {
    ///         "idClinica": "63d234ab65f5b32d7a7c0011",
    ///         "nomeClinica": "Clínica Odonto Bem",
    ///         "telefoneClinica": "1123456789",
    ///         "emailClinica": "contato@odontobem.com",
    ///         "idUsuario": "987654321",
    ///         "nomeCliente": "Carlos Oliveira",
    ///         "telefoneCliente": "11987654321",
    ///         "emailCliente": "carlos.oliveira@email.com",
    ///         "diaPreferenciaCliente": "Sexta-feira",
    ///         "turnoPreferenciaCliente": "Noite",
    ///         "horarioPreferenciaCliente": "19:30",
    ///         "especilidade": "Ortodontia",
    ///         "cepClinica": "01452000",
    ///         "estadoClinica": "SP",
    ///         "cidadeClinica": "São Paulo",
    ///         "bairroClinica": "Pinheiros",
    ///         "ruaClinica": "Rua dos Três Irmãos",
    ///         "statusSugestaoClinica": "Aprovada",
    ///         "statusSugestaoCliente": "Confirmada",
    ///         "dataAlteracao": "2025-03-11T15:00:00",
    ///         "dataConsulta": "2025-03-12",
    ///         "horarioConsulta": "19:30"
    ///     }
    /// ```
    /// 
    /// ### Exemplo de resposta quando o cadastro for efetuado:
    /// 
    /// ```json
    ///     {
    ///         "id": "67d0af03c335e74f3472265e",
    ///         "idClinica": "63d234ab65f5b32d7a7c0011",
    ///         "nomeClinica": "Clínica Odonto Bem",
    ///         "telefoneClinica": "1123456789",
    ///         "emailClinica": "contato@odontobem.com",
    ///         "idUsuario": "987654321",
    ///         "nomeCliente": "Carlos Oliveira",
    ///         "telefoneCliente": "11987654321",
    ///         "emailCliente": "carlos.oliveira@email.com",
    ///         "diaPreferenciaCliente": "Sexta-feira",
    ///         "turnoPreferenciaCliente": "Noite",
    ///         "horarioPreferenciaCliente": "19:30",
    ///         "especilidade": "Ortodontia",
    ///         "cepClinica": "01452000",
    ///         "estadoClinica": "SP",
    ///         "cidadeClinica": "São Paulo",
    ///         "bairroClinica": "Pinheiros",
    ///         "ruaClinica": "Rua dos Três Irmãos",
    ///         "statusSugestaoClinica": "Aprovada",
    ///         "statusSugestaoCliente": "Confirmada",
    ///         "dataAlteracao": "2025-03-11T15:00:00",
    ///         "dataConsulta": "2025-03-12",
    ///         "horarioConsulta": "19:30"
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Requisição realizada com sucesso</response>
    /// <response code="201">Sugestão criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost("CadastrarSugestao")]
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CadastrarSugestao([FromBody] SugestaoConsultaCliente sugestao)
    {
        if (ModelState.IsValid)
        {
            await _sugestaoService.Criar(sugestao);
            return CreatedAtAction(nameof(ConsultarTudo), new { id = sugestao.Id }, sugestao); 
        }
        return BadRequest(ModelState); 
    }

    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var sugestoes = await _sugestaoService.ConsultarTodos(); 
        return View(sugestoes); 
    }

    /// <summary>
    ///     Consultar a lista com todas as Sugestões.
    /// </summary>
    /// <remarks>
    /// ## Consultar todos os registros de Sugestões do banco de dados
    /// 
    /// Use este endpoint se seu objetivo for recuperar todos os registros de Sugestões do banco de dados.
    /// 
    /// ### Campos disponíveis para consultar as Sugestões:
    /// - **id** : Id do registro da sugestão, gerado automaticamente.
    /// - **IdClinica** : Identificador único da clínica associada à sugestão.
    /// - **NomeClinica** : Nome da clínica associada à sugestão.
    /// - **TelefoneClinica** : Número de telefone de contato da clínica.
    /// - **EmailClinica** : Endereço de e-mail principal de contato da clínica.
    /// - **IdUsuario** : Identificador único do usuário associado à sugestão.
    /// - **NomeCliente** : Nome completo do cliente associado à sugestão.
    /// - **TelefoneCliente** : Número de telefone de contato do cliente.
    /// - **EmailCliente** : Endereço de e-mail principal de contato do cliente.
    /// - **DiaPreferenciaCliente** : Dia de preferência do cliente para atendimento.
    /// - **TurnoPreferenciaCliente** : Turno de preferência do cliente para atendimento (ex: manhã, tarde ou noite).
    /// - **HorarioPreferenciaCliente** : Horário de preferência do cliente para atendimento.
    /// - **Especialidade** : Especialidade desejada para o atendimento (ex: Ortodontia).
    /// - **CEPPreferenciaCliente** : CEP de preferência do cliente para o atendimento.
    /// - **EstadoPreferenciaCliente** : Estado de preferência do cliente para o atendimento.
    /// - **CidadePreferenciaCliente** : Cidade de preferência do cliente para o atendimento.
    /// - **BairroPreferenciaCliente** : Bairro de preferência do cliente para o atendimento.
    /// - **RuaPreferenciaCliente** : Rua de preferência do cliente para o atendimento.
    /// - **StatusSugestaoClinica** : Status da sugestão na clínica (ex: Aprovada, Aceita).
    /// - **StatusSugestaoCliente** : Status da sugestão para o cliente (ex: Confirmada, Aceita).
    /// - **DataAlteracao** : Data da última alteração realizada na sugestão.
    /// - **DataConsulta** : Data base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// - **HorarioConsulta** : Hora base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    ///
    /// Exemplo de corpo de resposta (body) com a lista de Sugestões:
    /// ```json
    /// [
    ///   {
    ///     "id": "67d0af03c335e74f3472265e",
    ///     "idClinica": "63d234ab65f5b32d7a7c0011",
    ///     "nomeClinica": "Clínica Odonto Bem",
    ///     "telefoneClinica": "1123456789",
    ///     "emailClinica": "contato@odontobem.com",
    ///     "idUsuario": "987654321",
    ///     "nomeCliente": "Carlos Oliveira",
    ///     "telefoneCliente": "11987654321",
    ///     "emailCliente": "carlos.oliveira@email.com",
    ///     "diaPreferenciaCliente": "Sexta-feira",
    ///     "turnoPreferenciaCliente": "Noite",
    ///     "horarioPreferenciaCliente": "19:30",
    ///     "especilidade": "Ortodontia",
    ///     "cepClinica": "01452000",
    ///     "estadoClinica": "SP",
    ///     "cidadeClinica": "São Paulo",
    ///     "bairroClinica": "Pinheiros",
    ///     "ruaClinica": "Rua dos Três Irmãos",
    ///     "statusSugestaoClinica": "Aprovada",
    ///     "statusSugestaoCliente": "Confirmada",
    ///     "dataAlteracao": "2025-03-11T22:00:00Z",
    ///     "dataConsulta": "2025-03-12",
    ///     "horarioConsulta": "19:30:00"
    ///   },
    ///   {
    ///     "id": "67d0afc5ac13f169dbf8af5b",
    ///     "idClinica": "67ce4b3d61760e36f862dd59",
    ///     "nomeClinica": "Delfos",
    ///     "telefoneClinica": "1123456789",
    ///     "emailClinica": "contato@odontobem.com",
    ///     "idUsuario": "67cc95b32811515d372209ce",
    ///     "nomeCliente": "Carlos Oliveira",
    ///     "telefoneCliente": "11987654321",
    ///     "emailCliente": "carlos.oliveira@email.com",
    ///     "diaPreferenciaCliente": "Sexta-feira",
    ///     "turnoPreferenciaCliente": "Noite",
    ///     "horarioPreferenciaCliente": "19:30",
    ///     "especilidade": "Ortodontia",
    ///     "cepClinica": "01452000",
    ///     "estadoClinica": "SP",
    ///     "cidadeClinica": "São Paulo",
    ///     "bairroClinica": "Pinheiros",
    ///     "ruaClinica": "Rua dos Três Irmãos",
    ///     "statusSugestaoClinica": "Aceita",
    ///     "statusSugestaoCliente": "Aceita",
    ///     "dataAlteracao": "2025-03-11T22:00:00Z",
    ///     "dataConsulta": "2025-03-12",
    ///     "horarioConsulta": "19:30"
    ///   }
    /// ]
    /// ```
    /// </remarks>
    /// <response code="200">Sugestões recuperadas com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarTudo")]
    [Produces("application/json")]
    public async Task<IActionResult> ConsultarTudo()
    {
        var sugestoes = await _sugestaoService.ConsultarTodos();
        return Ok(sugestoes);
    }

    /// <summary>
    ///     Consultar um único registro de Sugestão
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar um único Sugestão no banco, sendo consultado pelo ID do banco de dados
    /// 
    /// Use este endpoint quando precisar consultar somente um registro com todos os campos específicos.
    /// 
    /// ### Campos que devem ser utilizados para consultar um Sugestão:
    /// 
    /// - **id**: ID do registro no banco de dados.
    /// 
    ///  ### Exemplo de body para requisição:
    ///  
    /// ```json
    ///     { "id": "67d0afc5ac13f169dbf8af5b" }
    /// ```
    /// 
    /// ### Exemplo de body que receberemos como resposta:
    /// 
    /// ```json
    ///   {
    ///     "id": "67d0afc5ac13f169dbf8af5b",
    ///     "idClinica": "67ce4b3d61760e36f862dd59",
    ///     "nomeClinica": "Delfos",
    ///     "telefoneClinica": "1123456789",
    ///     "emailClinica": "contato@odontobem.com",
    ///     "idUsuario": "67cc95b32811515d372209ce",
    ///     "nomeCliente": "Carlos Oliveira",
    ///     "telefoneCliente": "11987654321",
    ///     "emailCliente": "carlos.oliveira@email.com",
    ///     "diaPreferenciaCliente": "Sexta-feira",
    ///     "turnoPreferenciaCliente": "Noite",
    ///     "horarioPreferenciaCliente": "19:30",
    ///     "especilidade": "Ortodontia",
    ///     "cepClinica": "01452000",
    ///     "estadoClinica": "SP",
    ///     "cidadeClinica": "São Paulo",
    ///     "bairroClinica": "Pinheiros",
    ///     "ruaClinica": "Rua dos Três Irmãos",
    ///     "statusSugestaoClinica": "Aceita",
    ///     "statusSugestaoCliente": "Aceita",
    ///     "dataAlteracao": "2025-03-11T22:00:00Z",
    ///     "dataConsulta": "2025-03-12",
    ///     "horarioConsulta": "19:30"
    ///   }
    /// ```
    /// 
    /// ### Campos disponíveis para consultar as Sugestões:
    /// - **id** : Id do registro da sugestão, gerado automaticamente.
    /// - **IdClinica** : Identificador único da clínica associada à sugestão.
    /// - **NomeClinica** : Nome da clínica associada à sugestão.
    /// - **TelefoneClinica** : Número de telefone de contato da clínica.
    /// - **EmailClinica** : Endereço de e-mail principal de contato da clínica.
    /// - **IdUsuario** : Identificador único do usuário associado à sugestão.
    /// - **NomeCliente** : Nome completo do cliente associado à sugestão.
    /// - **TelefoneCliente** : Número de telefone de contato do cliente.
    /// - **EmailCliente** : Endereço de e-mail principal de contato do cliente.
    /// - **DiaPreferenciaCliente** : Dia de preferência do cliente para atendimento.
    /// - **TurnoPreferenciaCliente** : Turno de preferência do cliente para atendimento (ex: manhã, tarde ou noite).
    /// - **HorarioPreferenciaCliente** : Horário de preferência do cliente para atendimento.
    /// - **Especialidade** : Especialidade desejada para o atendimento (ex: Ortodontia).
    /// - **CEPPreferenciaCliente** : CEP de preferência do cliente para o atendimento.
    /// - **EstadoPreferenciaCliente** : Estado de preferência do cliente para o atendimento.
    /// - **CidadePreferenciaCliente** : Cidade de preferência do cliente para o atendimento.
    /// - **BairroPreferenciaCliente** : Bairro de preferência do cliente para o atendimento.
    /// - **RuaPreferenciaCliente** : Rua de preferência do cliente para o atendimento.
    /// - **StatusSugestaoClinica** : Status da sugestão na clínica (ex: Aprovada, Aceita).
    /// - **StatusSugestaoCliente** : Status da sugestão para o cliente (ex: Confirmada, Aceita).
    /// - **DataAlteracao** : Data da última alteração realizada na sugestão.
    /// - **DataConsulta** : Data base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// - **HorarioConsulta** : Hora base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// 
    /// </remarks>
    /// 
    /// <response code="200">Sugestão consultada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarSugestaoId/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarSugestaoId(string id)
    {
        var sugestao = await _sugestaoService.ConsultarId(id);

        if (sugestao == null)
        {
            return NotFound(new { message = "Sugestão não encontrado." });
        }

        return Ok(sugestao);
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

        var sugestao = await _sugestaoService.ConsultarId(userIdString);
        if (sugestao == null)
        {
            return NotFound();
        }

        return View(sugestao);
    }

    [HttpPost("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar(SugestaoConsultaCliente sugestao)
    {
        if (!ModelState.IsValid)
        {
            return View(sugestao);
        }

        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var sugestaoExistente = await _sugestaoService.ConsultarId(userIdString);

        if (sugestaoExistente == null)
        {
            return NotFound();
        }

        sugestaoExistente.IdClinica = sugestao.IdClinica    ;
        sugestaoExistente.NomeClinica = sugestao.NomeClinica;
        sugestaoExistente.CNPJClinica = sugestao.CNPJClinica;
        sugestaoExistente.TelefoneClinica = sugestao.TelefoneClinica;
        sugestaoExistente.EmailClinica = sugestao.EmailClinica;
        sugestaoExistente.IdUsuario = sugestao.IdUsuario;
        sugestaoExistente.NomeCliente = sugestao.NomeCliente;
        sugestaoExistente.CPFCliente = sugestao.CPFCliente;
        sugestaoExistente.TelefoneCliente = sugestao.TelefoneCliente;
        sugestaoExistente.EmailCliente = sugestao.EmailCliente;
        sugestaoExistente.DiaPreferenciaCliente = sugestao.DiaPreferenciaCliente;
        sugestaoExistente.TurnoPreferenciaCliente = sugestao.TurnoPreferenciaCliente;
        sugestaoExistente.HorarioPreferenciaCliente = sugestao.HorarioPreferenciaCliente;
        sugestaoExistente.Especialidade = sugestao.Especialidade;
        sugestaoExistente.NomeMedico = sugestao.NomeMedico;
        sugestaoExistente.CEPClinica = sugestao.CEPClinica;
        sugestaoExistente.EstadoClinica = sugestao.EstadoClinica;
        sugestaoExistente.CidadeClinica = sugestao.CidadeClinica;
        sugestaoExistente.BairroClinica = sugestao.BairroClinica;
        sugestaoExistente.RuaClinica = sugestao.RuaClinica;
        sugestaoExistente.StatusSugestaoCliente = sugestao.StatusSugestaoCliente;
        sugestaoExistente.DataAlteracao = sugestao.DataAlteracao;
        sugestaoExistente.DataConsulta = sugestao.DataConsulta;
        sugestaoExistente.HoraConsulta = sugestao.HoraConsulta;

        await _sugestaoService.Atualizar(sugestaoExistente);

        TempData["SuccessMessage"] = "Sugestão atualizado com sucesso!";
        //return RedirectToAction("MensagemAtualizacao");
        return View(sugestaoExistente);
    }

    [HttpGet("MensagemAtualizacao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemAtualizacao()
    {
        return View();
    }

    /// <summary>
    ///     Atualiza os dados completos existentes do Sugestão, com base no ID do banco de dados
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Atualizar todas as informações do Sugestão no banco
    /// 
    /// Use este endpoint se o objetivo for atualizar todos os campos para o Sugestão no cadastro. Se for parcial, utilize outro endpoint.
    /// 
    /// ### Campos disponíveis para consultar as Sugestões:
    /// - **id** : Id do registro da sugestão, gerado automaticamente.
    /// - **IdClinica** : Identificador único da clínica associada à sugestão.
    /// - **NomeClinica** : Nome da clínica associada à sugestão.
    /// - **TelefoneClinica** : Número de telefone de contato da clínica.
    /// - **EmailClinica** : Endereço de e-mail principal de contato da clínica.
    /// - **IdUsuario** : Identificador único do usuário associado à sugestão.
    /// - **NomeCliente** : Nome completo do cliente associado à sugestão.
    /// - **TelefoneCliente** : Número de telefone de contato do cliente.
    /// - **EmailCliente** : Endereço de e-mail principal de contato do cliente.
    /// - **DiaPreferenciaCliente** : Dia de preferência do cliente para atendimento.
    /// - **TurnoPreferenciaCliente** : Turno de preferência do cliente para atendimento (ex: manhã, tarde ou noite).
    /// - **HorarioPreferenciaCliente** : Horário de preferência do cliente para atendimento.
    /// - **Especialidade** : Especialidade desejada para o atendimento (ex: Ortodontia).
    /// - **CEPPreferenciaCliente** : CEP de preferência do cliente para o atendimento.
    /// - **EstadoPreferenciaCliente** : Estado de preferência do cliente para o atendimento.
    /// - **CidadePreferenciaCliente** : Cidade de preferência do cliente para o atendimento.
    /// - **BairroPreferenciaCliente** : Bairro de preferência do cliente para o atendimento.
    /// - **RuaPreferenciaCliente** : Rua de preferência do cliente para o atendimento.
    /// - **StatusSugestaoClinica** : Status da sugestão na clínica (ex: Aprovada, Aceita).
    /// - **StatusSugestaoCliente** : Status da sugestão para o cliente (ex: Confirmada, Aceita).
    /// - **DataAlteracao** : Data da última alteração realizada na sugestão.
    /// - **DataConsulta** : Data base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// - **HorarioConsulta** : Hora base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// 
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    ///   {
    ///     "id": "67d0afc5ac13f169dbf8af5b",
    ///     "idClinica": "67ce4b3d61760e36f862dd59",
    ///     "nomeClinica": "Delfos",
    ///     "telefoneClinica": "1123456789",
    ///     "emailClinica": "contato@odontobem.com",
    ///     "idUsuario": "67cc95b32811515d372209ce",
    ///     "nomeCliente": "Carlos Oliveira",
    ///     "telefoneCliente": "11987654321",
    ///     "emailCliente": "carlos.oliveira@email.com",
    ///     "diaPreferenciaCliente": "Sexta-feira",
    ///     "turnoPreferenciaCliente": "Noite",
    ///     "horarioPreferenciaCliente": "19:30",
    ///     "especilidade": "Ortodontia",
    ///     "cepClinica": "01452000",
    ///     "estadoClinica": "SP",
    ///     "cidadeClinica": "São Paulo",
    ///     "bairroClinica": "Pinheiros",
    ///     "ruaClinica": "Rua dos Três Irmãos",
    ///     "statusSugestaoClinica": "Aceita",
    ///     "statusSugestaoCliente": "Aceita",
    ///     "dataAlteracao": "2025-03-11T22:00:00Z",
    ///     "dataConsulta": "2025-03-12",
    ///     "horarioConsulta": "19:30"
    ///   }
    /// ``` 
    /// 
    /// ### Se preencher alguns campos e outros não, estes campos serão preenchidos com dados inválidos e default criado pelo sistema.
    /// </remarks>
    /// 
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do Sugestão no banco de dados.</param>
    /// <param name="sugestao">Dados do Sugestão a serem atualizados.</param>
    /// <response code="200">Sugestão atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="401">Sugestão não autorizado</response>
    /// <response code="404">Sugestão não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut("AtualizarSugestao/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> AtualizarSugestao(string id, [FromBody] SugestaoConsultaCliente sugestao)
    {
        if (string.IsNullOrEmpty(id) || sugestao == null || id != sugestao.Id)
        {
            return BadRequest("Id da Sugestão não corresponde ao fornecido.");
        }

        var sugestaoExistente = await _sugestaoService.ConsultarId(id);

        if (sugestaoExistente == null)
        {
            return NotFound();
        }

        sugestaoExistente.IdClinica = sugestao.IdClinica    ;
        sugestaoExistente.NomeClinica = sugestao.NomeClinica;
        sugestaoExistente.CNPJClinica = sugestao.CNPJClinica;
        sugestaoExistente.TelefoneClinica = sugestao.TelefoneClinica;
        sugestaoExistente.EmailClinica = sugestao.EmailClinica;
        sugestaoExistente.IdUsuario = sugestao.IdUsuario;
        sugestaoExistente.NomeCliente = sugestao.NomeCliente;
        sugestaoExistente.CPFCliente = sugestao.CPFCliente;
        sugestaoExistente.TelefoneCliente = sugestao.TelefoneCliente;
        sugestaoExistente.EmailCliente = sugestao.EmailCliente;
        sugestaoExistente.DiaPreferenciaCliente = sugestao.DiaPreferenciaCliente;
        sugestaoExistente.TurnoPreferenciaCliente = sugestao.TurnoPreferenciaCliente;
        sugestaoExistente.HorarioPreferenciaCliente = sugestao.HorarioPreferenciaCliente;
        sugestaoExistente.Especialidade = sugestao.Especialidade;
        sugestaoExistente.NomeMedico = sugestao.NomeMedico;
        sugestaoExistente.CEPClinica = sugestao.CEPClinica;
        sugestaoExistente.EstadoClinica = sugestao.EstadoClinica;
        sugestaoExistente.CidadeClinica = sugestao.CidadeClinica;
        sugestaoExistente.BairroClinica = sugestao.BairroClinica;
        sugestaoExistente.RuaClinica = sugestao.RuaClinica;
        //sugestaoExistente.StatusSugestaoClinica = sugestao.StatusSugestaoClinica;
        sugestaoExistente.StatusSugestaoCliente = sugestao.StatusSugestaoCliente;
        sugestaoExistente.DataAlteracao = sugestao.DataAlteracao;
        sugestaoExistente.DataConsulta = sugestao.DataConsulta;
        sugestaoExistente.HoraConsulta = sugestao.HoraConsulta;

        await _sugestaoService.Atualizar(sugestaoExistente);

        return Ok(sugestaoExistente); 
    }


    [HttpGet("ConfirmarExcluir/{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmarExcluir(string id)
    {
        var sugestao = await _sugestaoService.ConsultarId(id);
        
        if (sugestao == null)
        {
            return NotFound();
        }

        return View(sugestao);
    }

    /// Rota de API para atualizar parcialmente um Sugestão
    /// <summary>
    ///     Atualiza parcialmente os dados de uma Sugestão existente
    /// </summary>
    /// 
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do Sugestão a ser atualizada</param>
    /// 
    /// <remarks>
    /// 
    /// ## Atualização Parcial de um Sugestão
    /// 
    /// Use este endpoint quando precisar atualizar apenas alguns campos específicos de um Sugestão,
    /// sem a necessidade de enviar todos os dados.
    /// 
    /// ### Campos que podem ser atualizados:
    /// - **NomeClinica** : Nome da clínica associada à sugestão.
    /// - **TelefoneClinica** : Número de telefone de contato da clínica.
    /// - **EmailClinica** : Endereço de e-mail principal de contato da clínica.
    /// - **NomeCliente** : Nome completo do cliente associado à sugestão.
    /// - **TelefoneCliente** : Número de telefone de contato do cliente.
    /// - **EmailCliente** : Endereço de e-mail principal de contato do cliente.
    /// - **DiaPreferenciaCliente** : Dia de preferência do cliente para atendimento.
    /// - **TurnoPreferenciaCliente** : Turno de preferência do cliente para atendimento (ex: manhã, tarde ou noite).
    /// - **HorarioPreferenciaCliente** : Horário de preferência do cliente para atendimento.
    /// - **Especialidade** : Especialidade desejada para o atendimento (ex: Ortodontia).
    /// - **CEPPreferenciaCliente** : CEP de preferência do cliente para o atendimento.
    /// - **EstadoPreferenciaCliente** : Estado de preferência do cliente para o atendimento.
    /// - **CidadePreferenciaCliente** : Cidade de preferência do cliente para o atendimento.
    /// - **BairroPreferenciaCliente** : Bairro de preferência do cliente para o atendimento.
    /// - **RuaPreferenciaCliente** : Rua de preferência do cliente para o atendimento.
    /// - **StatusSugestaoClinica** : Status da sugestão na clínica (ex: Aprovada, Aceita).
    /// - **StatusSugestaoCliente** : Status da sugestão para o cliente (ex: Confirmada, Aceita).
    /// - **DataAlteracao** : Data da última alteração realizada na sugestão.
    /// - **DataConsulta** : Data base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// - **HorarioConsulta** : Hora base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// 
    /// ### Campos que não podem ser atualizados:
    /// - **id** : Id do registro da sugestão, gerado automaticamente.
    /// - **IdClinica** : Identificador único da clínica associada à sugestão.
    /// - **IdUsuario** : Identificador único do usuário associado à sugestão.
    /// 
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    /// {
    ///     "id": "67cc95b32811515d372209ce",
    ///     "email": "novo.email@clinica.com.br",
    ///     "telefone": "11988887777"
    /// }
    /// ```
    /// 
    /// Apenas os campos informados no corpo da requisição serão atualizados.
    /// 
    /// ### Exemplo de resposta:
    /// 
    /// ```json
    ///   {
    ///     "id": "67d0afc5ac13f169dbf8af5b",
    ///     "idClinica": "67ce4b3d61760e36f862dd59",
    ///     "nomeClinica": "Delfos",
    ///     "telefoneClinica": "1123456789",
    ///     "emailClinica": "contato@odontobem.com",
    ///     "idUsuario": "67cc95b32811515d372209ce",
    ///     "nomeCliente": "Carlos Oliveira",
    ///     "telefoneCliente": "11987654321",
    ///     "emailCliente": "carlos.oliveira@email.com",
    ///     "diaPreferenciaCliente": "Sexta-feira",
    ///     "turnoPreferenciaCliente": "Noite",
    ///     "horarioPreferenciaCliente": "19:30",
    ///     "especilidade": "Ortodontia",
    ///     "cepClinica": "01452000",
    ///     "estadoClinica": "SP",
    ///     "cidadeClinica": "São Paulo",
    ///     "bairroClinica": "Pinheiros",
    ///     "ruaClinica": "Rua dos Três Irmãos",
    ///     "statusSugestaoClinica": "Aceita",
    ///     "statusSugestaoCliente": "Aceita",
    ///     "dataAlteracao": "2025-03-11T22:00:00Z",
    ///     "dataConsulta": "2025-03-12",
    ///     "horarioConsulta": "19:30"
    ///   }
    /// ```
    /// </remarks>
    /// 
    /// <param name="camposParaAtualizar"></param>
    /// 
    /// <response code="200">Sugestão atualizada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Sugestão não encontrada</response>
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

        var sugestaoAtualizado = await _sugestaoService.AtualizarParcial(id, camposParaAtualizar);

        if (sugestaoAtualizado == null)
        {
            return NotFound("Sugestão não encontrada.");
        }

        return Ok(sugestaoAtualizado);
    }


    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Excluir(string id)
    {
        var sugestao = await _sugestaoService.ConsultarId(id);
        
        if (sugestao != null)
        {
            await _sugestaoService.Excluir(id);
            TempData["SuccessMessage"] = "Sugestão excluído com sucesso.";
            //return RedirectToAction("MensagemExclusao", "Sugestao"); 
            return View(sugestao);
        }

        TempData["ErrorMessage"] = "Sugestão não encontrado.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("MensagemExclusao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemExclusao()
    {
        return View();
    }

    /// <summary>
    ///     Excluir os Sugestão do banco de dados.
    /// </summary>
    ///
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do Sugestão a ser excluído</param>
    /// 
    /// <remarks>
    /// 
    /// ## Excluir um Sugestão do banco de dados e dos cadastros.
    /// 
    /// ### Exemplo da requisição para excluir um Sugestão:
    /// 
    /// ```json
    ///     {
    ///         "id": "67cc95b32811515d372209ce",
    ///     }
    /// ```
    /// 
    /// ### Exemplo da resposta para excluir um Sugestão:
    /// 
    /// ```json
    ///     {
    ///         "message": "Sugestão excluído com sucesso."
    ///     }
    /// ``` 
    /// 
    /// Uma vez excluida da base, não tem reversão desta ação.
    /// </remarks>
    /// 
    /// <response code="200">Sugestão criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete("ExcluirSugestao/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> ExcluirSugestao(string id)
    {
        var sugestao = await _sugestaoService.ConsultarId(id);
        
        if (sugestao == null)
        {
            return NotFound();
        }

        await _sugestaoService.Excluir(id);

        return Ok(new { message = "Sugestão excluído com sucesso." });  
    }
}