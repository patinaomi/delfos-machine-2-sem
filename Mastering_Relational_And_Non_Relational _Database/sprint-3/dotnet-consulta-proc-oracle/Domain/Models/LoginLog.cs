using System.ComponentModel.DataAnnotations;

namespace LexusTech.Models
{
    public class LoginLog
    {
        [Key]
        public int IdLogin { get; set; }
        public int IdUsuario { get; set; }
        public string? Email { get; set; }
        public DateTime DataHora { get; set; }
    }
}