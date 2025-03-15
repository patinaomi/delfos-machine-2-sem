
using System.ComponentModel.DataAnnotations;

namespace LexusTech.Models
{
    public class Login
    {   
        [Key]
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }

    }
}