using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConferenceOrganizer.Data
{
    public interface IConferenceOrganizerDatabase
    {
        IEnumerable<Proposal> GetProposals();
        Proposal FindProposal(string id);
        void PostProposal(Proposal proposal);
        void PutProposal(string id, Proposal proposal);
        void DeleteProposal(string id);
    }

    public class ConferenceOrganizerDatabase : IConferenceOrganizerDatabase
    {
        IMongoCollection<Proposal> collection;

        public ConferenceOrganizerDatabase()
        {
            IMongoClient client = new MongoClient("mongodb://127.0.0.1:27017");
            collection = client.GetDatabase("conferenceOrganizer").GetCollection<Proposal>("proposals");
        }

        public IEnumerable<Proposal> GetProposals()
        {
            return collection.Find(x => true).ToListAsync().Result;
        }

        public Proposal FindProposal(string id)
        {
            return collection.Find(x => x.id == id).First();
        }

        public void PostProposal(Proposal proposal)
        {
            collection.InsertOne(proposal);
        }

        public void PutProposal(string id, Proposal proposal)
        {
            var filter = Builders<Proposal>.Filter.Eq("id", id);
            collection.FindOneAndReplace(filter, proposal);
        }

        public void DeleteProposal(string id)
        {
            collection.DeleteOne(X=> X.id == id);
        }
    }

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
