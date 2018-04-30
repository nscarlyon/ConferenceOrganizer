using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ConferenceOrganizer.Data
{
    public class MongoSchedule
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public bool Published { get; set; }
        public List<string> Rooms { get; set; }
        public List<MongoTimeSlot> TimeSlots { get; set; }
    }
}