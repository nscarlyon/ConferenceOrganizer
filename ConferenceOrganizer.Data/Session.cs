using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConferenceOrganizer.Data
{
    public class Session
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string speakerName { get; set; }
        public string title { get; set; }
        public string room { get; set; }
        public int startHour { get; set; }
        public int startMin { get; set; }
        public int endHour { get; set; }
        public int endMin { get; set; }
    }
}