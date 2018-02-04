using ConferenceOrganizer.Data;
using System.Collections.Generic;
using System;

namespace ConferenceOrganizer.Domain
{
    public class SessionsDomain
    {
        private ConferenceOrganizerDatabase conferenceOrganizerDatabase;

        public SessionsDomain()
        {
            conferenceOrganizerDatabase = new ConferenceOrganizerDatabase();
        }

        public IEnumerable<Session> GetSessions()
        {
            return conferenceOrganizerDatabase.GetSessions();
        }

        public Session GetSessionById(string id)
        {
            return conferenceOrganizerDatabase.GetSession(id);
        }

        public void PostSession(Session session)
        {
            conferenceOrganizerDatabase.PostSession(session);
        }

        public void PutSession(string id, Session session)
        {
            conferenceOrganizerDatabase.PutSession(id, session);
        }

        public void DeleteSessionById(string id)
        {
            conferenceOrganizerDatabase.DeleteSession(id);
        }

        public void DeleteSessions()
        {
            conferenceOrganizerDatabase.DeleteSessions();
        }
    }
}
