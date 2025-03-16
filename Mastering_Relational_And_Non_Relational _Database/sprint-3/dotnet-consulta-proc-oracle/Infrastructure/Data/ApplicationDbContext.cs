
    using LexusTech.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public required DbSet<Usuario> T_Usuario { get; set; } = null!;
        public required DbSet<Clinica> T_Clinica { get; set; } = null!;
        public required DbSet<Medico> T_Medico { get; set; } = null!;


    }