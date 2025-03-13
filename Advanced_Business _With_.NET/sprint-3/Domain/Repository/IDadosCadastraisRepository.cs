using Project.Models;

public interface IDadosCadastraisRepository
{
    Usuario ObterUsuarioId(string usuarioId);
    Endereco ObterEnderecoId(string usuarioId);
    List<DiasPreferencia> ObterDiasId(string usuarioId);
    List<Turno> ObterTurnosId(string usuarioId);
    List<Horarios> ObterHorariosId(string usuarioId);
}
