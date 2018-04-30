using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ConferenceOrganizer.Data
{
    public class Proposal
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public List<ScheduledTime> scheduledTimes {get; set;}
        public string speakerName { get; set; }
        public string bio { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string email { get; set; }
    }

    public class ScheduledTime
    {
        public string room { get; set; }
        public string standardTime { get; set; }
    }
}