using ConferenceOrganizer.Data;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceOrganizer.Domain
{
    public class SessionsDomain
    {
        private SessionsCollection sessionsCollection;
        private ProposalsCollection proposalsCollection;

        public SessionsDomain()
        {
            sessionsCollection = new SessionsCollection();
            proposalsCollection = new ProposalsCollection();
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
            if (session.Break == false) SetProposalScheduledTimes(session);
        }

        private void SetProposalScheduledTimes(MongoSession session)
        {
            var sessions = sessionsCollection.GetSessions();
            var postedSession = sessions.Find(s => s.Room == session.Room && s.StandardTime == session.StandardTime);
            var proposal = proposalsCollection.GetProposalById(postedSession.ProposalId);
            var scheduledTime = new MongoScheduleTime
            {
                SessionId = postedSession.id,
                Room = postedSession.Room,
                StandardTime = postedSession.StandardTime
            };

            if (proposal.ScheduledTimes != null) proposal.ScheduledTimes.Add(scheduledTime);
            else
            {
                proposal.ScheduledTimes = new List<MongoScheduleTime>
                {
                   scheduledTime
                };
            } 
            proposalsCollection.UpdateProposal(proposal);
        }

        public void PutSession(string id, MongoSession session)
        {
            sessionsCollection.PutSession(id, session);
        }

        public void DeleteSessionById(string id)
        {
            RemoveProposalScheduledTime(id);
            sessionsCollection.DeleteSession(id);
        }

        public void RemoveProposalScheduledTime(string id)
        {
            var session = sessionsCollection.GetSession(id);
            if (session.Break == false)
            {
                var proposal = proposalsCollection.GetProposalById(session.ProposalId);
                proposal.ScheduledTimes = proposal.ScheduledTimes.Where(s => s.SessionId != session.id).ToList();
                proposalsCollection.UpdateProposal(proposal);
            }
        }

        public void DeleteSessions()
        {
            sessionsCollection.DeleteSessions();
        }
    }
}
