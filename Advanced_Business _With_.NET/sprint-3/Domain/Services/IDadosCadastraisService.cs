using Project.Models;

public interface IDadosCadastraisService
{
    DadosCadastraisDTO ObterDadosCadastraisPorUsuarioId(string usuarioId);
}
