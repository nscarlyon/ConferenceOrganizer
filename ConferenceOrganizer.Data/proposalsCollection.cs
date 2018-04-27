using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceOrganizer.Data
{
    public class ProposalsCollection : IProposalsCollection
    {
        IMongoCollection<Proposal> collection;
        IMongoDatabase database;

        public ProposalsCollection()
        {
            database = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("conferenceOrganizer");
            collection = database.GetCollection<Proposal>("proposals");
        }

        public IEnumerable<Proposal> GetProposals()
        {
            return collection.Find(x => true).ToListAsync().Result;
        }

        public Proposal FindProposal(string id)
        {
            return collection.Find(x => x.id == id).FirstOrDefault();
        }

        public void PostProposal(Proposal proposal)
        {
            collection.InsertOne(proposal);
        }

        public void UpdateProposal(Proposal proposal)
        {
            var filter = Builders<Proposal>.Filter.Eq("id", proposal.id);
            collection.ReplaceOne(filter, proposal);
        }

        public void DeleteProposal(string id)
        {
            collection.DeleteOne(X => X.id == id);
        }

        public void DeleteProposals()
        {
            collection.DeleteMany(X => true);
        }
    }
}
