using System;
using System.Collections.Generic;
using System.Text;
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

    public class CFP
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string status { get; set; }
    }
}
