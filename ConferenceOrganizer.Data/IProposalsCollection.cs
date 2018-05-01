using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceOrganizer.Data
{
    public interface IProposalsCollection
    {
        IEnumerable<MongoProposal> GetProposals();
        MongoProposal GetProposalById(string id);
        void UpdateProposal(MongoProposal proposal);
        void PostProposal(MongoProposal proposal);
        void DeleteProposal(string id);
        void DeleteProposals();
    }
}
