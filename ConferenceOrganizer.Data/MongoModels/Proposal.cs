﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ConferenceOrganizer.Data
{
    public class MongoProposal
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string SpeakerName { get; set; }
        public string Bio { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
    }
}