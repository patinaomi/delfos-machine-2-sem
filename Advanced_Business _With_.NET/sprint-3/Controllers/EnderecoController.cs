using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("Endereco")] 
public class EnderecoController : Controller
{
    //private readonly ApplicationDbContext _context;
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


            // Cria o objeto Endereco
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
            //return RedirectToAction("Mensagem");
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
    /// Cadastra um novo endereço de preferência para o usuário.
    /// </summary>
    /// <remarks>
    /// Exemplo de body:
    /// 
    /// {   
    ///     "idUsuario": "67cc95b32811515d372209ce",
    ///     "cep": "12345678",
    ///     "estado": "São Paulo",
    ///     "cidade": "São Paulo",
    ///     "bairro": "Centro",
    ///     "rua": "Cantori 30"
    /// }
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

        return CreatedAtAction(nameof(Consultar), new { id = endereco.Id }, endereco);
    }


    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var enderecos = await _enderecoService.ConsultarTodos(); 
        return View(enderecos); 
    }

    // Rota de API
    /// <summary>
    ///     Consultar o endereço de preferência do usuário.
    /// </summary>
    /// 
    /// <remarks>
    /// Exemplo de body que virá de resposta:
    /// 
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
    ///    ]
    /// </remarks>
    /// 
    /// <response code="200">Endereco consultado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarEndereco")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarEndereco()
    {
        var enderecos = await _enderecoService.ConsultarTodos(); 
        return Ok(enderecos);
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

        //var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var enderecoExistente = await _enderecoService.ConsultarPorUsuarioId(userIdString);

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
        return RedirectToAction("MensagemAtualizacao");
    }

    [HttpGet("MensagemAtualizacao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemAtualizacao()
    {
        return View();
    }

    /// <summary>
    /// Atualiza um endereço existente do usuário, com base no ID do usuário.
    /// </summary>
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do usuário a ser atualizado o endereço.</param>
    /// <param name="enderecoDTO">Dados do endereço a serem atualizados.</param>
    /// <response code="200">Endereço atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="401">Usuário não autorizado</response>
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
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;
        if (string.IsNullOrEmpty(idUsuario))
            return Unauthorized("Usuário não logado.");

        var enderecoExistente = await _enderecoService.ConsultarPorUsuarioId(id);
        if (enderecoExistente == null || enderecoExistente.IdUsuario != idUsuario)
            return NotFound("Endereço não encontrado ou não pertence ao usuário.");

        enderecoExistente.CEP = enderecoDTO.CEP;
        enderecoExistente.Estado = enderecoDTO.Estado;
        enderecoExistente.Cidade = enderecoDTO.Cidade;
        enderecoExistente.Bairro = enderecoDTO.Bairro;
        enderecoExistente.Rua = enderecoDTO.Rua;

        await _enderecoService.Atualizar(enderecoExistente);

        return Ok(new { message = "Endereço atualizado com sucesso!" });
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


    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Excluir(string id)
    {
        var endereco = await _enderecoService.ConsultarPorUsuarioId(id);
        
        if (endereco != null)
        {
            // Exclui o endereco do banco de dados
            await _enderecoService.Excluir(id);
            
            // Redireciona para a página de login ou para onde você preferir
            TempData["SuccessMessage"] = "Endereco excluído com sucesso.";
            return RedirectToAction("MensagemExclusao", "Endereco"); 
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