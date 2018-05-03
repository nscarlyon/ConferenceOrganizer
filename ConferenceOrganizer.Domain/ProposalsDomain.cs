using ConferenceOrganizer.Data;
using System.Collections.Generic;

namespace ConferenceOrganizer.Domain
{
    public class ProposalsDomain
    {
        private ProposalsCollection proposalsCollection;

        public ProposalsDomain()
        {
            proposalsCollection = new ProposalsCollection();
        }

        public IEnumerable<MongoProposal> GetProposals()
        {
            return proposalsCollection.GetProposals();
        }

        public MongoProposal GetProposalById(string id)
        {
            return proposalsCollection.GetProposalById(id);
        }

        public void PostProposal(MongoProposal proposal)
        {
            proposalsCollection.PostProposal(proposal);
        }

        public void UpdateProposal(MongoProposal proposal)
        {
            proposalsCollection.UpdateProposal(proposal);
        }

        public IEnumerable<MongoProposal> DeleteProposalById(string id)
        {
            proposalsCollection.DeleteProposal(id);
            return GetProposals();
        }

        public void DeleteProposals()
        {
            proposalsCollection.DeleteProposals();
        }
    }
}
