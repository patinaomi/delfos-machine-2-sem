using Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("DadosCadastrais")]
public class DadosCadastraisController : Controller
{
    private readonly IDadosCadastraisService _dadosCadastraisService;

    public DadosCadastraisController(IDadosCadastraisService dadosCadastraisService)
    {
        _dadosCadastraisService = dadosCadastraisService;
    }

    [HttpGet("Consultar")]
    public IActionResult Consultar()
    {
        var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;
            if (string.IsNullOrEmpty(idUsuario))
                return Unauthorized("Usuário não logado.");

        // Imprimir no console o valor de idUsuario
        Console.WriteLine($"Dados do usuario: {idUsuario}");

        if (!string.IsNullOrEmpty(idUsuario))
        {
            var dadosCadastrais = _dadosCadastraisService.ObterDadosCadastraisPorUsuarioId(idUsuario);

            Console.WriteLine($"Dados cadastrais: {dadosCadastrais}");

            return View(dadosCadastrais);
        }
        

        return RedirectToAction("Error"); 
    }
}
