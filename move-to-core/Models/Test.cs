using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MoveToCore.Models
{
    public class Test
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Message { get; set; }
        public string Comments { get; set; }
    }
}
