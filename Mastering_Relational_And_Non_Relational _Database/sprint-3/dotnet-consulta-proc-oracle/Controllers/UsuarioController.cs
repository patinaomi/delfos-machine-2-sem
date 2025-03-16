using LexusTech.Infrastructure.Interfaces;
using LexusTech.Models;
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
    
    [AllowAnonymous]
    [HttpGet("Criar")]
    public IActionResult Criar()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost("Criar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar([Bind("Id,Nome,Sobrenome,Telefone,Email,DataNasc,Endereco")] Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            await _usuarioService.Criar(usuario);
            TempData["SuccessMessage"] = "Usuário cadastrado com sucesso!";
            //return RedirectToAction("Mensagem");
        }
        return View(usuario);
    }

    [HttpGet("Consultar")]
    public async Task<IActionResult> Consultar()
    {
        var usuarios = await _usuarioService.ConsultarTodos(); 
        return View(usuarios); 
    }

    [HttpGet("BuscarCliente")]
    public async Task<IActionResult> BuscarCliente()
    {
        return View(); 
    }

    [HttpPost("BuscarCliente")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BuscarCliente(int id)
    {
        if (id <= 0)
        {
            TempData["ErrorMessage"] = "ID inválido.";
            return View();
        }

        var usuarioEncontrado = await _usuarioService.BuscarCliente(id);

        if (usuarioEncontrado == null)
        {
            TempData["ErrorMessage"] = "Usuário não encontrado.";
            return View();
        }

        return View(usuarioEncontrado);
    }


    [HttpGet("Atualizar")]
    public IActionResult Atualizar(int id)
    {
        return View();
    }

    [HttpPost("Atualizar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Atualizar([Bind("Id,Nome,Sobrenome,Telefone,Email,DataNasc,Endereco")] Usuario usuario)
    {
        if (!ModelState.IsValid)
        {
            return View(usuario);
        }

        await _usuarioService.Atualizar(usuario);

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
    public async Task<IActionResult> Excluir([Bind("Id")] Usuario usuario)
    {
        if (!ModelState.IsValid)
        {
            return View(usuario);
        }

        await _usuarioService.Atualizar(usuario);

        TempData["SuccessMessage"] = "Usuário excluído com sucesso!";
        //return RedirectToAction("MensagemAtualizacao");
        return View("ExcluirView");
    }


}