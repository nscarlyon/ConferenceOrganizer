using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceOrganizer.Data
{
    public interface ISessionsCollection
    {
        MongoSession GetSession(string id);
        IEnumerable<MongoSession> GetSessions();
        void PostSession(MongoSession session);
        void DeleteSession(string id);
        void PutSession(string id, MongoSession session);
    }
}
