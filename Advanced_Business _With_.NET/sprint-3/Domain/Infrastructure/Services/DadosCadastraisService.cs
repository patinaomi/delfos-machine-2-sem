using Project.Models;

public class DadosCadastraisService : IDadosCadastraisService
{
    private readonly IDadosCadastraisRepository _dadosCadastraisRepository;

    public DadosCadastraisService(IDadosCadastraisRepository dadosCadastraisRepository)
    {
        _dadosCadastraisRepository = dadosCadastraisRepository;
    }

    public DadosCadastraisDto ObterDadosCadastraisPorUsuarioId(string userId)
    {
        var usuario = _dadosCadastraisRepository.ObterUsuarioPorId(userId);
        var endereco = _dadosCadastraisRepository.ObterEnderecoPorUsuarioId(userId);
        var diasPreferencia = _dadosCadastraisRepository.ObterDiasPreferenciaPorUsuarioId(userId);
        var turnos = _dadosCadastraisRepository.ObterTurnosPorUsuarioId(userId);
        var horarios = _dadosCadastraisRepository.ObterHorariosPorUsuarioId(userId);

        return new DadosCadastraisDto
        {
            Usuario = new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
                // Outros campos do UsuarioDTO
            },
            Endereco = new EnderecoDTO
            {
                Rua = endereco.Rua,
                CEP = endereco.CEP,
                Cidade = endereco.Cidade,
                // Outros campos do EnderecoDTO
            },
            DiasPreferencia = diasPreferencia.Select(dp => new DiasPreferenciaDTO
            {
                DiasSemana = dp.DiasSemana
            }).ToList(),
            TurnosPreferencia = turnos.Select(t => new TurnoDTO
            {
                TurnoPreferencia = t.TurnoPreferencia
            }).ToList(),
            HorariosPreferencia = horarios.Select(h => new HorariosDTO
            {
                HorariosPreferencia = h.HorariosPreferencia
            }).ToList()
        };
    }
}
