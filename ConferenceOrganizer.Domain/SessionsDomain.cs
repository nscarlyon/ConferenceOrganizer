using ConferenceOrganizer.Data;
using ConferenceOrganizer.Domain.DomainModels;
using System.Collections.Generic;
using System.Linq;

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
            var sessions = sessionsCollection.GetSessions();

            return sessions.Select(session => new Session
            {
                id = session.id,
                SpeakerName = session.SpeakerName,
                Bio = session.Bio,
                Email = session.Email,
                ProposalId = session.ProposalId,
                Title = session.Title,
                Description = session.Description,
                Room = session.Room,
                StandardTime = session.StandardTime,
                Break = session.Break
            });
        }

        public Session GetSessionById(string id)
        {
            var session = sessionsCollection.GetSession(id);
            return new Session
            {
                id = session.id,
                SpeakerName = session.SpeakerName,
                Bio = session.Bio,
                Email = session.Email,
                ProposalId = session.ProposalId,
                Title = session.Title,
                Description = session.Description,
                Room = session.Room,
                StandardTime = session.StandardTime,
                Break = session.Break
            };
        }

        public void PostSession(Session session)
        {
            var mongoSession = new MongoSession
            {
                SpeakerName = session.SpeakerName,
                Bio = session.Bio,
                Email = session.Email,
                ProposalId = session.ProposalId,
                Title = session.Title,
                Description = session.Description,
                Room = session.Room,
                StandardTime = session.StandardTime,
                Break = session.Break
            };

            sessionsCollection.PostSession(mongoSession);
        }

        public void PutSession(string id, Session session)
        {
            var mongoSession = new MongoSession
            {
                SpeakerName = session.SpeakerName,
                Bio = session.Bio,
                Email = session.Email,
                ProposalId = session.ProposalId,
                Title = session.Title,
                Description = session.Description,
                Room = session.Room,
                StandardTime = session.StandardTime,
                Break = session.Break
            };

            sessionsCollection.PutSession(id, mongoSession);
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
