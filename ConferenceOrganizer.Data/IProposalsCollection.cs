using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceOrganizer.Data
{
    public interface IProposalsCollection
    {
        IEnumerable<Proposal> GetProposals();
        Proposal FindProposal(string id);
        void UpdateProposal(Proposal proposal);
        void PostProposal(Proposal proposal);
        void DeleteProposal(string id);
        void DeleteProposals();
    }
}
