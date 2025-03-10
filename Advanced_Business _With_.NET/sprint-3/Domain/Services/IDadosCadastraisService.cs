using Project.Models;

public interface IDadosCadastraisService
{
    DadosCadastraisDto ObterDadosCadastraisPorUsuarioId(string usuarioId);
}
