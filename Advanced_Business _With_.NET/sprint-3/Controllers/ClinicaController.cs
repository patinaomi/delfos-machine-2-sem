using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Route("Clinica")] 
public class ClinicaController : Controller
{
    private readonly IClinicaService _clinicaService;

    public ClinicaController(IClinicaService clinicaService)
    {
        _clinicaService = clinicaService;
    }

    // usar essaa tag para permitir que todos possam fazer cadastro, mas quem não estiver logado, não vai conseguir acessar nada.
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
    public async Task<IActionResult> Criar([Bind("Id,CNPJ,Nome,Sobrenome,Telefone,Email,Senha")] Clinica clinica)
    {
        if (ModelState.IsValid)
        {
            clinica.Perfil = "Clinica";
            await _clinicaService.Criar(clinica);
            TempData["SuccessMessage"] = "Clinica cadastrado com sucesso!";
            //return RedirectToAction("Mensagem");
        }
        return View(clinica);
    }

    [HttpGet("Mensagem")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Mensagem()
    {
        return View();
    }

    /// <summary>
    ///     Cria um novo cadastro de Clinica no banco de dados.
    /// </summary>
    /// 
    /// <remarks>
    /// ## Criação de uma nova clínica
    /// 
    /// Use este endpoint quando precisar cadastrar uma nova clínica. Aqui ainda não cadastramos os médicos, somente as empresas.
    /// 
    /// ### Campos que devem ser utilizados para criar uma nova Clínica:
    /// 
    /// - **nome**: Nome da clínica
    /// - **cnpj**: CNPJ da clínica
    /// - **telefone**: Número de telefone para contato
    /// - **email**: Endereço de email para contato
    /// - **senha**: Senha de acesso (será criptografada)
    /// 
    /// ### Campos que não devem ser utilizados para criar uma nova Clínica:
    /// - **id**: ID não é necessário pois o banco vai criar um de forma automática.
    /// - **perfil**: Não será necessário enviar pois é default aqui
    /// 
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    ///     {
    ///         "nome": "João",
    ///         "CNPJ": "1234567891011",
    ///         "telefone": "11958757000"
    ///         "email": "joao@exemplo.com",
    ///         "senha": "senha123",
    ///         "perfil": "Clinica"
    ///     }
    /// ```
    /// 
    /// Somente os campos incluídos no corpo da requisição serão cadastrados.
    /// 
    /// ## Exemplo de body que receberemos como resposta:
    /// 
    /// ```json
    ///     {   
    ///         "id": "67ce4b3d61760e36f862dd59"
    ///         "nome": "João",
    ///         "CNPJ": "1234567891011",
    ///         "telefone": "11958757000"
    ///         "email": "joao@exemplo.com",
    ///         "senha": "senha123",
    ///         "perfil": "Clinica"
    ///     }
    ///     
    /// Perceba que a resposta irá criar um ID automático gerado pelo banco de dados.
    /// 
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Clinica criado com sucesso</response>
    /// <response code="201">Registro efetuado</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost("CadastrarClinica")]
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CadastrarClinica([FromBody] Clinica clinica)
    {
        if (ModelState.IsValid)
        {
            clinica.Perfil = "Clinica";
            await _clinicaService.Criar(clinica);
            return CreatedAtAction(nameof(ConsultarTodos), new { id = clinica.Id }, clinica); 
        }
        return BadRequest(ModelState); 
    }

    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var Clinicas = await _clinicaService.ConsultarTodos(); 
        return View(Clinicas); 
    }

    /// <summary>
    ///     Consultar a lista com todas as Clinicas.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar todos os clínicas no banco
    /// 
    /// Use este endpoint quando precisar consultar as clínicas com todos campos específicos de uma clínica.
    /// 
    /// ### Exemplo de body que receberemos como resposta:
    /// 
    /// ```json
    ///    [
    ///         {
    ///             "id": "67ce4b3d61760e36f862dd59",
    ///             "nome": "Delfos",
    ///             "cnpj": "1234567891011",
    ///             "telefone": "4255888069",
    ///             "email": "delfos@delfos.com",
    ///             "senha": "123456",
    ///             "perfil": "Clinica"
    ///         },
    ///         {
    ///             "id": "67cf2b26c33c2aa2b9f0d069",
    ///             "nome": "Machine",
    ///             "cnpj": "1234567891011",
    ///             "telefone": "11958757740",
    ///             "email": "machine@delfos.com",
    ///             "senha": "123456",
    ///             "perfil": "Clinica"
    ///         }
    ///    ]
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Clinica criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarTodos")]
    [Produces("application/json")]
    public async Task<IActionResult> ConsultarTodos()
    {
        var Clinicas = await _clinicaService.ConsultarTodos();
        return Ok(Clinicas);
    }

    /// <summary>
    ///     Consultar uma única Clinica.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar uma única clínica pelo ID do banco de dados
    /// 
    /// Use este endpoint quando precisar consultar somente um clínica com todos campos específicos de uma clínica.
    /// 
    /// ### Exemplo de body que receberemos como resposta:
    /// 
    /// ```json
    ///    {
    ///        "id": "67ce4b3d61760e36f862dd59",
    ///        "nome": "Delfos",
    ///        "cnpj": "1234567891011",
    ///        "telefone": "4255888069",
    ///        "email": "delfos@delfos.com",
    ///        "senha": "123456",
    ///        "perfil": "Clinica"
    ///     }
    ///  
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Dados consultados com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarId/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarId(string id)
    {
        var clinica = await _clinicaService.ConsultarId(id);

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
        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdClinica")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var clinicas = await _clinicaService.ConsultarId(userIdString);
        if (clinicas == null)
        {
            return NotFound();
        }

        return View(clinicas);
    }

    [HttpPost("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar(Clinica clinica)
    {
        if (!ModelState.IsValid)
        {
            return View(clinica);
        }

        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdClinica")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var clinicaExistente = await _clinicaService.ConsultarId(userIdString);

        if (clinicaExistente == null)
        {
            return NotFound();
        }

        clinicaExistente.Nome = clinica.Nome;
        clinicaExistente.Telefone = clinica.Telefone;
        clinicaExistente.Email = clinica.Email;
        clinicaExistente.Senha = clinica.Senha;

        await _clinicaService.Atualizar(clinicaExistente);

        TempData["SuccessMessage"] = "Clinica atualizado com sucesso!";
        return RedirectToAction("MensagemAtualizacao");
    }

    [HttpGet("MensagemAtualizacao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemAtualizacao()
    {
        return View();
    }

    /// <summary>
    ///     Atualizar todos os dados de uma Clinica específica.
    /// </summary>
    /// 
    /// <remarks>
    /// ## Atualizar todos os dados da clínica
    /// 
    /// Use este endpoint quando precisar atualizar todos os dados de uma única vez. Usando está rota, se não for informado os dados dos demais campos, eles serão afetados por informações defaults do sistema.
    /// 
    /// <param name="id" type="string" example="67ce4b3d61760e36f862dd59">ID do Clinica a ser atualizado</param>
    /// 
    /// ### Campos que podem ser atualizados:
    /// 
    /// - **nome**: Nome da clínica
    /// - **cnpj**: CNPJ da clínica
    /// - **telefone**: Número de telefone para contato
    /// - **email**: Endereço de email para contato
    /// - **senha**: Senha de acesso (será criptografada)
    /// 
    /// ### Campos que não podem ser atualizados:
    /// 
    /// - **id**: ID do banco de dados
    /// - **perfil**: Perfil da clínica que será sem o padrão "Clinica" a não ser que ela seja usuário "Comum".
    /// 
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    /// {
    ///     "id": "67ce4b3d61760e36f862dd59",
    ///     "nome": "Delfos",
    ///     "cnpj": "1234567891011",
    ///     "telefone": "11987654321",
    ///     "email": "novo.email@clinica.com.br"
    ///     "perfil": "Clinica",
    /// }
    /// ```
    /// 
    /// </remarks>
    /// 
    /// <param name="clinica"></param>
    /// 
    /// <response code="200">Clinica atualizado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Clinica não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut("AtualizarClinica/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> AtualizarClinica(string id, [FromBody] Clinica clinica)
    {
        if (string.IsNullOrEmpty(id) || clinica == null || id != clinica.Id)
        {
            return BadRequest("Id do Clinica não corresponde ao fornecido.");
        }

        var clinicaExistente = await _clinicaService.ConsultarId(id);

        if (clinicaExistente == null)
        {
            return NotFound();
        }

        clinicaExistente.Nome = clinica.Nome;
        clinicaExistente.Telefone = clinica.Telefone;
        clinicaExistente.Email = clinica.Email;
        clinicaExistente.Senha = clinica.Senha;

        await _clinicaService.Atualizar(clinicaExistente);

        return Ok(clinicaExistente); 
    }

    /// <summary>
    ///     Atualiza parcialmente os dados de uma clínica existente
    /// </summary>
    /// 
    /// <param name="id" type="string" example="67ce4b3d61760e36f862dd59">ID da clínica a ser atualizada</param>
    /// <param name="camposParaAtualizar">Campos específicos a serem atualizados</param>
    /// 
    /// <remarks>
    /// 
    /// ## Atualização Parcial de Clínica
    /// 
    /// Use este endpoint quando precisar atualizar apenas alguns campos específicos de uma clínica,
    /// sem a necessidade de enviar todos os dados.
    /// 
    /// ### Campos que podem ser atualizados:
    /// - **nome**: Nome da clínica
    /// - **cnpj**: CNPJ da empresa/Clínica
    /// - **telefone**: Número de telefone para contato
    /// - **email**: Endereço de email para contato
    /// - **senha**: Senha de acesso (será criptografada)
    /// 
    /// ### Campos que não podem ser atualizados:
    /// - **Perfil**: Perfil = Clínica pois a rota utilizada foi a de parceiros e não de cliente
    /// 
    /// ### Exemplo de requisição:
    /// 
    /// ```json
    /// {
    ///     "id": "67ce4b3d61760e36f862dd59",
    ///     "email": "novo.email@clinica.com.br"
    /// }
    /// ```
    /// 
    /// Somente os campos incluídos no corpo da requisição serão atualizados.
    /// 
    /// ### Exemplo de resposta da requisição:
    /// 
    /// ```json
    /// {
    ///     "id": "67ce4b3d61760e36f862dd59",
    ///     "nome": "Delfos",
    ///     "cnpj": "1234567891011",
    ///     "telefone": "string",
    ///     "email": "delfos@delfos.com", -- Validação pode ser feita pelo campo informado!
    ///     "senha": "string",
    ///     "perfil": "Clinica"
    /// }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Clinica atualizada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Clinica não encontrada</response>
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

        var clinicaAtualizada = await _clinicaService.AtualizarParcial(id, camposParaAtualizar);

        if (clinicaAtualizada == null)
        {
            return NotFound("Clinica não encontrada.");
        }

        return Ok(clinicaAtualizada);
    }


    [HttpGet("ConfirmarExcluir/{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmarExcluir(string id)
    {
        var Clinica = await _clinicaService.ConsultarId(id);
        
        if (Clinica == null)
        {
            return NotFound();
        }

        return View(Clinica);
    }


    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Excluir(string id)
    {
        var Clinica = await _clinicaService.ConsultarId(id);
        
        if (Clinica != null)
        {
            // Exclui o Clinica do banco de dados
            await _clinicaService.Excluir(id);

            //await HttpContext.SignOutAsync();
            
            TempData["SuccessMessage"] = "Clinica excluído com sucesso.";
            //return RedirectToAction("MensagemExclusao", "Clinica"); 
        }

        TempData["ErrorMessage"] = "Clinica não encontrado.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("MensagemExclusao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemExclusao()
    {
        return View();
    }

    /// <summary>
    ///     Excluir os Clinicas do banco de dados.
    /// </summary>
    ///
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do Clinica a ser excluído</param>
    /// 
    /// <remarks>
    /// 
    /// ## Excluir uma clínica do banco de dados e dos cadastros.
    /// 
    /// ### Exemplo da requisição para excluir uma clínica:
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
    ///         "message": "Clinica excluído com sucesso."
    ///     }
    /// ``` 
    /// 
    /// Uma vez excluida da base, não tem reversão desta ação.
    /// </remarks>
    /// 
    /// <response code="200">Clinica criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete("ExcluirClinica/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> ExcluirClinica(string id)
    {
        var Clinica = await _clinicaService.ConsultarId(id);
        
        if (Clinica == null)
        {
            return NotFound();
        }

        await _clinicaService.Excluir(id);

        return Ok(new { message = "Clínica excluída com sucesso." });  
    }


}