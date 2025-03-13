using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project.Models
{
    public class SugestaoConsultaClienteDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [Required(ErrorMessage = "O campo 'IdClinica' é obrigatório.")]
        [StringLength(50, ErrorMessage = "O 'IdClinica' deve ter no máximo 50 caracteres.")]
        public string? IdClinica { get; set; }

        [Required(ErrorMessage = "O campo 'NomeClinica' é obrigatório.")]
        [StringLength(100, ErrorMessage = "O 'NomeClinica' deve ter no máximo 100 caracteres.")]
        public string? NomeClinica { get; set; }

        [Required(ErrorMessage = "O campo 'TelefoneClinica' é obrigatório.")]
        [Phone(ErrorMessage = "O 'TelefoneClinica' deve ser um número de telefone válido.")]
        public string? TelefoneClinica { get; set; }

        [Required(ErrorMessage = "O campo 'EmailClinica' é obrigatório.")]
        [EmailAddress(ErrorMessage = "O 'EmailClinica' deve ser um e-mail válido.")]
        [StringLength(100, ErrorMessage = "O 'EmailClinica' deve ter no máximo 100 caracteres.")]
        public string? EmailClinica { get; set; }

        [Required(ErrorMessage = "O campo 'IdUsuario' é obrigatório.")]
        [StringLength(50, ErrorMessage = "O 'IdUsuario' deve ter no máximo 50 caracteres.")]
        public string? IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        [StringLength(100, ErrorMessage = "O 'Nome' deve ter no máximo 100 caracteres.")]
        public string? NomeCliente { get; set; }

        public string? CPFCliente { get; set; }

        [Required(ErrorMessage = "O campo 'Telefone' é obrigatório.")]
        [Phone(ErrorMessage = "O 'Telefone' deve ser um número de telefone válido.")]
        public string? TelefoneCliente { get; set; }

        [Required(ErrorMessage = "O campo 'Email' é obrigatório.")]
        [EmailAddress(ErrorMessage = "O 'Email' deve ser um e-mail válido.")]
        [StringLength(100, ErrorMessage = "O 'Email' deve ter no máximo 100 caracteres.")]
        public string? EmailCliente { get; set; }

        [Required(ErrorMessage = "O campo 'Data' é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "O campo 'Data' deve ser uma data válida.")]
        public string? DiaPreferenciaCliente { get; set; }

        [Required(ErrorMessage = "O campo 'Turno' é obrigatório.")]
        [StringLength(20, ErrorMessage = "O 'Turno' deve ter no máximo 20 caracteres.")]
        public string? TurnoPreferenciaCliente { get; set; }

        [Required(ErrorMessage = "O campo 'Horario' é obrigatório.")]
        [StringLength(20, ErrorMessage = "O 'Horario' deve ter no máximo 20 caracteres.")]
        public string? HorarioPreferenciaCliente { get; set; }

        [Required(ErrorMessage = "O campo 'Especilidade' é obrigatório.")]
        [StringLength(100, ErrorMessage = "A 'Especialidade' deve ter no máximo 100 caracteres.")]
        public string? Especialidade { get; set; }

        [Required(ErrorMessage = "O campo 'CEPClinica' é obrigatório.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "O 'CEPClinica' deve ter exatamente 8 caracteres.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "O 'CEPClinica' deve ser um número de 8 dígitos.")]
        public string? CEPClinica { get; set; }

        [Required(ErrorMessage = "O campo 'EstadoClinica' é obrigatório.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "O 'EstadoClinica' deve ter 2 caracteres.")]
        public string? EstadoClinica { get; set; }

        [Required(ErrorMessage = "O campo 'CidadeClinica' é obrigatório.")]
        [StringLength(100, ErrorMessage = "A 'CidadeClinica' deve ter no máximo 100 caracteres.")]
        public string? CidadeClinica { get; set; }

        [Required(ErrorMessage = "O campo 'BairroClinica' é obrigatório.")]
        [StringLength(100, ErrorMessage = "O 'BairroClinica' deve ter no máximo 100 caracteres.")]
        public string? BairroClinica { get; set; }

        [Required(ErrorMessage = "O campo 'RuaClinica' é obrigatório.")]
        [StringLength(100, ErrorMessage = "A 'RuaClinica' deve ter no máximo 100 caracteres.")]
        public string? RuaClinica { get; set; }

        [Required(ErrorMessage = "O campo 'StatusSugestaoCliente' é obrigatório.")]
        [StringLength(50, ErrorMessage = "O 'StatusSugestaoCliente' deve ter no máximo 50 caracteres.")]
        public string? StatusSugestaoCliente { get; set; } = "Sem resposta";
        public DateTime DataAlteracao { get; set; }
        public DateOnly DataConsulta { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime HoraConsulta { get; set; }
        public string? NomeMedico { get; set; }
        public string? CNPJClinica { get; set; }
    }
}
