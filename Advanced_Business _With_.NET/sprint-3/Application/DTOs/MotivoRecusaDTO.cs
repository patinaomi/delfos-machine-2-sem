    using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Project.Models;

    public class MotivoRecusaDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "O motivo é obrigatório")]
        [StringLength(100, ErrorMessage = "O motivo deve ter no máximo 50 caracteres")]
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú\s]+$", ErrorMessage = "O Nome deve conter apenas letras.")]
        public string? Motivo { get; set; }

        [Required(ErrorMessage = "A Descriçãp é obrigatória")]
        [StringLength(100, ErrorMessage = "A Descriçãp deve ter no máximo 50 caracteres")]
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú\s]+$", ErrorMessage = "A Descriçãp deve conter apenas letras.")]
        public string? Descricao { get; set; }

    }