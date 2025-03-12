
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project.Models;

    public class Campanha
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Atividade { get; set; }
        public string? Descricao { get; set; }
        public string? Pontuacao { get; set; } // Será 5,10,15 ou 20
        public string? Perfil { get; set; } // Será Clinica ou Cliente
        public string? IdUsuario { get; set; }
        public string? Status { get; set; } = "Pendente"; // Nasce como pendente e depois virá Concluída se o cliente realizar a ação.
        public string? DataCriacao { get; set; }
        public string? DataConclusao { get; set; }

    }