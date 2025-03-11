    using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Project.Models;

    public class EnderecoDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdUsuario { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório")]
        [StringLength(8, ErrorMessage = "O campo deve ter no máximo 8 caracteres, sem o simbolo -")]
        [RegularExpression(@"^\d+$", ErrorMessage = "O campo deve conter apenas números.")]
        public string? CEP { get; set; }

        [Required(ErrorMessage = "O Estado é obrigatório")]
        [StringLength(100, ErrorMessage = "O estado deve ter no máximo 50 caracteres")]
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú\s]+$", ErrorMessage = "O Sobrenome deve conter apenas letras.")]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória")]
        public string? Cidade { get; set; }

        [Required(ErrorMessage = "O bairro é obrigatória")]
        public string? Bairro { get; set; }
     
      [ Required(ErrorMessage = "A rua é obrigatória")]
        public string? Rua { get; set; }
     

    

    }