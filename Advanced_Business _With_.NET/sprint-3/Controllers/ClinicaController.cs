using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
    public async Task<IActionResult> Criar([Bind("Id,CNPJ,Nome,Sobrenome,Telefone,Email,Senha")] Clinica Clinica)
    {
        if (ModelState.IsValid)
        {
            await _clinicaService.Criar(Clinica);
            TempData["SuccessMessage"] = "Clinica cadastrado com sucesso!";
            //return RedirectToAction("Mensagem");
        }
        return View(Clinica);
    }

    [HttpGet("Mensagem")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Mensagem()
    {
        return View();
    }

    // Rota de API para criar um Clinica
    /// <summary>
    ///     Cria um novo Clinica.
    /// </summary>
    /// 
    /// <remarks>
    /// Exemplo de body:
    /// 
    /// {
    ///     "nome": "João",
    ///     "sobrenome": "Silva",
    ///     "email": "joao@exemplo.com",
    ///     "senha": "senha123"
    /// }
    /// </remarks>
    /// 
    /// <response code="200">Clinica criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost("CadastrarClinica")]
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CadastrarClinica([FromBody] Clinica clinica)
    {
        if (ModelState.IsValid)
        {
            clinica.Perfil = "Clinica";
            await _clinicaService.Criar(clinica);
            return CreatedAtAction(nameof(ConsultarTodosClinicas), new { id = clinica.Id }, clinica); 
        }
        return BadRequest(ModelState); 
    }


    // Rota de View
    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var Clinicas = await _clinicaService.ConsultarTodos(); 
        return View(Clinicas); 
    }

    // Rota de API
    /// <summary>
    ///     Consultar a lista com todo os Clinicas.
    /// </summary>
    /// 
    /// <remarks>
    /// Exemplo de body:
    /// 
    /// [
    ///     { 
    ///         "id": "67cc95b32811515d372209ce",
    ///         "nome": "claudio",
    ///         "telefone": "11958757740",
    ///         "email": "claudio_cssp@hotmail.com",
    ///         "senha": "123456"
    ///     },
    ///     {
    ///         "id": "67cca0540924d08d2c4b7819",
    ///         "nome": "Caio",
    ///         "telefone": "11958757740",
    ///         "email": "caio@delfos.com",
    ///         "senha": "123456"
    ///     }
    ///]
    /// </remarks>
    /// 
    /// <response code="200">Clinica criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarTodosClinicas")]
    [Produces("application/json")]
    public async Task<IActionResult> ConsultarTodosClinicas()
    {
        var Clinicas = await _clinicaService.ConsultarTodos();
        return Ok(Clinicas);
    }

    // View para atualizar um Clinica
    [HttpGet("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar()
    {
        //var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdClinica")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var Clinica = await _clinicaService.ConsultarId(userIdString);
        if (Clinica == null)
        {
            return NotFound();
        }

        return View(Clinica);
    }

    [HttpPost("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar(Clinica Clinica)
    {
        if (!ModelState.IsValid)
        {
            return View(Clinica);
        }

        //var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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

        clinicaExistente.Nome = Clinica.Nome;
        clinicaExistente.Telefone = Clinica.Telefone;
        clinicaExistente.Email = Clinica.Email;
        clinicaExistente.Senha = Clinica.Senha;

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

    // Rota de API para atualizar um Clinica
    /// <summary>
    /// Atualizar os dados de um Clinica.
    /// </summary>
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do Clinica a ser atualizado</param>
    /// <param name="nome" type="string" example="Claudio">Nome do Clinica</param>
    /// <param name="telefone" type="string" example="11958887577">Telefone do Clinica</param>
    /// <param name="email" type="string" example="claudio@defos.com.br">Email de contato</param>
    /// <param name="senha" type="string" example="123123">Nova senha do Clinica</param>
    /// 
    /// <response code="200">Clinica atualizado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Clinica não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut("AtualizarClinica/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> AtualizarClinica(string id, [FromBody] Clinica Clinica)
    {
        if (string.IsNullOrEmpty(id) || Clinica == null || id != Clinica.Id)
        {
            return BadRequest("Id do Clinica não corresponde ao fornecido.");
        }

        var clinicaExistente = await _clinicaService.ConsultarId(id);

        if (clinicaExistente == null)
        {
            return NotFound();
        }

        clinicaExistente.Nome = Clinica.Nome;
        clinicaExistente.Telefone = Clinica.Telefone;
        clinicaExistente.Email = Clinica.Email;
        clinicaExistente.Senha = Clinica.Senha;

        await _clinicaService.Atualizar(clinicaExistente);

        return Ok(clinicaExistente); 
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

            // Desloga o Clinica
            //await _context.SaveChangesAsync();
            await HttpContext.SignOutAsync();
            
            // Redireciona para a página de login ou para onde você preferir
            TempData["SuccessMessage"] = "Clinica excluído com sucesso.";
            return RedirectToAction("MensagemExclusao", "Clinica"); 
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

    // Rota de API para excluir um Clinica
    /// <summary>
    ///     Excluir os Clinicas do banco de dados.
    /// </summary>
    /// 
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do Clinica a ser excluído</param>
    /// 
    /// <remarks>
    /// Exemplo de body:
    /// 
    /// {
    ///     "nome": "João",
    ///     "sobrenome": "Silva",
    ///     "email": "joao@exemplo.com",
    ///     "senha": "senha123"
    /// }
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

        return Ok(new { message = "Clinica excluído com sucesso." });  
    }




}