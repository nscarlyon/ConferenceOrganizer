using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceOrganizer.Data
{
    public class Schedule
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public IEnumerable<string> rooms { get; set; }
        public IEnumerable<TimeSlot> timeSlots { get; set; }
    }

    public class TimeSlot
    {
        public string timeSlot { get; set; }
        public IEnumerable<Session> sessions { get; set; }

        public void AddSession(Session session)
        {
            sessions.ToList().Add(session);
        }
    }
}