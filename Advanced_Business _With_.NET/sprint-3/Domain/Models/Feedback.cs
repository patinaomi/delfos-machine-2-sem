

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project.Models;

    public class Feedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Nota { get; set; }
        public string? Comentario { get; set; }

    }