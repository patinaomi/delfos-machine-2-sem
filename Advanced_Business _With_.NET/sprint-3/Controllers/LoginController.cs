using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Project.Infrastructure.Interfaces;

namespace Project.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet("Logar")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Logar()
        {
            return View();
        }

        [HttpGet("Consultar")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Consultar()
        {
            return View();
        }

        [HttpPost("Logar")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Logar(string email, string senha)
        {
            // Autentica o usuário
            var usuario = await _loginService.Autenticar(email, senha);
            if (usuario == null)
            {
                //return RedirectToAction("Login");
                ViewBag.ErrorMessage = "Usuário ou senha inválidos";
                return View();
            }

            // Realiza o login do usuário e adiciona as claims necessárias
            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, usuario.Nome ?? string.Empty),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, usuario.Email ?? string.Empty),
                new System.Security.Claims.Claim("IdUsuario", usuario.Id.ToString())
            };

            var claimsIdentity = new System.Security.Claims.ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new System.Security.Claims.ClaimsPrincipal(claimsIdentity);

            // Realiza o login do usuário
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            // Armazena o Id do usuário na sessão
            HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
            HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
            HttpContext.Session.SetString("UsuarioEmail", usuario.Email);
            HttpContext.Session.SetString("Perfil", usuario.Perfil);

            // Passando os dados para a View através do ViewData
            ViewData["UsuarioId"] = usuario.Id;
            ViewData["UsuarioNome"] = usuario.Nome;
            ViewData["UsuarioEmail"] = usuario.Email;
            ViewData["UsuarioPerfil"] = usuario.Perfil;

            Console.WriteLine($"Nome do Usuário: {usuario.Nome}");
            Console.WriteLine($"Email do Usuário: {usuario.Email}");
            Console.WriteLine($"ID do Usuário: {usuario.Id}");
            Console.WriteLine($"Perfil: {usuario.Perfil}");

            return RedirectToAction("Inicio", "Home");
        }
        
        [HttpGet("MensagemErro")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult MensagemErro()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            // Realiza o logout, limpando o cookie de autenticação
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Logar", "Login");
        }

        /// <summary>
        ///     Valida o login do usuário.
        /// </summary>
        /// 
        /// <remarks>
        /// 
        /// ## Validação de Login
        /// 
        /// Use este endpoint para validar as credenciais de um usuário.
        /// 
        /// ### Campos que devem ser utilizados para validar o login:
        /// - **email** string : Email do usuário
        /// - **senha** string : Senha do usuário
        /// 
        /// ### Exemplo de body para requisição:
        /// ```json
        ///     {
        ///         "email": "joao@exemplo.com",
        ///         "senha": "senha123"
        ///     }
        /// ```
        /// 
        /// ### Exemplo de resposta quando a validação for bem-sucedida:
        /// 
        /// ```json
        ///     {
        ///         "message": "Login válido"
        ///     }
        /// ```
        /// 
        /// ### Exemplo de resposta quando a validação falhar:
        /// 
        /// ```json
        ///     {
        ///         "message": "Usuário ou senha inválidos"
        ///     }
        /// ```
        /// </remarks>
        /// 
        /// <response code="200">Login válido</response>
        /// <response code="401">Usuário ou senha inválidos</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPost("ValidarLogin")]
        public async Task<IActionResult> ValidarLogin(string email, string senha)
        {
            var usuario = await _loginService.Autenticar(email, senha);
            if (usuario == null)
            {
                return Unauthorized("Usuário ou senha inválidos");
            }

            return Ok(new { message = "Login válido" });
        }

        
    }
}
