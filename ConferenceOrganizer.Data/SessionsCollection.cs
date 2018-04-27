using MongoDB.Driver;
using System.Collections.Generic;

namespace ConferenceOrganizer.Data
{
    public class SessionsCollection : ISessionsCollection
    {
        IMongoCollection<Session> collection;
        IMongoDatabase database;

        public SessionsCollection()
        {
            database = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("conferenceOrganizer");
            collection = database.GetCollection<Session>("sessions");
        }

        public void DeleteSessions()
        {
            collection.DeleteMany(X => true);
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
