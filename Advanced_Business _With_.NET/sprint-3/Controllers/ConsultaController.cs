using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("Consulta")] 
public class ConsultaController : Controller
{

    private readonly IConsultaService _consultaService;

    public ConsultaController(IConsultaService consultaService)
    {
        _consultaService = consultaService;
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
    public async Task<IActionResult> Criar(Consulta consulta)
    {
        if (ModelState.IsValid)
        {
            await _consultaService.Criar(consulta);
            TempData["SuccessMessage"] = "Consulta cadastrado com sucesso!";
            return RedirectToAction("Mensagem");
        }
        return View(consulta);
    }


    /// <summary>
    ///     Cria uma nova Consulta.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Cadastrar nova Consulta no banco de dados
    /// 
    /// Use este endPoint para cadastrar uma Consulta.
    /// 
    /// ### Campos que devem ser utilizados para criar um novo Consulta:
    /// - **IdUsuario** string : Identificador do usuário
    /// - **NomeCliente** string : Nome completo do cliente
    /// - **CPFCliente** string : CPF oficial do cliente com 11 dígitos
    /// - **Data** DateOnly : Data da consulta
    /// - **Turno** string : Turno da consulta (manhã, tarde, noite)
    /// - **Horario** string : Horário da consulta
    /// - **IdClinica** string : Identificador da clínica
    /// - **NomeClinica** string : Nome da clínica
    /// - **CNPJClinica** string : CNPJ da clínica
    /// - **NomeMedico** string : Nome do médico
    /// - **Especialidade** string : Especialidade do médico
    /// - **CEPClinica** string : CEP da clínica
    /// - **EstadoClinica** string : Estado da clínica
    /// - **CidadeClinica** string : Cidade da clínica
    /// - **BairroClinica** string : Bairro da clínica
    /// - **RuaClinica** string : Rua da clínica
    /// - **StatusConsulta** string : Status da consulta (padrão = "Encerrado")
    /// - **RespostaFeedback** string : Resposta do feedback (padrão = "Sem resposta")
    /// 
    /// ### Campos que não devem ser utilizados para criar uma nova Consulta:
    /// - **Id** : Id da consulta que será gerado automaticamente
    /// 
    /// ### Exemplo de body para requisição:
    /// ```json
    ///     {
    ///         "IdUsuario": "1234567890",
    ///         "NomeCliente": "João Silva",
    ///         "CPFCliente": "12345678910",
    ///         "Data": "2025-03-12",
    ///         "Turno": "Manhã",
    ///         "Horario": "09:00",
    ///         "IdClinica": "0987654321",
    ///         "NomeClinica": "Clínica Exemplo",
    ///         "CNPJClinica": "12345678000199",
    ///         "NomeMedico": "Dr. José",
    ///         "Especialidade": "Cardiologia",
    ///         "CEPClinica": "12345678",
    ///         "EstadoClinica": "SP",
    ///         "CidadeClinica": "São Paulo",
    ///         "BairroClinica": "Centro",
    ///         "RuaClinica": "Rua Exemplo, 123",
    ///         "StatusConsulta": "Encerrado",
    ///         "RespostaFeedback": "Sem resposta"
    ///     }
    /// ```
    /// 
    /// ### Exemplo de resposta quando o cadastro for efetuado:
    /// 
    /// ```json
    ///     {   
    ///         "Id": "67cc95b32811515d37220000", // gerado pelo banco de dados
    ///         "IdUsuario": "1234567890",
    ///         "NomeCliente": "João Silva",
    ///         "CPFCliente": "12345678910",
    ///         "Data": "2025-03-12",
    ///         "Turno": "Manhã",
    ///         "Horario": "09:00",
    ///         "IdClinica": "0987654321",
    ///         "NomeClinica": "Clínica Exemplo",
    ///         "CNPJClinica": "12345678000199",
    ///         "NomeMedico": "Dr. José",
    ///         "Especialidade": "Cardiologia",
    ///         "CEPClinica": "12345678",
    ///         "EstadoClinica": "SP",
    ///         "CidadeClinica": "São Paulo",
    ///         "BairroClinica": "Centro",
    ///         "RuaClinica": "Rua Exemplo, 123",
    ///         "StatusConsulta": "Encerrado",
    ///         "RespostaFeedback": "Sem resposta"
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Requisição realizada com sucesso</response>
    /// <response code="201">Consulta criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost("CadastrarConsulta")]
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CadastrarConsulta([FromBody] Consulta consulta)
    {
        if (ModelState.IsValid)
        {
            await _consultaService.Criar(consulta);
            return CreatedAtAction(nameof(ConsultarTodasConsultas), new { id = consulta.Id }, consulta); 
        }
        return BadRequest(ModelState); 
    }

    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var consultas = await _consultaService.ConsultarTodos(); 
        return View(consultas); 
    }

    /// <summary>
    ///     Consultar a lista com todas as Consultas.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar todos os registros de Consultas do banco de dados
    /// 
    /// Use este endPoint se seu objetivo é recuperar todos os registros de Consultas do banco de dados.
    /// 
    /// ### Campos disponíveis para consultar as Consultas:
    /// - **Id** : Id da consulta gerado automaticamente
    /// - **NomeCliente** string : Nome completo do cliente
    /// - **CPFCliente** string : CPF oficial do cliente com 11 dígitos
    /// - **Data** DateOnly : Data da consulta
    /// - **Turno** string : Turno da consulta (manhã, tarde, noite)
    /// - **Horario** string : Horário da consulta
    /// - **IdClinica** string : Identificador da clínica
    /// - **NomeClinica** string : Nome da clínica
    /// - **CNPJClinica** string : CNPJ da clínica
    /// - **NomeMedico** string : Nome do médico
    /// - **Especialidade** string : Especialidade do médico
    /// - **CEPClinica** string : CEP da clínica
    /// - **EstadoClinica** string : Estado da clínica
    /// - **CidadeClinica** string : Cidade da clínica
    /// - **BairroClinica** string : Bairro da clínica
    /// - **RuaClinica** string : Rua da clínica
    /// - **StatusConsulta** string : Status da consulta
    /// - **RespostaFeedback** string : Resposta do feedback
    /// 
    /// ### Exemplo de body de resposta, sempre será uma lista com todas as Consultas no banco:
    /// ```json
    ///     [
    ///         { 
    ///             "Id": "67cc95b32811515d372209ce",
    ///             "NomeCliente": "Claudio",
    ///             "CPFCliente": "12345678910",
    ///             "Data": "2025-03-12",
    ///             "Turno": "Manhã",
    ///             "Horario": "09:00",
    ///             "IdClinica": "0987654321",
    ///             "NomeClinica": "Clínica Exemplo",
    ///             "CNPJClinica": "12345678000199",
    ///             "NomeMedico": "Dr. José",
    ///             "Especialidade": "Cardiologia",
    ///             "CEPClinica": "12345678",
    ///             "EstadoClinica": "SP",
    ///             "CidadeClinica": "São Paulo",
    ///             "BairroClinica": "Centro",
    ///             "RuaClinica": "Rua Exemplo, 123",
    ///             "StatusConsulta": "Encerrado",
    ///             "RespostaFeedback": "Sem resposta"
    ///         },
    ///         {
    ///             "Id": "67cca0540924d08d2c4b7819",
    ///             "NomeCliente": "Caio",
    ///             "CPFCliente": "12345678910",
    ///             "Data": "2025-03-12",
    ///             "Turno": "Tarde",
    ///             "Horario": "14:00",
    ///             "IdClinica": "0987654322",
    ///             "NomeClinica": "Clínica Exemplo 2",
    ///             "CNPJClinica": "12345678000198",
    ///             "NomeMedico": "Dr. Maria",
    ///             "Especialidade": "Dermatologia",
    ///             "CEPClinica": "12345679",
    ///             "EstadoClinica": "RJ",
    ///             "CidadeClinica": "Rio de Janeiro",
    ///             "BairroClinica": "Copacabana",
    ///             "RuaClinica": "Avenida Atlântica, 456",
    ///             "StatusConsulta": "Encerrado",
    ///             "RespostaFeedback": "Sem resposta"
    ///         }
    ///     ]
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Requisição realizada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarTodasConsultas")]
    [Produces("application/json")]
    public async Task<IActionResult> ConsultarTodasConsultas()
    {
        var consultas = await _consultaService.ConsultarTodos();
        return Ok(consultas);
    }

    /// <summary>
    ///     Consultar um único registro de Consulta.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar um único Consulta no banco, sendo consultado pelo ID do banco de dados.
    /// 
    /// Use este endpoint quando precisar consultar somente um registro com todos os campos específicos.
    /// 
    /// ### Campo que deve ser utilizado para consultar um Consulta:
    /// 
    /// - **Id**: ID do banco
    /// 
    /// ### Exemplo de body para requisição:
    /// 
    /// ```json
    ///     {
    ///         "Id": "67d1baa20c34f565cac59aab"
    ///     }
    /// ```
    /// 
    /// ### Exemplo de body que receberemos como resposta:
    /// 
    /// ```json
    ///    {
    ///         "Id": "67d1baa20c34f565cac59aab",
    ///         "NomeCliente": "Claudio",
    ///         "CPFCliente": "12345678910",
    ///         "Data": "2025-03-12",
    ///         "Turno": "Manhã",
    ///         "Horario": "09:00",
    ///         "IdClinica": "0987654321",
    ///         "NomeClinica": "Clínica Exemplo",
    ///         "CNPJClinica": "12345678000199",
    ///         "NomeMedico": "Dr. José",
    ///         "Especialidade": "Cardiologia",
    ///         "CEPClinica": "12345678",
    ///         "EstadoClinica": "SP",
    ///         "CidadeClinica": "São Paulo",
    ///         "BairroClinica": "Centro",
    ///         "RuaClinica": "Rua Exemplo, 123",
    ///         "StatusConsulta": "Encerrado",
    ///         "RespostaFeedback": "Sem resposta"
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Consulta consultado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarConsultaId/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarConsultaId(string id)
    {
        var consulta = await _consultaService.ConsultarId(id);

        if (consulta == null)
        {
            return NotFound(new { message = "Consulta não encontrado." });
        }

        return Ok(consulta);
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

        var consulta = await _consultaService.ConsultarId(userIdString);
        if (consulta == null)
        {
            return NotFound();
        }

        return View(consulta);
    }

    [HttpPost("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar(Consulta consulta)
    {
        if (!ModelState.IsValid)
        {
            return View(consulta);
        }

        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var consultaExistente = await _consultaService.ConsultarId(userIdString);

        if (consultaExistente == null)
        {
            return NotFound();
        }

        consultaExistente.IdUsuario = consulta.IdUsuario;
        consultaExistente.NomeCliente = consulta.NomeCliente;
        consultaExistente.CPFCliente = consulta.CPFCliente;
        consultaExistente.Data = consulta.Data;
        consultaExistente.Turno = consulta.Turno;
        consultaExistente.Horario = consulta.Horario;
        consultaExistente.IdClinica = consulta.IdClinica;
        consultaExistente.NomeClinica = consulta.NomeClinica;
        consultaExistente.CNPJClinica = consulta.CNPJClinica;
        consultaExistente.NomeMedico = consulta.NomeMedico;
        consultaExistente.Especialidade = consulta.Especialidade;
        consultaExistente.CEPClinica = consulta.CEPClinica;
        consultaExistente.EstadoClinica = consulta.EstadoClinica;
        consultaExistente.CidadeClinica = consulta.CidadeClinica;
        consultaExistente.BairroClinica = consulta.BairroClinica;
        consultaExistente.RuaClinica = consulta.RuaClinica;
        consultaExistente.StatusConsulta = consulta.StatusConsulta;
        consultaExistente.RespostaFeedback = consulta.RespostaFeedback;

        await _consultaService.Atualizar(consultaExistente);

        TempData["SuccessMessage"] = "Consulta atualizado com sucesso!";
        return RedirectToAction("MensagemAtualizacao");
    }

    [HttpGet("MensagemAtualizacao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemAtualizacao()
    {
        return View();
    }

    /// <summary>
    ///     Atualiza os dados completos existentes do Consulta, com base no ID do banco de dados.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Atualizar todas as informações do Consulta no banco
    /// 
    /// Use este endpoint se o objetivo for atualizar todos os campos do Consulta no cadastro. Se for parcial, utilize outro endpoint.
    /// 
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    ///     {   
    ///         "Id": "67d1baa20c34f565cac59aab",
    ///         "NomeCliente": "Patricia Delfos",
    ///         "CPFCliente": "12345678910",
    ///         "Data": "2025-03-12",
    ///         "Turno": "Manhã",
    ///         "Horario": "09:00",
    ///         "IdClinica": "0987654321",
    ///         "NomeClinica": "Clínica Exemplo",
    ///         "CNPJClinica": "12345678000199",
    ///         "NomeMedico": "Dr. José",
    ///         "Especialidade": "Cardiologia",
    ///         "CEPClinica": "12345678",
    ///         "EstadoClinica": "SP",
    ///         "CidadeClinica": "São Paulo",
    ///         "BairroClinica": "Centro",
    ///         "RuaClinica": "Rua Exemplo, 123",
    ///         "StatusConsulta": "Encerrado",
    ///         "RespostaFeedback": "Sem resposta"
    ///     }
    /// ``` 
    /// 
    /// ### Se preencher alguns campos e outros não, estes campos serão preenchidos com dados inválidos e valores padrão criados pelo sistema.
    /// </remarks>
    /// 
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do Consulta no banco de dados.</param>
    /// <param name="consulta">Dados do Consulta a serem atualizados.</param>
    /// <response code="200">Consulta atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="401">Consulta não autorizado</response>
    /// <response code="404">Consulta não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut("AtualizarUsuario/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> AtualizarUsuario(string id, [FromBody] Consulta consulta)
    {
        if (string.IsNullOrEmpty(id) || consulta == null || id != consulta.Id)
        {
            return BadRequest("Id do Clinica não corresponde ao fornecido.");
        }

        var consultaExistente = await _consultaService.ConsultarId(id);

        if (consultaExistente == null)
        {
            return NotFound();
        }

        consultaExistente.IdUsuario = consulta.IdUsuario;
        consultaExistente.NomeCliente = consulta.NomeCliente;
        consultaExistente.CPFCliente = consulta.CPFCliente;
        consultaExistente.Data = consulta.Data;
        consultaExistente.Turno = consulta.Turno;
        consultaExistente.Horario = consulta.Horario;
        consultaExistente.IdClinica = consulta.IdClinica;
        consultaExistente.NomeClinica = consulta.NomeClinica;
        consultaExistente.CNPJClinica = consulta.CNPJClinica;
        consultaExistente.NomeMedico = consulta.NomeMedico;
        consultaExistente.Especialidade = consulta.Especialidade;
        consultaExistente.CEPClinica = consulta.CEPClinica;
        consultaExistente.EstadoClinica = consulta.EstadoClinica;
        consultaExistente.CidadeClinica = consulta.CidadeClinica;
        consultaExistente.BairroClinica = consulta.BairroClinica;
        consultaExistente.RuaClinica = consulta.RuaClinica;
        consultaExistente.StatusConsulta = consulta.StatusConsulta;
        consultaExistente.RespostaFeedback = consulta.RespostaFeedback;

        await _consultaService.Atualizar(consultaExistente);

        return Ok(consultaExistente); 
    }


    [HttpGet("ConfirmarExcluir/{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmarExcluir(string id)
    {
        var consulta = await _consultaService.ConsultarId(id);
        
        if (consulta == null)
        {
            return NotFound();
        }

        return View(consulta);
    }

    /// <summary>
    ///     Atualiza parcialmente os dados de uma Consulta existente.
    /// </summary>
    /// 
    /// <param name="id" type="string" example="67d1baa20c34f565cac59aab">ID do Consulta a ser atualizado</param>
    /// 
    /// <remarks>
    /// 
    /// ## Atualização Parcial de um Consulta
    /// 
    /// Use este endpoint quando precisar atualizar apenas alguns campos específicos de um Consulta,
    /// sem a necessidade de enviar todos os dados.
    /// 
    /// ### Campos que podem ser atualizados:
    /// - **NomeCliente**: Nome do cliente
    /// - **CPFCliente** string : CPF oficial do cliente com 11 dígitos
    /// - **Data** DateOnly : Data da consulta
    /// - **Turno** string : Turno da consulta (manhã, tarde, noite)
    /// - **Horario** string : Horário da consulta
    /// - **IdClinica** string : Identificador da clínica
    /// - **NomeClinica** string : Nome da clínica
    /// - **CNPJClinica** string : CNPJ da clínica
    /// - **NomeMedico** string : Nome do médico
    /// - **Especialidade** string : Especialidade do médico
    /// - **CEPClinica** string : CEP da clínica
    /// - **EstadoClinica** string : Estado da clínica
    /// - **CidadeClinica** string : Cidade da clínica
    /// - **BairroClinica** string : Bairro da clínica
    /// - **RuaClinica** string : Rua da clínica
    /// - **StatusConsulta** string : Status da consulta
    /// - **RespostaFeedback** string : Resposta do feedback
    /// 
    /// ### Campos que não podem ser atualizados:
    /// - **Id**: ID da consulta gerado automaticamente
    /// 
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    /// {
    ///     "Id": "67d1baa20c34f565cac59aab",
    ///     "Email": "novo.email@clinica.com.br"
    /// }
    /// ```
    /// 
    /// Somente os campos incluídos no corpo da requisição serão atualizados.
    /// 
    /// ### Exemplo de resposta da requisição:
    /// 
    /// ```json
    /// {
    ///     "Id": "67d1baa20c34f565cac59aab",
    ///     "NomeCliente": "Delfos",
    ///     "CPFCliente": "12345678910",
    ///     "Data": "2025-03-12",
    ///     "Turno": "Manhã",
    ///     "Horario": "09:00",
    ///     "IdClinica": "0987654321",
    ///     "NomeClinica": "Clínica Exemplo",
    ///     "CNPJClinica": "12345678000199",
    ///     "NomeMedico": "Dr. José",
    ///     "Especialidade": "Cardiologia",
    ///     "CEPClinica": "12345678",
    ///     "EstadoClinica": "SP",
    ///     "CidadeClinica": "São Paulo",
    ///     "BairroClinica": "Centro",
    ///     "RuaClinica": "Rua Exemplo, 123",
    ///     "StatusConsulta": "Encerrado",
    ///     "RespostaFeedback": "Sem resposta"
    /// }
    /// ```
    /// </remarks>
    /// 
    /// <param name="camposParaAtualizar"></param>
    /// 
    /// <response code="200">Consulta atualizada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Consulta não encontrada</response>
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

        var consultaAtualizado = await _consultaService.AtualizarParcial(id, camposParaAtualizar);

        if (consultaAtualizado == null)
        {
            return NotFound("Consulta não encontrada.");
        }

        return Ok(consultaAtualizado);
    }

    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Excluir(string id)
    {
        var consulta = await _consultaService.ConsultarId(id);
        
        if (consulta != null)
        {
            await _consultaService.Excluir(id);

            //await HttpContext.SignOutAsync();
            
            TempData["SuccessMessage"] = "Consulta excluído com sucesso.";
            //return RedirectToAction("MensagemExclusao", "Consulta"); 
        }

        TempData["ErrorMessage"] = "Consulta não encontrado.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("MensagemExclusao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemExclusao()
    {
        return View();
    }

    /// <summary>
    ///     Excluir os Consulta do banco de dados.
    /// </summary>
    ///
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do Consulta a ser excluído</param>
    /// 
    /// <remarks>
    /// 
    /// ## Excluir um Consulta do banco de dados e dos cadastros.
    /// 
    /// ### Exemplo da requisição para excluir um Consulta:
    /// 
    /// ```json
    ///     {
    ///         "id": "67cc95b32811515d372209ce",
    ///     }
    /// ```
    /// 
    /// ### Exemplo da resposta para excluir um Consulta:
    /// 
    /// ```json
    ///     {
    ///         "message": "Consulta excluído com sucesso."
    ///     }
    /// ``` 
    /// 
    /// Uma vez excluida da base, não tem reversão desta ação.
    /// </remarks>
    /// 
    /// <response code="200">Consulta criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete("ExcluirConsulta/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> ExcluirConsulta(string id)
    {
        var consulta = await _consultaService.ConsultarId(id);
        
        if (consulta == null)
        {
            return NotFound();
        }

        await _consultaService.Excluir(id);

        return Ok(new { message = "Consulta excluído com sucesso." });  
    }

}