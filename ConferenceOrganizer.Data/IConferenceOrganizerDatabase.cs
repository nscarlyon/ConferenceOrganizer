using System.Collections.Generic;

namespace ConferenceOrganizer.Data
{
    public interface IConferenceOrganizerDatabase
    {
        //IEnumerable<Proposal> GetProposals();
        //Proposal FindProposal(string id);
        //void UpdateProposal(Proposal proposal);
        //void PostProposal(Proposal proposal);
        //void DeleteProposal(string id);
        //void DeleteProposals();

        Schedule GetSchedule();
        void PostSchedule(Schedule schedule);
        void PutSchedule(string id, Schedule schedule);
        void PublishSchedule();
        void UnpublishSchedule();
        void DeleteSchedule();
    }
}