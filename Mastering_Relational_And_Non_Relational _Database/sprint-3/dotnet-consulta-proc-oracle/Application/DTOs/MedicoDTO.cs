    using System.ComponentModel.DataAnnotations;
    namespace LexusTech.Models;

    public class MedicoDTO
    {
        [Key]
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Sobrenome { get; set; }

        public string? Telefone { get; set; }

        public string? IdClinica { get; set; }

        public string? IdEspecialidade { get; set; }
        public string? Avaliacao { get; set; }

    }