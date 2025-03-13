
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project.Models;

    public class Chat
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? IdUsuario { get; set; }
        public string? Pergunta { get; set; }
        public string? Resposta { get; set; }
        public DateTime Data { get; set; }

    }