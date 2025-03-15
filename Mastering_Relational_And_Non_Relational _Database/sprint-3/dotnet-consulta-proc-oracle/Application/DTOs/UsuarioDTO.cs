    using System.ComponentModel.DataAnnotations;
    namespace LexusTech.Models;

    public class UsuarioDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú\s]+$", ErrorMessage = "O Nome deve conter apenas letras.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O sobrenome é obrigatório")]
        [StringLength(100, ErrorMessage = "O sobrenome deve ter no máximo 50 caracteres")]
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú\s]+$", ErrorMessage = "O Sobrenome deve conter apenas letras.")]
        public string? Sobrenome { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        [Phone(ErrorMessage = "Telefone inválido")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string? Senha { get; set; }
        public DateTime DataNasc { get; set; }
        public string? Endereco { get; set; }

    }