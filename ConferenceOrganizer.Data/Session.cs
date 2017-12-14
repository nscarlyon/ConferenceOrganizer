using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConferenceOrganizer.Data
{
    public class Session
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string speakerName { get; set; }
        public string presentationTitle { get; set; }
        public string roomName { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}