using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConferenceOrganizer.Data
{
    public class Session
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string SpeakerName { get; set; }
        public string Title { get; set; }
        public string Room { get; set; }
        public string StandardTime { get; set; }
    }
}