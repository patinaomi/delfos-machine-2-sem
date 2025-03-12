using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project.DTOs
{
    public class ConsultaDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public string? IdUsuario { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do cliente deve ter no máximo 100 caracteres.")]
        public string? NomeCliente { get; set; }

        [Required(ErrorMessage = "O CPF do cliente é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter 11 dígitos numéricos.")]
        public string? CPFCliente { get; set; }

        [Required(ErrorMessage = "A data da consulta é obrigatória.")]
        public DateOnly Data { get; set; }

        [Required(ErrorMessage = "O turno é obrigatório.")]
        [RegularExpression(@"^(Manhã|Tarde|Noite)$", ErrorMessage = "O turno deve ser 'Manhã', 'Tarde' ou 'Noite'.")]
        public string? Turno { get; set; }

        [Required(ErrorMessage = "O horário é obrigatório.")]
        [RegularExpression(@"^([01]\d|2[0-3]):([0-5]\d)$", ErrorMessage = "O horário deve estar no formato HH:mm.")]
        public string? Horario { get; set; }

        [Required(ErrorMessage = "O ID da clínica é obrigatório.")]
        public string? IdClinica { get; set; }

        [Required(ErrorMessage = "O nome da clínica é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da clínica deve ter no máximo 100 caracteres.")]
        public string? NomeClinica { get; set; }

        [Required(ErrorMessage = "O CNPJ da clínica é obrigatório.")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "O CNPJ deve conter 14 dígitos numéricos.")]
        public string? CNPJClinica { get; set; }

        [Required(ErrorMessage = "O nome do médico é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do médico deve ter no máximo 100 caracteres.")]
        public string? NomeMedico { get; set; }

        [Required(ErrorMessage = "A especialidade é obrigatória.")]
        public string? Especialidade { get; set; }

        [Required(ErrorMessage = "O CEP da clínica é obrigatório.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O CEP deve estar no formato 00000-000.")]
        public string? CEPClinica { get; set; }

        [Required(ErrorMessage = "O estado da clínica é obrigatório.")]
        [StringLength(2, ErrorMessage = "O estado deve conter 2 caracteres.")]
        public string? EstadoClinica { get; set; }

        [Required(ErrorMessage = "A cidade da clínica é obrigatória.")]
        public string? CidadeClinica { get; set; }

        [Required(ErrorMessage = "O bairro da clínica é obrigatório.")]
        public string? BairroClinica { get; set; }

        [Required(ErrorMessage = "A rua da clínica é obrigatória.")]
        public string? RuaClinica { get; set; }

        public string? StatusConsulta { get; set; } = "Encerrado";

        public string? RespostaFeedback { get; set; } = "Sem resposta";
    }
}
