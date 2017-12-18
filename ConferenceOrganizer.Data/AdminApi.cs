using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceOrganizer.Data
{
    public class AdminApi : IAdminApi
    {
        IMongoCollection<Session> collection;
        IMongoDatabase database;

        public AdminApi()
        {
            database = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("conferenceOrganizer");
            collection = database.GetCollection<Session>("sessions");
        }

        public void PutCFP(string id, CFP cfp)
        {
            var cfpCollection = database.GetCollection<CFP>("cfp");
            var filter = Builders<CFP>.Filter.Eq("id", id);
            cfpCollection.FindOneAndReplace(filter, cfp);
        }

        public CFP GetCFPStatus()
        {
            var cfpCollection = database.GetCollection<CFP>("cfp");
            return cfpCollection.Find(x => true).ToListAsync().Result[0];
        }

        public IEnumerable<string> GetSpeakers()
        {
            var proposalsCollection = database.GetCollection<Proposal>("proposals");
            var result = proposalsCollection.AsQueryable<Proposal>()
                                   .Select(p => p.speakerName)
                                   .Distinct();
            return result;
        }

        public IEnumerable<Session> GetSessions()
        {
            return collection.Find(x => true).ToListAsync().Result;
        }

        public Session GetSession(string id)
        {
            return collection.Find(x => x.id == id).First();
        }

        public void PostSession(Session session)
        {
            collection.InsertOne(session);
        }

        public void PutSession(string id, Session session)
        {
            var filter = Builders<Session>.Filter.Eq("id", id);
            session.id = id;
            collection.FindOneAndReplace(filter, session);
        }

        public void DeleteSession(string id)
        {
            collection.DeleteOne(X => X.id == id);
        }
    }
}
