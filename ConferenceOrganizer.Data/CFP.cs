using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConferenceOrganizer.Data
{
    public class CFP
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string status { get; set; }
    }
}