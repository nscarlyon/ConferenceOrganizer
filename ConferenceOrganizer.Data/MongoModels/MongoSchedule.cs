using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ConferenceOrganizer.Data.MongoModels
{
    public class MongoSchedule
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public List<string> Rooms { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }
        public bool Published { get; set; }
    }

    public class TimeSlot
    {
        public string StandardTime { get; set; }
        public int StartHour { get; set; }
        public int StartMin { get; set; }
        public int EndHour { get; set; }
        public int EndMin { get; set; }
    }
}
