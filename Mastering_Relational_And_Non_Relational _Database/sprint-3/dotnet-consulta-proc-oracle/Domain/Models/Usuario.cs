  
    namespace LexusTech.Models;

    public class Usuario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public DateTime DataNasc { get; set; }
        public string? Endereco { get; set; }

    }