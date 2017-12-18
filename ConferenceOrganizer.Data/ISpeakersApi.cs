using System.Collections.Generic;

namespace ConferenceOrganizer.Data
{
    public interface ISpeakersApi
    {
        IEnumerable<Proposal> GetProposals();
        Proposal FindProposal(string id);
        void PostProposal(Proposal proposal);
        void DeleteProposal(string id);
        void DeleteProposals();
    }
}
