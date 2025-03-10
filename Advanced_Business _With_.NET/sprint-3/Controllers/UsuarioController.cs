using Project.Infrastructure.Interfaces;
using Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("Usuario")] 
public class UsuarioController : Controller
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
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
    public async Task<IActionResult> Criar([Bind("Id,Nome,Sobrenome,Telefone,Email,Senha")] Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            await _usuarioService.Criar(usuario);
            TempData["SuccessMessage"] = "Usuário cadastrado com sucesso!";
            return RedirectToAction("Mensagem");
        }
        return View(usuario);
    }

    [HttpGet("Mensagem")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Mensagem()
    {
        return View();
    }

    // Rota de API para criar um usuário
    /// <summary>
    ///     Cria um novo usuário.
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
    /// <response code="200">Usuário criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost("CadastrarUsuario")]
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CadastrarUsuario([FromBody] Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            await _usuarioService.Criar(usuario);
            return CreatedAtAction(nameof(ConsultarTodosUsuarios), new { id = usuario.Id }, usuario); 
        }
        return BadRequest(ModelState); 
    }


    // Rota de View
    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var usuarios = await _usuarioService.ConsultarTodos(); 
        return View(usuarios); 
    }

    // Rota de API
    /// <summary>
    ///     Consultar a lista com todo os usuários.
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
    /// <response code="200">Usuário criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("ConsultarTodosUsuarios")]
    [Produces("application/json")]
    public async Task<IActionResult> ConsultarTodosUsuarios()
    {
        var usuarios = await _usuarioService.ConsultarTodos();
        return Ok(usuarios);
    }

    // View para atualizar um usuário
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

        var usuario = await _usuarioService.ConsultarId(userIdString);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    [HttpPost("Atualizar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Atualizar(Usuario usuario)
    {
        if (!ModelState.IsValid)
        {
            return View(usuario);
        }

        //var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");
        }

        var usuarioExistente = await _usuarioService.ConsultarId(userIdString);

        if (usuarioExistente == null)
        {
            return NotFound();
        }

        usuarioExistente.Nome = usuario.Nome;
        usuarioExistente.Telefone = usuario.Telefone;
        usuarioExistente.Email = usuario.Email;
        usuarioExistente.Senha = usuario.Senha;

        await _usuarioService.Atualizar(usuarioExistente);

        TempData["SuccessMessage"] = "Usuário atualizado com sucesso!";
        return RedirectToAction("MensagemAtualizacao");
    }

    [HttpGet("MensagemAtualizacao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemAtualizacao()
    {
        return View();
    }

    // Rota de API para atualizar um usuário
    /// <summary>
    /// Atualizar os dados de um usuário.
    /// </summary>
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do usuário a ser atualizado</param>
    /// <param name="nome" type="string" example="Claudio">Nome do usuário</param>
    /// <param name="telefone" type="string" example="11958887577">Telefone do usuário</param>
    /// <param name="email" type="string" example="claudio@defos.com.br">Email de contato</param>
    /// <param name="senha" type="string" example="123123">Nova senha do usuário</param>
    /// 
    /// <response code="200">Usuário atualizado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Usuário não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut("AtualizarUsuario/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> AtualizarUsuario(string id, [FromBody] Usuario usuario)
    {
        if (string.IsNullOrEmpty(id) || usuario == null || id != usuario.Id)
        {
            return BadRequest("Id do usuário não corresponde ao fornecido.");
        }

        var usuarioExistente = await _usuarioService.ConsultarId(id);

        if (usuarioExistente == null)
        {
            return NotFound();
        }

        usuarioExistente.Nome = usuario.Nome;
        usuarioExistente.Telefone = usuario.Telefone;
        usuarioExistente.Email = usuario.Email;
        usuarioExistente.Senha = usuario.Senha;

        await _usuarioService.Atualizar(usuarioExistente);

        return Ok(usuarioExistente); 
    }


    [HttpGet("ConfirmarExcluir/{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmarExcluir(string id)
    {
        var usuario = await _usuarioService.ConsultarId(id);
        
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }


    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Excluir(string id)
    {
        var usuario = await _usuarioService.ConsultarId(id);
        
        if (usuario != null)
        {
            // Exclui o usuário do banco de dados
            await _usuarioService.Excluir(id);

            // Desloga o usuário
            //await _context.SaveChangesAsync();
            await HttpContext.SignOutAsync();
            
            // Redireciona para a página de login ou para onde você preferir
            TempData["SuccessMessage"] = "Usuário excluído com sucesso.";
            return RedirectToAction("MensagemExclusao", "Usuario"); 
        }

        TempData["ErrorMessage"] = "Usuário não encontrado.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("MensagemExclusao")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult MensagemExclusao()
    {
        return View();
    }

    // Rota de API para excluir um usuário
    /// <summary>
    ///     Excluir os usuários do banco de dados.
    /// </summary>
    /// 
    /// <param name="id" type="string" example="67cc95b32811515d372209ce">ID do usuário a ser excluído</param>
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
    /// <response code="200">Usuário criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete("ExcluirUsuario/{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> ExcluirUsuario(string id)
    {
        var usuario = await _usuarioService.ConsultarId(id);
        
        if (usuario == null)
        {
            return NotFound();
        }

        await _usuarioService.Excluir(id);

        return Ok(new { message = "Usuário excluído com sucesso." });  
    }




}