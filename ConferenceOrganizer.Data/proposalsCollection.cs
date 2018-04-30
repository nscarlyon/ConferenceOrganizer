using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceOrganizer.Data
{
    public class ProposalsCollection : IProposalsCollection
    {
        IMongoCollection<MongoProposal> collection;
        IMongoDatabase database;

        public ProposalsCollection()
        {
            database = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("conferenceOrganizer");
            collection = database.GetCollection<MongoProposal>("proposals");
        }

        public IEnumerable<MongoProposal> GetProposals()
        {
            return collection.Find(x => true).ToListAsync().Result;
        }

        public MongoProposal FindProposal(string id)
        {
            return collection.Find(x => x.id == id).FirstOrDefault();
        }

        public void PostProposal(MongoProposal proposal)
        {
            collection.InsertOne(proposal);
        }

        public void UpdateProposal(MongoProposal proposal)
        {
            var filter = Builders<MongoProposal>.Filter.Eq("id", proposal.id);
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
