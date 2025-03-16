using LexusTech.Infrastructure.Interfaces;
using LexusTech.Models;
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
    
    [AllowAnonymous]
    [HttpGet("Criar")]
    public IActionResult Criar()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost("Criar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(Clinica clinica)
    {
        if (ModelState.IsValid)
        {
            await _clinicaService.Criar(clinica);
            TempData["SuccessMessage"] = "Clinica cadastrado com sucesso!";
        }
        return View(clinica);
    }

    [HttpGet("Consultar")]
    public async Task<IActionResult> Consultar()
    {
        var clinicas = await _clinicaService.ConsultarTodos(); 
        return View(clinicas); 
    }

    [HttpGet("BuscarClinica")]
    public async Task<IActionResult> BuscarClinica()
    {
        return View(); 
    }

    [HttpPost("BuscarClinica")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BuscarClinica(int id)
    {
        if (id <= 0)
        {
            TempData["ErrorMessage"] = "ID inválido.";
            return View();
        }

        var clinicaEncontrado = await _clinicaService.BuscarClinica(id);

        if (clinicaEncontrado == null)
        {
            TempData["ErrorMessage"] = "Clinica não encontrado.";
            return View();
        }

        return View(clinicaEncontrado);
    }


    [HttpGet("Atualizar")]
    public IActionResult Atualizar(int id)
    {
        return View();
    }

    [HttpPost("Atualizar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Atualizar(Clinica clinica)
    {
        if (!ModelState.IsValid)
        {
            return View(clinica);
        }

        await _clinicaService.Atualizar(clinica);

        TempData["SuccessMessage"] = "Clinica atualizado com sucesso!";
        return View();
    }

    [HttpGet("ExcluirView")]
    public IActionResult ExcluirView(int id)
    {
        return View();
    }


    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Excluir(Clinica clinica)
    {
        if (!ModelState.IsValid)
        {
            return View(clinica);
        }

        await _clinicaService.Atualizar(clinica);

        TempData["SuccessMessage"] = "Clinica excluído com sucesso!";
        //return RedirectToAction("MensagemAtualizacao");
        return View("ExcluirView");
    }


}