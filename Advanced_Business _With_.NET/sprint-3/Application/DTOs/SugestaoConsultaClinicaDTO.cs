using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project.Models
{
    public class SugestaoConsultaClinicaDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [Required(ErrorMessage = "O campo 'IdUsuario' é obrigatório.")]
        [StringLength(50, ErrorMessage = "O 'IdUsuario' deve ter no máximo 50 caracteres.")]
        public string? IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        [StringLength(100, ErrorMessage = "O 'Nome' deve ter no máximo 100 caracteres.")]
        public string? NomeCliente { get; set; }

        [Required(ErrorMessage = "O campo 'Telefone' é obrigatório.")]
        [Phone(ErrorMessage = "O campo 'Telefone' deve ser um número de telefone válido.")]
        public string? TelefoneCliente { get; set; }

        [Required(ErrorMessage = "O campo 'Email' é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo 'Email' deve ser um e-mail válido.")]
        [StringLength(100, ErrorMessage = "O 'Email' deve ter no máximo 100 caracteres.")]
        public string? EmailCliente { get; set; }

        [Required(ErrorMessage = "O campo 'DiaPreferenciaCliente' é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "O campo 'DiaPreferenciaCliente' deve ser uma data válida.")]
        public string? DiaPreferenciaCliente { get; set; }

        [Required(ErrorMessage = "O campo 'TurnoPreferenciaCliente' é obrigatório.")]
        [StringLength(20, ErrorMessage = "O 'TurnoPreferenciaCliente' deve ter no máximo 20 caracteres.")]
        public string? TurnoPreferenciaCliente { get; set; }

        [Required(ErrorMessage = "O campo 'HorarioPreferenciaCliente' é obrigatório.")]
        [StringLength(20, ErrorMessage = "O 'HorarioPreferenciaCliente' deve ter no máximo 20 caracteres.")]
        public string? HorarioPreferenciaCliente { get; set; }

        [Required(ErrorMessage = "O campo 'Especilidade' é obrigatório.")]
        [StringLength(100, ErrorMessage = "A 'Especialidade' deve ter no máximo 100 caracteres.")]
        public string? Especialidade { get; set; }

        [Required(ErrorMessage = "O campo 'CEPPreferenciaCliente' é obrigatório.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "O 'CEPPreferenciaCliente' deve ter exatamente 8 caracteres.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "O 'CEPPreferenciaCliente' deve ser um número de 8 dígitos.")]
        public string? CEPPreferenciaCliente { get; set; }

        [Required(ErrorMessage = "O campo 'EstadoPreferenciaCliente' é obrigatório.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "O 'EstadoPreferenciaCliente' deve ter 2 caracteres.")]
        public string? EstadoPreferenciaCliente { get; set; }

        [Required(ErrorMessage = "O campo 'CidadePreferenciaCliente' é obrigatório.")]
        [StringLength(100, ErrorMessage = "A 'CidadePreferenciaCliente' deve ter no máximo 100 caracteres.")]
        public string? CidadePreferenciaCliente { get; set; }

        [Required(ErrorMessage = "O campo 'BairroPreferenciaCliente' é obrigatório.")]
        [StringLength(100, ErrorMessage = "O 'BairroPreferenciaCliente' deve ter no máximo 100 caracteres.")]
        public string? BairroPreferenciaCliente { get; set; }

        [Required(ErrorMessage = "O campo 'RuaPreferenciaCliente' é obrigatório.")]
        [StringLength(100, ErrorMessage = "A 'RuaPreferenciaCliente' deve ter no máximo 100 caracteres.")]
        public string? RuaPreferenciaCliente { get; set; }

        [Required(ErrorMessage = "O campo 'StatusSugestaoClinica' é obrigatório.")]
        [StringLength(50, ErrorMessage = "O 'StatusSugestaoClinica' deve ter no máximo 50 caracteres.")]
        public string? StatusSugestaoClinica { get; set; } = "Sem resposta";

        public DateTime DataAlteracao { get; set; }

        public DateOnly DataConsulta { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime HoraConsulta { get; set; }

        public string? CPFCliente { get; set; }
        public string? NomeMedico { get; set; }
        public string? CNPJClinica { get; set; }
    }
}
