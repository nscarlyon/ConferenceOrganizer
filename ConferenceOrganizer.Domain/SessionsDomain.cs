using ConferenceOrganizer.Data;
using System.Collections.Generic;

namespace ConferenceOrganizer.Domain
{
    public class SessionsDomain
    {
        private SessionsCollection sessionsCollection;

        public SessionsDomain()
        {
            sessionsCollection = new SessionsCollection();
        }

        public IEnumerable<MongoSession> GetSessions()
        {
            return sessionsCollection.GetSessions();
        }

        public MongoSession GetSessionById(string id)
        {
            return sessionsCollection.GetSession(id);
        }

        public void PostSession(MongoSession session)
        {
            sessionsCollection.PostSession(session);
        }

        public void PutSession(string id, MongoSession session)
        {
            sessionsCollection.PutSession(id, session);
        }

        public void DeleteSessionById(string id)
        {
            sessionsCollection.DeleteSession(id);
        }

        public void DeleteSessions()
        {
            sessionsCollection.DeleteSessions();
        }
    }
}
