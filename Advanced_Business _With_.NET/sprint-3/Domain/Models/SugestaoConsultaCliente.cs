
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project.Models;

    public class SugestaoConsultaCliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? IdClinica { get; set; }
        public string? NomeClinica { get; set; }
        public string? CNPJClinica { get; set; }
        public string? TelefoneClinica { get; set; }
        public string? EmailClinica { get; set; }
        public string? NomeMedico { get; set; }
        public string? IdUsuario { get; set; }
        public string? NomeCliente { get; set; }
        public string? CPFCliente { get; set; }
        public string? TelefoneCliente { get; set; }
        public string? EmailCliente { get; set; }
        public string? DiaPreferenciaCliente { get; set; }
        public string? TurnoPreferenciaCliente { get; set; }
        public string? HorarioPreferenciaCliente { get; set; }
        public string? Especialidade { get; set; }
        public string? CEPClinica { get; set; }
        public string? EstadoClinica { get; set; }
        public string? CidadeClinica { get; set; }
        public string? BairroClinica { get; set; }
        public string? RuaClinica { get; set; }
        //public string? StatusSugestaoClinica { get; set; } = "Sem resposta";
        public string? StatusSugestaoCliente { get; set; } = "Sem resposta";
        public DateTime DataAlteracao { get; set; } // Precisa ser com base no horario e formato de são paulo exemplo 11/03/2025. Com mesmo horário de aceite pois não pode ter as três horas de diferença.

        public DateOnly DataConsulta { get; set; }
        public TimeOnly HoraConsulta { get; set; }

    }