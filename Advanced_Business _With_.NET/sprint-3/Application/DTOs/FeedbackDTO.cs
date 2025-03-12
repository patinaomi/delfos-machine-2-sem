

using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project.Models;

    public class FeedbackDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "A Nota é obrigatória e de 1 até 5")]
        public string? Nota { get; set; }

        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string? Comentario { get; set; }

    }