using System.Collections.Generic;

namespace ConferenceOrganizer.Domain.DomainModels
{
    public class Proposal
    {
        public string Id { get; set; }
        public string SpeakerName { get; set; }
        public string Bio { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public IEnumerable<ScheduledSession> ScheduledSessions { get; set; }
    }

    public class ScheduledSession
    {
        public string SessionId { get; set; }
        public string Room { get; set; }
        public string StandardTime { get; set; }

        public override bool Equals(object obj)
        {
            var session = obj as ScheduledSession;
            return session != null &&
                   SessionId == session.SessionId &&
                   Room == session.Room &&
                   StandardTime == session.StandardTime;
        }
    }
}
