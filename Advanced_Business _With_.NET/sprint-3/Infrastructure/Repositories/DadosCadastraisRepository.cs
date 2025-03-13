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
        var database = mongoClient.GetDatabase("Project");

        _usuarios = database.GetCollection<Usuario>("t_usuario");
        _enderecos = database.GetCollection<Endereco>("t_endereco");
        _diasPreferencia = database.GetCollection<DiasPreferencia>("t_dias_preferencia");
        _turnos = database.GetCollection<Turno>("t_turno");
        _horarios = database.GetCollection<Horarios>("t_horario_preferencia");
    }

    public Usuario ObterUsuarioId(string usuarioId)
    {
        var usuario = _usuarios.Find(u => u.Id == usuarioId).FirstOrDefault();
        Console.WriteLine(usuario != null ? $"Usuário encontrado: {usuario.Nome}" : "Usuário não encontrado.");
        return _usuarios.Find(u => u.Id == usuarioId).FirstOrDefault();
    }

    public Endereco ObterEnderecoId(string usuarioId)
    {
        var endereco = _enderecos.Find(e => e.IdUsuario == usuarioId).FirstOrDefault();
        Console.WriteLine(endereco != null ? $"Endereço encontrado para usuário {usuarioId}" : "Endereço não encontrado.");
        return _enderecos.Find(e => e.IdUsuario == usuarioId).FirstOrDefault();
    }

    public List<DiasPreferencia> ObterDiasId(string usuarioId)
    {
        var dias = _diasPreferencia.Find(dp => dp.IdUsuario == usuarioId).ToList();
        Console.WriteLine(dias.Any() ? $"Dias de preferência encontrados para usuário {usuarioId}" : "Nenhum dia de preferência encontrado.");
        return _diasPreferencia.Find(dp => dp.IdUsuario == usuarioId).ToList();
    }

    public List<Turno> ObterTurnosId(string usuarioId)
    {
        var turnos = _turnos.Find(t => t.IdUsuario == usuarioId).ToList();
        Console.WriteLine(turnos.Any() ? $"Turnos encontrados para usuário {usuarioId}" : "Nenhum turno encontrado.");
        return _turnos.Find(t => t.IdUsuario == usuarioId).ToList();
    }

    public List<Horarios> ObterHorariosId(string usuarioId)
    {
        var horarios = _horarios.Find(h => h.IdUsuario == usuarioId).ToList();
        Console.WriteLine(horarios.Any() ? $"Horários encontrados para usuário {usuarioId}" : "Nenhum horário encontrado.");
        return _horarios.Find(h => h.IdUsuario == usuarioId).ToList();
    }
}
