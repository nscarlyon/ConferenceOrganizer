using System.Collections.Generic;

namespace ConferenceOrganizer.Data
{
    public interface IConferenceOrganizerDatabase
    {
        IEnumerable<string> GetSpeakers();
        Session GetSession(string id);
        IEnumerable<Session> GetSessions();
        void PostSession(Session session);
        void DeleteSession(string id);
        void PutSession(string id, Session session);

        IEnumerable<Proposal> GetProposals();
        IEnumerable<Proposal> GetProposalsBySpeaker(string name);
        Proposal FindProposal(string id);
        void UpdateProposal(Proposal proposal);
        void PostProposal(Proposal proposal);
        void DeleteProposal(string id);
        void DeleteProposals();

        Schedule GetSchedule();
        void PostSchedule(Schedule schedule);
        void PutSchedule(string id, Schedule schedule);
        void PublishSchedule();
        void UnpublishSchedule();
        void DeleteSchedule();
    }
}