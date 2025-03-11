using Project.Models;

public class DadosCadastraisDTO
{
    public required UsuarioDTO Usuario { get; set; }
    public required EnderecoDTO Endereco { get; set; }
    public required List<DiasPreferenciaDTO> DiasPreferencia { get; set; }
    public required List<TurnoDTO> TurnosPreferencia { get; set; }
    public required List<HorariosDTO> HorariosPreferencia { get; set; }
}
