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

        public IEnumerable<Session> GetSessions()
        {
            return sessionsCollection.GetSessions();
        }

        public Session GetSessionById(string id)
        {
            return sessionsCollection.GetSession(id);
        }

        public void PostSession(Session session)
        {
            sessionsCollection.PostSession(session);
        }

        public void PutSession(string id, Session session)
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
