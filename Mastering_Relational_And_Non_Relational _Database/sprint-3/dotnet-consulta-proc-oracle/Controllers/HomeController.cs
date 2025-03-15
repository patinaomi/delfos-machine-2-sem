using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LexusTech.Models;

namespace LexusTech.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {   
        _logger.LogInformation("Acessou a página Index.");
        return View();
    }

    public IActionResult Problema()
    {
        return View();
    }

    public IActionResult Solucao()
    {
        return View();
    }

    public IActionResult Projeto()
    {
        return View();
    }

    public IActionResult Prototipo()
    {
        return View();
    }
    public IActionResult Diferencial()
    {
        return View();
    }

    public IActionResult Objetivo()
    {
        return View();
    }

    public IActionResult VideoProjeto()
    {
        return View();
    }

    public IActionResult Time()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}