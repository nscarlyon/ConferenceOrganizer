using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceOrganizer.Data
{
    public interface ISessionsCollection
    {
        Session GetSession(string id);
        IEnumerable<Session> GetSessions();
        void PostSession(Session session);
        void DeleteSession(string id);
        void PutSession(string id, Session session);
    }
}
