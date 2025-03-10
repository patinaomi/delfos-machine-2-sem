
namespace Project.Models;

public class DadosCadastrais
{
    public required Usuario Usuario { get; set; } = new Usuario();
    public required Endereco Endereco { get; set; } = new Endereco();
    public required List<DiasPreferencia> DiasPreferencia { get; set; } = new List<DiasPreferencia>();
    public required Turno Turno { get; set; }  = new Turno();
    public required Horarios Horarios { get; set; }  = new Horarios();
    
}