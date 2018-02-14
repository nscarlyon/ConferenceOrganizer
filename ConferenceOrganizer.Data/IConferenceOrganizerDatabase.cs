using System.Collections.Generic;

namespace ConferenceOrganizer.Data
{
    public interface IConferenceOrganizerDatabase
    {
        CFP GetCFPStatus();
        void PutCFP(string id, CFP cfp);

        IEnumerable<string> GetSpeakers();
        Session GetSession(string id);
        IEnumerable<Session> GetSessions();
        void PostSession(Session session);
        void DeleteSession(string id);
        void PutSession(string id, Session session);

        IEnumerable<Proposal> GetProposals();
        IEnumerable<Proposal> GetProposalsBySpeaker(string name);
        Proposal FindProposal(string id);
        void PostProposal(Proposal proposal);
        void DeleteProposal(string id);
        void DeleteProposals();

        Schedule GetSchedule();
        void SetScheduleRooms(string id, Rooms rooms);
        void PostSchedule(Schedule schedule);
        void DeleteSchedule();
    }
}