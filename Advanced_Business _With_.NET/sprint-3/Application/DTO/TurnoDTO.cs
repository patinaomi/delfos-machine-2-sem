    using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Project.Models;

    public class TurnoDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdUsuario { get; set; }

        [Required(ErrorMessage = "Um dia é obrigatório")]
        public string? TurnoPreferencia { get; set; }

    }