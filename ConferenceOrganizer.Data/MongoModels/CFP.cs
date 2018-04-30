using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConferenceOrganizer.Data
{
    public class MongoCFP
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string status { get; set; }
    }
}