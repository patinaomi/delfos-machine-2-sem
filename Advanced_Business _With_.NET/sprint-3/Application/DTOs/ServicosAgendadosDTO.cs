using System;
using System.ComponentModel.DataAnnotations;

namespace Project.DTOs
{
    public class ServicosAgendadosDTO
    {
        public string? Id { get; set; }
        
        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public string? IdUsuario { get; set; }
        
        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do cliente deve ter no máximo 100 caracteres.")]
        public string? NomeCliente { get; set; }
        
        [Required(ErrorMessage = "A data é obrigatória.")]
        public DateOnly Data { get; set; }
        
        [Required(ErrorMessage = "O turno é obrigatório.")]
        [RegularExpression("^(Manhã|Tarde|Noite)$", ErrorMessage = "O turno deve ser Manhã, Tarde ou Noite.")]
        public string? Turno { get; set; }
        
        [Required(ErrorMessage = "O horário é obrigatório.")]
        [RegularExpression("^([01]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "O horário deve estar no formato HH:mm.")]
        public string? Horario { get; set; }
        
        [Required(ErrorMessage = "O ID da clínica é obrigatório.")]
        public string? IdClinica { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "O nome da clínica deve ter no máximo 100 caracteres.")]
        public string? NomeClinica { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "O nome do médico deve ter no máximo 100 caracteres.")]
        public string? NomeMedico { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "A especialidade deve ter no máximo 100 caracteres.")]
        public string? Especialidade { get; set; }
        
        [Required]
        public string? CEPClinica { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "O estado deve ter no máximo 50 caracteres.")]
        public string? EstadoClinica { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "A cidade deve ter no máximo 100 caracteres.")]
        public string? CidadeClinica { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "O bairro deve ter no máximo 100 caracteres.")]
        public string? BairroClinica { get; set; }
        
        [Required]
        [StringLength(150, ErrorMessage = "A rua deve ter no máximo 150 caracteres.")]
        public string? RuaClinica { get; set; }
    }
}
