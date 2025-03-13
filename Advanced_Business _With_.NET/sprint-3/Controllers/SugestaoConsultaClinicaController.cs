using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("SugestaoConsultaClinica")] 
public class SugestaoConsultaClinicaController : Controller
{
    private readonly ISugestaoConsultaClinicaService _sugestaoService;

    public SugestaoConsultaClinicaController(ISugestaoConsultaClinicaService sugestaoService)
    {
        _sugestaoService = sugestaoService;
    }

    // usar essaa tag para permitir que todos possam fazer cadastrado, mas quem não estiver logado, não vai conseguir acessar nada.
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
    public async Task<IActionResult> Criar(SugestaoConsultaClinica sugestao)
    {
        if (ModelState.IsValid)
        {
            await _sugestaoService.Criar(sugestao);
            TempData["SuccessMessage"] = "Sugestão cadastrada com sucesso!";
            return RedirectToAction("Mensagem");
        }
        return View(sugestao);
    }

    [HttpGet("Mensagem")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Mensagem()
    {
        return View();
    }

    // Rota da API para criar um Sugestão
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
    /// - **id** : Id do registro da sugestão, gerado automaticamente.
    /// - **IdUsuario** : Identificador único do usuário associado à sugestão.
    /// - **NomeCliente** : Nome do cliente associado à sugestão.
    /// - **TelefoneCliente** : Número de telefone de contato do cliente.
    /// - **EmailCliente** : Endereço de e-mail principal de contato do cliente.
    /// - **DiaPreferenciaCliente** : Dia de preferência para atendimento.
    /// - **TurnoPreferenciaCliente** : Turno de preferência para atendimento (ex: manhã, tarde ou noite).
    /// - **HorarioPreferenciaCliente** : Horário de preferência para atendimento.
    /// - **Especialidade** : Especialidade do serviço ou atendimento preferido.
    /// - **CEPPreferenciaCliente** : CEP de preferência do cliente para o atendimento.
    /// - **EstadoPreferenciaCliente** : Estado de preferência do cliente para o atendimento.
    /// - **CidadePreferenciaCliente** : Cidade de preferência do cliente para o atendimento.
    /// - **BairroPreferenciaCliente** : Bairro de preferência do cliente para o atendimento.
    /// - **RuaPreferenciaCliente** : Rua de preferência do cliente para o atendimento.
    /// - **StatusSugestaoClinica** : Status da sugestão na clínica.
    /// - **StatusSugestaoCliente** : Status da sugestão para o cliente. Na primeira fase que é avaliação da clínica, este campo nunca será preenchido.
    /// - **DataAlteracao** : Data da última alteração realizada na sugestão.
    /// - **DataConsulta** : Data base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// - **HorarioConsulta** : Hora base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// 
    /// ### Campos que não devem ser utilizados para criar uma nova sugestão:
    /// - **id** : Id do dia que será gerado automaticamente
    /// - **dataAlteracao** : Data da última atualização dos dados 
    /// 
    /// 
    /// ### Exemplo de body para requisição:
    /// ```json
    ///     { 
    ///         "IdUsuario": "123456789",
    ///         "NomeCliente": "Cláudio",
    ///         "TelefoneCliente": "11958757740",
    ///         "EmailCliente": "claudio_cssp@hotmail.com",
    ///         "DiaPreferenciaCliente": "Segunda-feira",
    ///         "TurnoPreferenciaCliente": "Manhã",
    ///         "HorarioPreferenciaCliente": "08:00",
    ///         "Especilidade": "Odontologia",
    ///         "CEPPreferenciaCliente": "01001000",
    ///         "EstadoPreferenciaCliente": "SP",
    ///         "CidadePreferenciaCliente": "São Paulo",
    ///         "BairroPreferenciaCliente": "Centro",
    ///         "RuaPreferenciaCliente": "Rua da Saúde",
    ///         "StatusSugestaoClinica": "Aprovada",
    ///         "StatusSugestaoCliente": "Confirmada",
    ///         "DataAlteracao": "2025-03-11T12:00:00",
    ///         "dataConsulta": "2025-03-12",
    ///         "horarioConsulta": "19:30"
    ///     }
    /// ```
    /// 
    /// ### Exemplo de resposta quando o cadastro for efetuado:
    /// 
    /// ```json
    ///     { 
    ///         "id": "67d0ac40c335e74f3472265c",
    ///         "IdUsuario": "123456789",
    ///         "NomeCliente": "Cláudio",
    ///         "TelefoneCliente": "11958757740",
    ///         "EmailCliente": "claudio_cssp@hotmail.com",
    ///         "DiaPreferenciaCliente": "Segunda-feira",
    ///         "TurnoPreferenciaCliente": "Manhã",
    ///         "HorarioPreferenciaCliente": "08:00",
    ///         "Especilidade": "Odontologia",
    ///         "CEPPreferenciaCliente": "01001000",
    ///         "EstadoPreferenciaCliente": "SP",
    ///         "CidadePreferenciaCliente": "São Paulo",
    ///         "BairroPreferenciaCliente": "Centro",
    ///         "RuaPreferenciaCliente": "Rua da Saúde",
    ///         "StatusSugestaoClinica": "Aprovada",
    ///         "StatusSugestaoCliente": "Confirmada",
    ///         "DataAlteracao": "2025-03-11T12:00:00",
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
    public async Task<IActionResult> CadastrarSugestao([FromBody] SugestaoConsultaClinica sugestao)
    {
        if (ModelState.IsValid)
        {
            await _sugestaoService.Criar(sugestao);
            return CreatedAtAction(nameof(ConsultarTudo), new { id = sugestao.Id }, sugestao); 
        }
        return BadRequest(ModelState); 
    }


    // Rota de View
    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var sugestoes = await _sugestaoService.ConsultarTodos(); 
        return View(sugestoes); 
    }

    /// Rota de API
    /// <summary>
    ///     Consultar a lista com todas as Sugestões.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar todos os registros de Sugestões do banco de dados
    /// 
    /// Use este endpoint se seu objetivo for recuperar todos os registros de Sugestões do banco de dados.
    /// 
    /// ### Campos disponíveis para consultar as Sugestões:
    /// - **id** : Id do registro da sugestão, gerado automaticamente.
    /// - **IdUsuario** : Identificador único do usuário associado à sugestão.
    /// - **NomeCliente** : Nome do cliente associado à sugestão.
    /// - **TelefoneCliente** : Número de telefone de contato do cliente.
    /// - **EmailCliente** : Endereço de e-mail principal de contato do cliente.
    /// - **DiaPreferenciaCliente** : Dia de preferência para atendimento.
    /// - **TurnoPreferenciaCliente** : Turno de preferência para atendimento (ex: manhã, tarde ou noite).
    /// - **HorarioPreferenciaCliente** : Horário de preferência para atendimento.
    /// - **Especialidade** : Especialidade do serviço ou atendimento preferido.
    /// - **CEPPreferenciaCliente** : CEP de preferência do cliente para o atendimento.
    /// - **EstadoPreferenciaCliente** : Estado de preferência do cliente para o atendimento.
    /// - **CidadePreferenciaCliente** : Cidade de preferência do cliente para o atendimento.
    /// - **BairroPreferenciaCliente** : Bairro de preferência do cliente para o atendimento.
    /// - **RuaPreferenciaCliente** : Rua de preferência do cliente para o atendimento.
    /// - **StatusSugestaoClinica** : Status da sugestão na clínica.
    /// - **StatusSugestaoCliente** : Status da sugestão para o cliente.
    /// - **DataAlteracao** : Data da última alteração realizada na sugestão.
    /// - **DataConsulta** : Data base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// - **HorarioConsulta** : Hora base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// 
    /// Exemplo de corpo de resposta (body) com a lista de Sugestões:
    /// ```json
    ///     [
    ///         {
    ///             "id": "67d0ac40c335e74f3472265c",
    ///              "idUsuario": "123456789",
    ///              "nomeCliente": "Cláudio",
    ///              "telefoneCliente": "11958757740",
    ///              "emailCliente": "claudio_cssp@hotmail.com",
    ///              "diaPreferenciaCliente": "Segunda-feira",
    ///              "turnoPreferenciaCliente": "Manhã",
    ///              "horarioPreferenciaCliente": "08:00",
    ///              "especilidade": "Odontologia",
    ///              "cepPreferenciaCliente": "01001000",
    ///              "estadoPreferenciaCliente": "SP",
    ///              "cidadePreferenciaCliente": "São Paulo",
    ///              "bairroPreferenciaCliente": "Centro",
    ///              "ruaPreferenciaCliente": "Rua da Saúde",
    ///              "statusSugestaoClinica": "Aprovada",
    ///              "statusSugestaoCliente": "Confirmada",
    ///              "dataAlteracao": "2025-03-11T19:00:00Z",
    ///              "dataConsulta": "2025-03-12",
    ///              "horarioConsulta": "19:30"
    ///             },
    ///         {
    ///              "id": "67d0ad99c335e74f3472265d",
    ///              "idUsuario": "67ce287f98294f7ba3edb1e6",
    ///              "nomeCliente": "Ana Souza",
    ///              "telefoneCliente": "11987654321",
    ///              "emailCliente": "ana.souza@email.com",
    ///              "diaPreferenciaCliente": "Quarta-feira",
    ///              "turnoPreferenciaCliente": "Tarde",
    ///              "horarioPreferenciaCliente": "14:30",
    ///              "especilidade": "Odontopediatria",
    ///              "cepPreferenciaCliente": "03012000",
    ///              "estadoPreferenciaCliente": "SP",
    ///              "cidadePreferenciaCliente": "São Paulo",
    ///              "bairroPreferenciaCliente": "Liberdade",
    ///              "ruaPreferenciaCliente": "Rua do Oriente",
    ///              "statusSugestaoClinica": "Pendente",
    ///              "statusSugestaoCliente": "Sem resposta",
    ///              "dataAlteracao": "2025-03-11T21:00:00Z",
    ///              "dataConsulta": "2025-03-12",
    ///              "horarioConsulta": "19:30"
    ///            }
    ///        ]
    /// ```
    /// </remarks>
    /// 
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
    ///     { "id": "67cc95b32811515d372209ce" }
    /// ```
    /// 
    /// ### Exemplo de body que receberemos como resposta:
    /// 
    /// ```json
    ///     {   
    ///         "id": "67d0ac40c335e74f3472265c",
    ///         "IdUsuario": "123456789",
    ///         "NomeCliente": "Cláudio",
    ///         "TelefoneCliente": "11958757740",
    ///         "EmailCliente": "claudio_cssp@hotmail.com",
    ///         "DiaPreferenciaCliente": "Segunda-feira",
    ///         "TurnoPreferenciaCliente": "Manhã",
    ///         "HorarioPreferenciaCliente": "08:00",
    ///         "Especilidade": "Odontologia",
    ///         "CEPPreferenciaCliente": "01001000",
    ///         "EstadoPreferenciaCliente": "SP",
    ///         "CidadePreferenciaCliente": "São Paulo",
    ///         "BairroPreferenciaCliente": "Centro",
    ///         "RuaPreferenciaCliente": "Rua da Saúde",
    ///         "StatusSugestaoClinica": "Aprovada",
    ///         "StatusSugestaoCliente": "Confirmada",
    ///         "DataAlteracao": "2025-03-11T12:00:00",
    ///         "dataConsulta": "2025-03-12",
    ///         "horarioConsulta": "19:30"
    ///     }
    /// ```
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

    // View para atualizar um Sugestão
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

        var sugestao = await _sugestaoService.ConsultarId(userIdString);
        if (sugestao == null)
        {
            return NotFound();
        }

        return View(sugestao);
    }

    [HttpPost("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar(SugestaoConsultaClinica sugestao)
    {
        if (!ModelState.IsValid)
        {
            return View(sugestao);
        }

        //var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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

        sugestaoExistente.IdUsuario = sugestao.IdUsuario;
        sugestaoExistente.NomeCliente = sugestao.NomeCliente;
        sugestaoExistente.TelefoneCliente = sugestao.TelefoneCliente;
        sugestaoExistente.EmailCliente = sugestao.EmailCliente;
        sugestaoExistente.DiaPreferenciaCliente = sugestao.DiaPreferenciaCliente;
        sugestaoExistente.TurnoPreferenciaCliente = sugestao.TurnoPreferenciaCliente;
        sugestaoExistente.HorarioPreferenciaCliente = sugestao.HorarioPreferenciaCliente;
        sugestaoExistente.Especialidade = sugestao.Especialidade;
        sugestaoExistente.CEPPreferenciaCliente = sugestao.CEPPreferenciaCliente;
        sugestaoExistente.EstadoPreferenciaCliente = sugestao.EstadoPreferenciaCliente;
        sugestaoExistente.CidadePreferenciaCliente = sugestao.CidadePreferenciaCliente;
        sugestaoExistente.BairroPreferenciaCliente = sugestao.BairroPreferenciaCliente;
        sugestaoExistente.RuaPreferenciaCliente = sugestao.RuaPreferenciaCliente;
        sugestaoExistente.StatusSugestaoClinica = sugestao.StatusSugestaoClinica;
        //sugestaoExistente.StatusSugestaoCliente = sugestao.StatusSugestaoCliente;
        sugestaoExistente.DataAlteracao = sugestao.DataAlteracao;
        sugestaoExistente.DataConsulta = sugestao.DataConsulta;
        sugestaoExistente.HoraConsulta = sugestao.HoraConsulta;

        await _sugestaoService.Atualizar(sugestaoExistente);

        TempData["SuccessMessage"] = "Sugestão atualizado com sucesso!";
        return RedirectToAction("MensagemAtualizacao");
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
    /// ### Campos disponíveis para atualização:
    /// - **id**: ID do Sugestão no banco de dados.
    /// - **IdUsuario**: ID do usuário associado ao Sugestão (opcional).
    /// - **NomeCliente**: Nome completo do cliente.
    /// - **TelefoneCliente**: Telefone de contato do cliente.
    /// - **EmailCliente**: Email principal de contato do cliente.
    /// - **DiaPreferenciaCliente**: Dia preferido para atendimento (exemplo: "Segunda-feira").
    /// - **TurnoPreferenciaCliente**: Turno preferido para atendimento (exemplo: "Manhã", "Tarde", "Noite").
    /// - **HorarioPreferenciaCliente**: Horário preferido para atendimento.
    /// - **Especialidade**: Especialidade do serviço desejado (exemplo: "Odontologia").
    /// - **CEPPreferenciaCliente**: CEP do local preferido para atendimento.
    /// - **EstadoPreferenciaCliente**: Estado do local preferido para atendimento.
    /// - **CidadePreferenciaCliente**: Cidade do local preferido para atendimento.
    /// - **BairroPreferenciaCliente**: Bairro do local preferido para atendimento.
    /// - **RuaPreferenciaCliente**: Rua do local preferido para atendimento.
    /// - **StatusSugestaoClinica**: Status da sugestão na clínica (exemplo: "Aprovada", "Rejeitada").
    /// - **StatusSugestaoCliente**: Status da sugestão do cliente (exemplo: "Confirmada", "Cancelada").
    /// - **DataAlteracao**: Data da última alteração dos dados.
    /// - **DataConsulta** : Data base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// - **HorarioConsulta** : Hora base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// 
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    ///     { 
    ///         "IdUsuario": "123456789",
    ///         "NomeCliente": "Cláudio",
    ///         "TelefoneCliente": "11958757740",
    ///         "EmailCliente": "claudio_cssp@hotmail.com",
    ///         "DiaPreferenciaCliente": "Segunda-feira",
    ///         "TurnoPreferenciaCliente": "Manhã",
    ///         "HorarioPreferenciaCliente": "08:00",
    ///         "Especilidade": "Odontologia",
    ///         "CEPPreferenciaCliente": "01001000",
    ///         "EstadoPreferenciaCliente": "SP",
    ///         "CidadePreferenciaCliente": "São Paulo",
    ///         "BairroPreferenciaCliente": "Centro",
    ///         "RuaPreferenciaCliente": "Rua da Saúde",
    ///         "StatusSugestaoClinica": "Aprovada",
    ///         "StatusSugestaoCliente": "Confirmada",
    ///         "DataAlteracao": "2025-03-11T12:00:00",
    ///         "dataConsulta": "2025-03-12",
    ///         "horarioConsulta": "19:30"
    ///     }
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
    public async Task<IActionResult> AtualizarSugestao(string id, [FromBody] SugestaoConsultaClinica sugestao)
    {
        if (string.IsNullOrEmpty(id) || sugestao == null || id != sugestao.Id)
        {
            return BadRequest("Id do Clinica não corresponde ao fornecido.");
        }

        var sugestaoExistente = await _sugestaoService.ConsultarId(id);

        if (sugestaoExistente == null)
        {
            return NotFound();
        }


        sugestaoExistente.IdUsuario = sugestao.IdUsuario;
        sugestaoExistente.NomeCliente = sugestao.NomeCliente;
        sugestaoExistente.TelefoneCliente = sugestao.TelefoneCliente;
        sugestaoExistente.EmailCliente = sugestao.EmailCliente;
        sugestaoExistente.DiaPreferenciaCliente = sugestao.DiaPreferenciaCliente;
        sugestaoExistente.TurnoPreferenciaCliente = sugestao.TurnoPreferenciaCliente;
        sugestaoExistente.HorarioPreferenciaCliente = sugestao.HorarioPreferenciaCliente;
        sugestaoExistente.Especialidade = sugestao.Especialidade;
        sugestaoExistente.CEPPreferenciaCliente = sugestao.CEPPreferenciaCliente;
        sugestaoExistente.EstadoPreferenciaCliente = sugestao.EstadoPreferenciaCliente;
        sugestaoExistente.CidadePreferenciaCliente = sugestao.CidadePreferenciaCliente;
        sugestaoExistente.BairroPreferenciaCliente = sugestao.BairroPreferenciaCliente;
        sugestaoExistente.RuaPreferenciaCliente = sugestao.RuaPreferenciaCliente;
        sugestaoExistente.StatusSugestaoClinica = sugestao.StatusSugestaoClinica;
        //sugestaoExistente.StatusSugestaoCliente = sugestao.StatusSugestaoCliente;
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
    /// - **nome**: Nome do Sugestão
    /// - **cpf**: CPF oficial do Sugestão com 11 dígitos
    /// - **telefone**: Número de telefone para contato
    /// - **email**: Endereço de email para contato
    /// - **diaPreferenciaCliente**: Dia de preferência do cliente para o agendamento
    /// - **turnoPreferenciaCliente**: Turno de preferência do cliente (Manhã, Tarde, Noite)
    /// - **horarioPreferenciaCliente**: Horário específico de preferência do cliente
    /// - **especialidade**: Especialidade de preferência para o atendimento
    /// - **cepPreferenciaCliente**: CEP de preferência do cliente
    /// - **estadoPreferenciaCliente**: Estado de preferência do cliente
    /// - **cidadePreferenciaCliente**: Cidade de preferência do cliente
    /// - **bairroPreferenciaCliente**: Bairro de preferência do cliente
    /// - **ruaPreferenciaCliente**: Rua de preferência do cliente
    /// - **statusSugestaoClinica**: Status da sugestão da clínica (exemplo: "Aprovado", "Pendente")
    /// - **statusSugestaoCliente**: Status da sugestão do cliente (exemplo: "Aceito", "Aguardando")
    /// - **dataAlteracao**: Data da última alteração
    /// - **DataConsulta** : Data base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// - **HorarioConsulta** : Hora base para a consulta, de acordo com a definição da clínica com as prefreências de, dia turno e horário do cliente.
    /// 
    /// ### Campos que não podem ser atualizados:
    /// - **Perfil**: O perfil será sempre "Comum" para clientes, não podendo ser alterado.
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
    ///     {   
    ///         "id": "67d0ac40c335e74f3472265c",
    ///         "IdUsuario": "123456789",
    ///         "NomeCliente": "Cláudio",
    ///         "TelefoneCliente": "11958757740",
    ///         "EmailCliente": "claudio_cssp@hotmail.com",
    ///         "DiaPreferenciaCliente": "Segunda-feira",
    ///         "TurnoPreferenciaCliente": "Manhã",
    ///         "HorarioPreferenciaCliente": "08:00",
    ///         "Especilidade": "Odontologia",
    ///         "CEPPreferenciaCliente": "01001000",
    ///         "EstadoPreferenciaCliente": "SP",
    ///         "CidadePreferenciaCliente": "São Paulo",
    ///         "BairroPreferenciaCliente": "Centro",
    ///         "RuaPreferenciaCliente": "Rua da Saúde",
    ///         "StatusSugestaoClinica": "Aprovada",
    ///         "StatusSugestaoCliente": "Confirmada",
    ///         "DataAlteracao": "2025-03-11T12:00:00",
    ///         "dataConsulta": "2025-03-12",
    ///         "horarioConsulta": "19:30"
    ///     }
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
            // Exclui o Sugestão do banco de dados
            await _sugestaoService.Excluir(id);

            // Desloga o Sugestão
            //await _context.SaveChangesAsync();
            await HttpContext.SignOutAsync();
            
            // Redireciona para a página de login ou para onde você preferir
            TempData["SuccessMessage"] = "Sugestão excluído com sucesso.";
            return RedirectToAction("MensagemExclusao", "Sugestao"); 
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

    // Rota de API para excluir um Sugestão
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