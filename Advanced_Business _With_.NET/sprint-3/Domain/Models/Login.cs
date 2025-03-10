
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project.Models
{
    public class Login
    {   
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdUsuario { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string Data { get; set; } = string.Empty;
        public string Hora { get; set; } = string.Empty;

        private DateTime ConvertToSaoPauloTime(DateTime utcDateTime)
        {
            TimeZoneInfo saoPauloTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, saoPauloTimeZone);
        }

        public Login()
        {
            var saoPauloTime = ConvertToSaoPauloTime(DateTime.UtcNow);
            Data = saoPauloTime.ToString("yyyy-MM-dd");
            Hora = saoPauloTime.ToString("HH:mm:ss");
        }


    }
}