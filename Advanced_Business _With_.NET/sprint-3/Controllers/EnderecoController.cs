using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("Endereco")] 
public class EnderecoController : Controller
{
    private readonly IEnderecoService _enderecoService;

    public EnderecoController(IEnderecoService enderecoService)
    {
        _enderecoService = enderecoService;
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
    public async Task<IActionResult> Criar(EnderecoDTO enderecoDTO)
    {

        if (ModelState.IsValid)
        {

            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;
            if (string.IsNullOrEmpty(idUsuario))
                return Unauthorized("Usuário não logado.");

            var endereco = new Endereco
            {
                CEP = enderecoDTO.CEP,
                Estado = enderecoDTO.Estado,
                Cidade = enderecoDTO.Cidade,
                Bairro = enderecoDTO.Bairro,
                Rua = enderecoDTO.Rua,
                IdUsuario = idUsuario
            };

            await _enderecoService.Criar(endereco);

            TempData["SuccessMessage"] = "Endereco cadastrado com sucesso!";
        }
        return View(enderecoDTO);
    }

    [HttpGet("Mensagem")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Mensagem()
    {
        return View();
    }

    /// <summary>
    ///     Cadastra um novo endereço de preferência para o usuário.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Cadastrar novo endereço de preferência para o usuário
    /// 
    /// Use este endPoint para cadastrar um endereço de peferência para o usuário. Com este endereço, o modelo de IA será capaz de buscar uma  clínica próxima do cliente.
    /// 
    /// ### Campos que devem ser utilizados para criar um novo dia:
    /// - **idUsuario** : IdUsuario que deseja registrar o endereço
    /// - **cep** string : CEP de preferência e não de residência
    /// - **estado** string : Estado de preferência e não de residência
    /// - **cidade** string : Cidade de preferência e não de residência
    /// - **bairro** string : Bairro de preferência e não de residência
    /// - **rua** string : Rua de preferência e não de residência
    /// 
    /// ### Campos que não devem ser utilizados para criar um novo dia:
    /// - **id** : Id do dia que será gerado automaticamente
    ///
    /// 
    /// ### Exemplo de body para requisição:
    /// 
    /// ```json
    ///     {   
    ///         "idUsuario": "67cc95b32811515d372209ce",
    ///         "cep": "12345678",
    ///         "estado": "São Paulo",
    ///         "cidade": "São Paulo",
    ///         "bairro": "Centro",
    ///         "rua": "Cantori 30"
    ///     }
    /// ```
    /// 
    /// ### Exemplo de resposta:
    /// 
    /// ```json
    ///     {   
    ///         "id": "67cc95b32811515d37220000",
    ///         "idUsuario": "67cc95b32811515d372209ce",
    ///         "cep": "12345678",
    ///         "estado": "São Paulo",
    ///         "cidade": "São Paulo",
    ///         "bairro": "Centro",
    ///         "rua": "Cantori 30"
    ///     }
    /// ```
    /// 
    /// </remarks>
    /// <response code="201">Endereço criado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="500">Erro interno</response>
    [HttpPost("CadastrarEndereco")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CadastrarEndereco([FromBody] EnderecoDTO enderecoDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (string.IsNullOrEmpty(enderecoDTO.IdUsuario))
        return BadRequest("O campo IdUsuario é obrigatório.");


        var endereco = new Endereco
        {
            CEP = enderecoDTO.CEP,
            Estado = enderecoDTO.Estado,
            Cidade = enderecoDTO.Cidade,
            Bairro = enderecoDTO.Bairro,
            Rua = enderecoDTO.Rua,
            IdUsuario = enderecoDTO.IdUsuario
        };

        await _enderecoService.Criar(endereco);

        return CreatedAtAction(nameof(Consultar), new { id = endereco.Id }, endereco);
    }


    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var enderecos = await _enderecoService.ConsultarTodos(); 
        return View(enderecos); 
    }

    /// <summary>
    ///     Consultar o endereço de preferência do usuário.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar todos os registros de endereço do banco de dados
    /// 
    /// Use este endPoint se seu objetivo é recuperar todos os registros de endereços do banco de dados
    /// 
    /// ### Campos que disponíveis na requisição:
    /// - **id** : Id do banco de dados, que foi gerado automaticamente.
    /// - **idUsuario** : IdUsuario que deseja registrar o endereço
    /// - **cep** string : CEP de preferência e não de residência
    /// - **estado** string : Estado de preferência e não de residência
    /// - **cidade** string : Cidade de preferência e não de residência
    /// - **bairro** string : Bairro de preferência e não de residência
    /// - **rua** string : Rua de preferência e não de residência
    /// 
    /// ### Exemplo de body que virá de resposta:
    /// 
    /// ```json
    ///[
    ///    {
    ///        "id": "67cdee51b304fd2aaac177c9",
    ///        "idUsuario": "67cc95b32811515d372209ce",
    ///        "cep": "01739018",
    ///        "estado": "São Paulo",
    ///        "cidade": "São Paulo",
    ///        "bairro": "Vila Andrade",
    ///        "rua": "Cantori 30"
    ///    },
    ///    {
    ///        "id": "67cdee91b304fd2aaac177ca",
    ///        "idUsuario": "67cc95b32811515d372209ce",
    ///        "cep": "05728020",
    ///        "estado": "São Paulo",
    ///        "cidade": "São Paulo",
    ///        "bairro": "Teste",
    ///        "rua": "Teste"
    ///    }
    ///]
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Endereco consultado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarTodosEndereco")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarTodosEndereco()
    {
        var enderecos = await _enderecoService.ConsultarTodos(); 
        return Ok(enderecos);
    }

    /// <summary>
    ///     Consultar um único registro de preferência de endereço que o usuário cadastrou.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Consultar um único endereço de preferência registrado, sendo consultado pelo ID do banco de dados
    /// 
    /// Use este endpoint quando precisar consultar somente um registro com todos campos específicos.
    /// 
    /// ### Campos que devem ser utilizados para consultar um endereço de preferência:
    /// 
    /// - **id**: ID do banco e não o idUsuario
    /// 
    ///  ### Exemplo de body para requisição:
    ///  
    /// ```json
    ///     "id": "67cdee91b304fd2aaac177ca"
    /// ```
    /// 
    /// ### Exemplo de body que receberemos como resposta:
    /// 
    /// ```json
    ///    {
    ///         "id": "67cdee91b304fd2aaac177ca",
    ///         "idUsuario": "67cc95b32811515d372209ce",
    ///         "cep": "05728020",
    ///         "estado": "São Paulo",
    ///         "cidade": "São Paulo",
    ///         "bairro": "Teste",
    ///         "rua": "Teste"
    ///     }
    /// ```
    /// </remarks>
    /// 
    /// <response code="200">Dia consultado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarEnderecoId/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarEnderecoId(string id)
    {
        var endereco = await _enderecoService.ConsultarId(id);

        if (endereco == null)
        {
            return NotFound(new { message = "Endereço não encontrado." });
        }

        return Ok(endereco);
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

        var endereco = await _enderecoService.ConsultarPorUsuarioId(userIdString);
        if (endereco == null)
        {
            return NotFound();
        }

        return View(endereco);
    }

    [HttpPost("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar(Endereco endereco)
    {
        if (!ModelState.IsValid)
        {
            return View(endereco);
        }

        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        //var enderecoExistente = await _enderecoService.ConsultarPorUsuarioId
        var enderecoExistente = await _enderecoService.ConsultarId(userIdString);

        if (enderecoExistente == null)
        {
            return NotFound();
        }

        enderecoExistente.CEP = endereco.CEP;
        enderecoExistente.Estado = endereco.Estado;
        enderecoExistente.Cidade = endereco.Cidade;
        enderecoExistente.Bairro = endereco.Bairro;
        enderecoExistente.Rua = endereco.Rua;

        await _enderecoService.Atualizar(enderecoExistente);

        TempData["SuccessMessage"] = "Usuário atualizado com sucesso!";
        //return RedirectToAction("MensagemAtualizacao");
        return View(enderecoExistente);
    }

    [HttpGet("MensagemAtualizacao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemAtualizacao()
    {
        return View();
    }

    /// <summary>
    ///     Atualiza o endereço completo existente do usuário, com base no ID do usuário e ID do banco de dados.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Atualizar a preferência de endereço cadastrado pelo usuário
    /// 
    /// Use este endpoint se o objetivo for atualizar todos os campos do endereço no cadastro. Se for parcial, utilize outro endPoint.
    /// 
    /// ### Exemplo de requisição
    /// 
    /// ```json
    ///     {   
    ///         "id": "67ce4b3d61760e36f862dd59",
    ///         "idUsuario": "67cc95b32811515d372209ce",
    ///         "cep": "05763290",
    ///         "estado": "SP", 
    ///         "cidade": "São Paulo",
    ///         "Bairro": "Vila Madalena",
    ///         "Rua": "Rua do Pão de Queijo"
    ///     }
    /// 
    /// ``` 
    /// 
    /// ### Se preencher alguns campos e outros não, estes campos serão preenchidos com dados inválidos e default criado pelo sistema.
    /// </remarks>
    /// 
    /// 
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do usuário a ser atualizado o endereço.</param>
    /// <param name="enderecoDTO">Dados do endereço a serem atualizados.</param>
    /// <response code="200">Endereço atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="401">Endereço não autorizado</response>
    /// <response code="404">Endereço não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut("AtualizarEndereco/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarEndereco(string id, [FromBody] EnderecoDTO enderecoDTO)
    {
        if (string.IsNullOrEmpty(id) || enderecoDTO == null || id != enderecoDTO.Id)
        {
            return BadRequest("Id do Clinica não corresponde ao fornecido.");
        }

        var enderecoExistente = await _enderecoService.ConsultarId(id);

        if (enderecoExistente == null)
        {
            return NotFound();
        }

        enderecoExistente.CEP = enderecoDTO.CEP;
        enderecoExistente.Estado = enderecoDTO.Estado;
        enderecoExistente.Cidade = enderecoDTO.Cidade;
        enderecoExistente.Bairro = enderecoDTO.Bairro;
        enderecoExistente.Rua = enderecoDTO.Rua;

        await _enderecoService.Atualizar(enderecoExistente);

        return Ok(enderecoExistente);
    }

    [HttpGet("ConfirmarExcluir/{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmarExcluir(string id)
    {
        var endereco = await _enderecoService.ConsultarPorUsuarioId(id);
        
        if (endereco == null)
        {
            return NotFound();
        }

        return View(endereco);
    }

    /// <summary>
    ///     Atualiza parcialmente os dados de um endereço existente
    /// </summary>
    /// 
    /// <param name="id" type="string" example="67ce4b3d61760e36f862dd59">ID da endereço a ser atualizada</param>
    /// <param name="camposParaAtualizar">Campos específicos a serem atualizados</param>
    /// 
    /// <remarks>
    /// 
    /// ## Atualização Parcial dos dados
    /// 
    /// Use este endpoint quando precisar atualizar apenas alguns campos específicos de uma tabela,
    /// sem a necessidade de enviar todos os dados.
    /// 
    /// ### Campos que podem ser atualizados:
    /// - **id**: ID gerado pelo banco de dados
    /// - **cep**: CEP do endereço
    /// - **Estado**: Estado do endereço
    /// - **Cidade**: Cidade do endereço de preferência do usuário
    /// - **Bairro**: Bairro do endereço
    /// - **Rua**: Endereço completo com número
    /// 
    /// ### Campos que não podem ser atualizados:
    /// - **IdUsuario**: IdUsuario que foi gerado pelo banco de dados
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
    ///     "id": "67cdee91b304fd2aaac177ca",
    ///     "idUsuario": "67cc95b32811515d372209ce",
    ///     "cep": "05763290",
    ///     "estado": "São Paulo",
    ///     "cidade": "São Paulo",
    ///     "bairro": "Teste",
    ///     "rua": "Teste"
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

        var clinicaAtualizada = await _enderecoService.AtualizarParcial(id, camposParaAtualizar);

        if (clinicaAtualizada == null)
        {
            return NotFound("Clinica não encontrada.");
        }

        return Ok(clinicaAtualizada);
    }


    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Excluir(string id)
    {
        var endereco = await _enderecoService.ConsultarPorUsuarioId(id);
        
        if (endereco != null)
        {
            await _enderecoService.Excluir(id);
            
            TempData["SuccessMessage"] = "Endereco excluído com sucesso.";
            //return RedirectToAction("MensagemExclusao", "Endereco"); 
        }

        TempData["ErrorMessage"] = "Endereco não encontrado.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("MensagemExclusao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemExclusao()
    {
        return View();
    }

    /// <summary>
    /// Exclui um endereço do usuário.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// ## Excluir um endereço de preferência do usuário cadastrado
    /// 
    /// Use este endoPoint se seu objetivo é excluir um cadastro contendo o endereço de preferência cadastrado errado. 
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
    ///         "message": "Endereço de preferência excluído com sucesso."
    ///     }
    /// ``` 
    /// 
    /// Uma vez excluida da base, não tem reversão desta ação.
    /// 
    /// </remarks>
    /// 
    /// <param name="id" type="string" example="67cdee51b304fd2aaac177c9">ID do endereço a ser excluído.</param>
    /// <response code="200">Endereço excluído com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    /// <response code="404">Endereço não encontrado</response>
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
        return BadRequest("ID do endereço não pode ser vazio.");

        var endereco = await _enderecoService.ConsultarId(id);
        if (endereco == null)
            return NotFound("Endereço não encontrado.");

        await _enderecoService.Excluir(id);
        return Ok(new { message = "Endereço excluído com sucesso." });
    }

}