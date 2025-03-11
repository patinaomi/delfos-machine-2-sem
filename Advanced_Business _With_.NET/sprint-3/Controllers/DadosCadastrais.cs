using Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

[Route("DadosCadastrais")]
public class DadosCadastraisController : Controller
{
    private readonly IMongoCollection<Usuario> _usuarios;
    private readonly IMongoCollection<Endereco> _enderecos;
    private readonly IMongoCollection<DiasPreferencia> _diasPreferencia;
    private readonly IMongoCollection<Turno> _turnos;
    private readonly IMongoCollection<Horarios> _horarios;


    public DadosCadastraisController(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("nomeDoBanco");

        _usuarios = database.GetCollection<Usuario>("t_usuario");
        _enderecos = database.GetCollection<Endereco>("t_endereco");
        _diasPreferencia = database.GetCollection<DiasPreferencia>("t_dias_preferencia");
        _turnos = database.GetCollection<Turno>("t_turno");
        _horarios = database.GetCollection<Horarios>("t_horario");
    }

    [HttpGet("Consultar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Consultar()
    {
        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Error");

        }

        var usuario = await _usuarios.Find(c => c.Id == userIdString).FirstOrDefaultAsync();
        if (usuario == null)
        {
            return RedirectToAction("Error");
        }

        var endereco = await _enderecos.Find(e => e.IdUsuario == userIdString).FirstOrDefaultAsync() ?? new Endereco();
        var diasPreferencia = await _diasPreferencia.Find(d => d.IdUsuario == userIdString).ToListAsync() ?? new List<DiasPreferencia>();
        var turno = await _turnos.Find(t => t.IdUsuario == userIdString).FirstOrDefaultAsync() ?? new Turno();
        var horarios = await _horarios.Find(h => h.IdUsuario == userIdString).FirstOrDefaultAsync() ?? new Horarios();

        var dadosCadastrais = new DadosCadastrais
        {
            Usuario = usuario,
            Endereco = endereco,
            DiasPreferencia = diasPreferencia,
            Turno = turno,
            Horarios = horarios
        };
        

        return View(dadosCadastrais); 
    }
}