    namespace Project.Domain
    {
        public class ConfigMongoDb
        {
            public string ConnectionString { get; set; } = null!;
            public string DatabaseName { get; set; } = null!;
            public string UsuarioCollectionName { get; set; } = null!;
            public string LoginCollectionName { get; set; } = null!;
            public string EnderecoCollectionName { get; set; } = null!;
            public string DiasPreferenciaCollectionName { get; set; } = null!;
            public string TurnoCollectionName { get; set; } = null!;
            public string HorariosCollectionName { get; set; } = null!;
            public string ClinicaCollectionName { get; set; } = null!;
            public string MedicoCollectionName { get; set; } = null!;
            public string SugestaoConsultaClinicaCollectionName { get; set; } = null!;
            public string SugestaoConsultaClienteCollectionName { get; set; } = null!;
            public string MotivoRecusaCollectionName { get; set; } = null!;

        }
    }