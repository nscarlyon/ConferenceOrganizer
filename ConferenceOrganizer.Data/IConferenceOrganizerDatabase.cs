using System.Collections.Generic;

namespace ConferenceOrganizer.Data
{
    public interface IConferenceOrganizerDatabase
    {
        CFP GetCFPStatus();
        IEnumerable<string> GetSpeakers();
        IEnumerable<Proposal> GetProposalsBySpeaker(string name);
        void PutCFP(string id, CFP cfp);
        IEnumerable<Proposal> GetProposals();
        Proposal FindProposal(string id);
        void PostProposal(Proposal proposal);
        void PutProposal(string id, Proposal proposal);
        void DeleteProposal(string id);
        void DeleteProposals();
        IEnumerable<Session> GetSessions();
    }
}