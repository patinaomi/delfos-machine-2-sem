using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project.Models
{
    public class CampanhaDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "O campo Atividade é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Atividade deve ter no máximo 100 caracteres.")]
        public string? Atividade { get; set; }

        [Required(ErrorMessage = "O campo Descricao é obrigatório.")]
        [StringLength(500, ErrorMessage = "O campo Descricao deve ter no máximo 500 caracteres.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O campo Pontuacao é obrigatório.")]
        [RegularExpression("5|10|15|20", ErrorMessage = "O campo Pontuacao deve ser 5, 10, 15 ou 20.")]
        public string? Pontuacao { get; set; }

        [Required(ErrorMessage = "O campo Perfil é obrigatório.")]
        [RegularExpression("Clinica|Cliente", ErrorMessage = "O campo Perfil deve ser 'Clinica' ou 'Cliente'.")]
        public string? Perfil { get; set; }

        [Required(ErrorMessage = "O campo IdUsuario é obrigatório.")]
        public string? IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo Status é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo Status deve ter no máximo 50 caracteres.")]
        public string? Status { get; set; } = "Pendente";

        [Required(ErrorMessage = "O campo DataCriacao é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "O campo DataCriacao deve ser uma data válida.")]
        public DateOnly DataCriacao { get; set; }

        [DataType(DataType.Date, ErrorMessage = "O campo DataConclusao deve ser uma data válida.")]
        public DateOnly DataConclusao { get; set; }
    }
}