using MongoDB.Driver;
using System.Collections.Generic;

namespace ConferenceOrganizer.Data
{
    public interface IConferenceOrganizerDatabase
    {
        string GetCFPStatus();
        IEnumerable<Proposal> GetProposals();
        Proposal FindProposal(string id);
        void PostProposal(Proposal proposal);
        void PutProposal(string id, Proposal proposal);
        void DeleteProposal(string id);
    }

    public class ConferenceOrganizerDatabase : IConferenceOrganizerDatabase
    {
        IMongoCollection<Proposal> collection;
        IMongoDatabase database;

        public ConferenceOrganizerDatabase()
        {
            database = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("conferenceOrganizer");
            collection = database.GetCollection<Proposal>("proposals");
        }

        public string GetCFPStatus()
        {
            var cfpCollection = database.GetCollection<CFP>("cfp");
            return cfpCollection.Find(x => true).ToListAsync().Result[0].status;
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
}
