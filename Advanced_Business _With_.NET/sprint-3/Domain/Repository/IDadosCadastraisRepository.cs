using Project.Models;

public interface IDadosCadastraisRepository
{
    Usuario ObterUsuarioPorId(string usuarioId);
    Endereco ObterEnderecoPorUsuarioId(string usuarioId);
    List<DiasPreferencia> ObterDiasPreferenciaPorUsuarioId(string usuarioId);
    List<Turno> ObterTurnosPorUsuarioId(string usuarioId);
    List<Horarios> ObterHorariosPorUsuarioId(string usuarioId);
}
