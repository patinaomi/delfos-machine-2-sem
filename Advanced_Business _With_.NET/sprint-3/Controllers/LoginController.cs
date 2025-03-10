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

        // Exibe a página de login
        [HttpGet("Logar")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Logar()
        {
            return View();
        }

        // Exibe a página com histórico dos logins
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
                // Se o usuário não for encontrado, retorna erro
                return RedirectToAction("Login");
            }

            // Realiza o login do usuário e adiciona as claims necessárias
            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, usuario.Nome),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, usuario.Email),
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

            // Passando os dados para a View através do ViewData
            ViewData["UsuarioId"] = usuario.Id;
            ViewData["UsuarioNome"] = usuario.Nome;
            ViewData["UsuarioEmail"] = usuario.Email;

            Console.WriteLine($"Nome do Usuário: {usuario.Nome}");
            Console.WriteLine($"Email do Usuário: {usuario.Email}");
            Console.WriteLine($"ID do Usuário: {usuario.Id}");

            // Redireciona para a página principal ou página desejada
            return RedirectToAction("Inicio", "Home");
        }
        
        // Página de mensagem de erro em caso de falha no login
        [HttpGet("MensagemErro")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult MensagemErro()
        {
            return View();
        }

        // Método para realizar o logout
        public async Task<IActionResult> Logout()
        {
            // Realiza o logout, limpando o cookie de autenticação
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Logar", "Login");
        }

        
    }
}
