using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project.Models
{
    public class ChatDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "O Id do usuário é obrigatório.")]
        public string? IdUsuario { get; set; }

        [Required(ErrorMessage = "A pergunta é obrigatória.")]
        [StringLength(500, ErrorMessage = "A pergunta deve ter no máximo 500 caracteres.")]
        public string? Pergunta { get; set; }

        [Required(ErrorMessage = "A resposta é obrigatória.")]
        [StringLength(1000, ErrorMessage = "A resposta deve ter no máximo 1000 caracteres.")]
        public string? Resposta { get; set; }

        [Required(ErrorMessage = "A data é obrigatória.")]
        public DateTime Data { get; set; }
    }
}
