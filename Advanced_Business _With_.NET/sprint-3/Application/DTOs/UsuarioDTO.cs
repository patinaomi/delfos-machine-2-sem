    using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Project.Models;

    public class UsuarioDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú\s]+$", ErrorMessage = "O Nome deve conter apenas letras.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [StringLength(11, ErrorMessage = "O CPF deve ter no máximo 11 caracteres")]
        public string? CPF { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        [Phone(ErrorMessage = "Telefone inválido")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{6,}$", 
        ErrorMessage = "A senha deve conter no mínimo 6 caracteres, pelo menos uma letra maiúscula e um dígito.")]

        public string? Senha { get; set; }

        public string Perfil { get; set; } = "Comum";

    }