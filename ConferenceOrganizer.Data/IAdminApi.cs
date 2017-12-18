using System.Collections.Generic;

namespace ConferenceOrganizer.Data
{
    public interface IAdminApi
    {
        CFP GetCFPStatus();
        void PutCFP(string id, CFP cfp);
        IEnumerable<string> GetSpeakers();
        Session GetSession(string id);
        IEnumerable<Session> GetSessions();
        void PostSession(Session session);
        void DeleteSession(string id);
        void PutSession(string id, Session session);
    }
}