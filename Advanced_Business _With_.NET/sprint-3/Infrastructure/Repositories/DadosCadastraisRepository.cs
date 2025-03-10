using Project.Models;
using MongoDB.Driver;

public class DadosCadastraisRepository : IDadosCadastraisRepository
{
    private readonly IMongoCollection<Usuario> _usuarios;
    private readonly IMongoCollection<Endereco> _enderecos;
    private readonly IMongoCollection<DiasPreferencia> _diasPreferencia;
    private readonly IMongoCollection<Turno> _turnos;
    private readonly IMongoCollection<Horarios> _horarios;

    public DadosCadastraisRepository(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("nomeDoBanco");

        _usuarios = database.GetCollection<Usuario>("usuarios");
        _enderecos = database.GetCollection<Endereco>("enderecos");
        _diasPreferencia = database.GetCollection<DiasPreferencia>("diasPreferencia");
        _turnos = database.GetCollection<Turno>("turnos");
        _horarios = database.GetCollection<Horarios>("horarios");
    }

    public Usuario ObterUsuarioPorId(string usuarioId)
    {
        return _usuarios.Find(u => u.Id == usuarioId).FirstOrDefault();
    }

    public Endereco ObterEnderecoPorUsuarioId(string usuarioId)
    {
        return _enderecos.Find(e => e.IdUsuario == usuarioId).FirstOrDefault();
    }

    public List<DiasPreferencia> ObterDiasPreferenciaPorUsuarioId(string usuarioId)
    {
        return _diasPreferencia.Find(dp => dp.IdUsuario == usuarioId).ToList();
    }

    public List<Turno> ObterTurnosPorUsuarioId(string usuarioId)
    {
        return _turnos.Find(t => t.IdUsuario == usuarioId).ToList();
    }

    public List<Horarios> ObterHorariosPorUsuarioId(string usuarioId)
    {
        return _horarios.Find(h => h.IdUsuario == usuarioId).ToList();
    }
}
