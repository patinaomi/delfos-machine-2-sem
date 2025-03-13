using MongoDB.Driver;
using Project.Models;

namespace Project.Infrastructure
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDbConnection");
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("Project");
        }

        public IMongoCollection<Usuario> Usuarios => _database.GetCollection<Usuario>("t_usuario");
        public IMongoCollection<Login> Login => _database.GetCollection<Login>("t_login");
        public IMongoCollection<Endereco> Endereco => _database.GetCollection<Endereco>("t_endereco");
        public IMongoCollection<DiasPreferencia> DiasPreferencia => _database.GetCollection<DiasPreferencia>("t_dias_preferencia");
        public IMongoCollection<Turno> Turno => _database.GetCollection<Turno>("t_turno");
        public IMongoCollection<Horarios> Horarios => _database.GetCollection<Horarios>("t_horario_preferencia");
        public IMongoCollection<Clinica> Clinica => _database.GetCollection<Clinica>("t_clinica");
        public IMongoCollection<Medico> Medico => _database.GetCollection<Medico>("t_medico");
        public IMongoCollection<SugestaoConsultaClinica> SugestaoConsultaClinica => _database.GetCollection<SugestaoConsultaClinica>("t_sugestao_consulta_clinica");
        public IMongoCollection<SugestaoConsultaCliente> SugestaoConsultaCliente => _database.GetCollection<SugestaoConsultaCliente>("t_sugestao_consulta_cliente");
        public IMongoCollection<MotivoRecusa> MotivoRecusa => _database.GetCollection<MotivoRecusa>("t_motivo_recusa");
        public IMongoCollection<ServicosAgendados> ServicosAgendados => _database.GetCollection<ServicosAgendados>("t_servicos_agendados");
        public IMongoCollection<Consulta> Consulta => _database.GetCollection<Consulta>("t_consulta");
        public IMongoCollection<Feedback> Feedback => _database.GetCollection<Feedback>("t_feedback");
        public IMongoCollection<Campanha> Campanha => _database.GetCollection<Campanha>("t_campanha");
        public IMongoCollection<Chat> Chat => _database.GetCollection<Chat>("t_chat");
    }
}
