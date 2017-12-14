using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConferenceOrganizer.Data
{
    public class Proposal
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string speakerName { get; set; }
        public string bio { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string email { get; set; }
    }

}