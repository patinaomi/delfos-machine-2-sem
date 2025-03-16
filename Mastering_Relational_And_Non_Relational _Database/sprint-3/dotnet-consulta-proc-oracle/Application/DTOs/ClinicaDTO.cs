    using System.ComponentModel.DataAnnotations;
    namespace LexusTech.Models;

    public class ClinicaDTO
    {
        [Key]
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Endereco { get; set; }

        public string? Telefone { get; set; }

        public string? Avaliacao { get; set; }

        public string? PrecoMedio { get; set; }

    }