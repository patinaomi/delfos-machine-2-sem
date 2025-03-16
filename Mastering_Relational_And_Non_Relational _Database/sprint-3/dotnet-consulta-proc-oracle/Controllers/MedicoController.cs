using LexusTech.Infrastructure.Interfaces;
using LexusTech.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("Medico")] 
public class MedicoController : Controller
{
    private readonly IMedicoService _medicoService;

    public MedicoController(IMedicoService medicoService)
    {
        _medicoService = medicoService;
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
    public async Task<IActionResult> Criar(Medico medico)
    {
        if (ModelState.IsValid)
        {
            await _medicoService.Criar(medico);
            TempData["SuccessMessage"] = "Usuário cadastrado com sucesso!";
            //return RedirectToAction("Mensagem");
        }
        return View(medico);
    }

    [HttpGet("Consultar")]
    public async Task<IActionResult> Consultar()
    {
        var medicos = await _medicoService.ConsultarTodos(); 
        return View(medicos); 
    }

    [HttpGet("BuscarMedico")]
    public async Task<IActionResult> BuscarMedico()
    {
        return View(); 
    }

    [HttpPost("BuscarMedico")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BuscarMedico(int id)
    {
        if (id <= 0)
        {
            TempData["ErrorMessage"] = "ID inválido.";
            return View();
        }

        var medicoEncontrado = await _medicoService.BuscarMedico(id);

        if (medicoEncontrado == null)
        {
            TempData["ErrorMessage"] = "Usuário não encontrado.";
            return View();
        }

        return View(medicoEncontrado);
    }


    [HttpGet("Atualizar")]
    public IActionResult Atualizar(int id)
    {
        return View();
    }

    [HttpPost("Atualizar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Atualizar(Medico medico)
    {
        if (!ModelState.IsValid)
        {
            return View(medico);
        }

        await _medicoService.Atualizar(medico);

        TempData["SuccessMessage"] = "Usuário atualizado com sucesso!";
        //return RedirectToAction("MensagemAtualizacao");
        return View();
    }

    [HttpGet("ExcluirView")]
    public IActionResult ExcluirView(int id)
    {
        return View();
    }


    [HttpPost("Excluir")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Excluir([Bind("Id")] Medico medico)
    {
        if (!ModelState.IsValid)
        {
            return View(medico);
        }

        await _medicoService.Atualizar(medico);

        TempData["SuccessMessage"] = "Usuário excluído com sucesso!";
        //return RedirectToAction("MensagemAtualizacao");
        return View("ExcluirView");
    }


}